using Raytracer.Lights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.Photon
{
    class Scene
    {
        PhotonMap global;
        PhotonMap caustic;
        public void emitPhotons(ref PhotonMap photonMap, bool causticMap, PhotonReflection type)
        {
            Random rnd = new Random();
            float intensity = lights[0].intensity;
            float red = (float)(lights[0].color.Red() * intensity);
            float green = (float)(lights[0].color.Green() * intensity);
            float blue = (float)(lights[0].color.Blue() * intensity);
            while (photonMap.size() < photonMap.maxPhotons)
            {
                Photon photon = new Photon();
                float[] tab = new float[3];
                do
                {
                    tab[0] = (float)((rnd.Next() % 10000 - 5000.0) / 5000.0);
                    tab[1] = (float)((rnd.Next() % 10000 - 5000.0) / 5000.0);
                    tab[2] = (float)((rnd.Next() % 10000 - 5000.0) / 5000.0);
                } while (tab[0] * tab[0] + tab[1] * tab[1] + tab[2] * tab[2] > 1);
              
                    photon.position[0] = lights[0].position.x;
                    photon.direction[0] = tab[0];
                photon.position[1] = lights[0].position.y;
                photon.direction[1] = tab[1];
                photon.position[2] = lights[0].position.z;
                photon.direction[2] = tab[2];

                photon.position[0] += (float)(((rnd.Next() % 50) / 10.0) - 5.0);
                photon.position[2] += (float)(((rnd.Next() % 50) / 10.0) - 5.0);
                if (causticMap)
                {
                    bool wasIntesect = false;
                     Ray ray = new Ray(new Vector3(photon.position[0], photon.position[1], photon.position[2]),new Vector3(tab[0], tab[1], tab[2]));
                    ray.Direction.GetNormalized();
                    int index = -1;
                    double distance = 1000;
                    foreach (var x in meshes)
                    {
                        index++;
                        double hitDistance = 1000;
                        if (x.Intersect(ref ray))
                        {
                            if (hitDistance < distance)
                            {
                                distance = hitDistance;
                              

                                    wasIntesect = true;

                            }
                        }
                    }
                    if (!wasIntesect)
                    {
                        photon = new Photon();
                        continue;
                    }

                }
                photon.energy[0] = red;
                photon.energy[1] = green;
                photon.energy[2] = blue;
                TracePhoton(ref photon, photonMap, type);

                photon = new Photon();
            }


            photonMap.ScaleEnergy();
        }
        Color color;
        int maxReflections = 40;
        public void TracePhoton(ref Photon photon, PhotonMap photonMap, PhotonReflection type)
        {
			bool wasReflection = false;
			bool wasCausticReflection = false;
			Random rnd = new Random();
			for (int i = 0; i < maxReflections; i++)
			{
				Ray ray= new Ray(new Vector3(photon.position[0], photon.position[1], photon.position[2]), new Vector3(photon.direction[0], photon.direction[1], photon.direction[2]));
				//cout << "pozycja " << ray.Origin() << endl;
				//cout << "kierunek " << ray.Direction() << endl;
				int index = -1;
				int j = -1;
				double distance = 1000;
				bool wasHit = false;
				foreach (Mesh mezh in meshes)
				{
					j++;
					
					if (mezh.Intersect(ref ray))
					{
						
							wasHit = true;
							
					}
				}
				if (!wasHit) break;
				Mesh mesh = meshes[index];
				Vector3 inter = mesh.IntersectV(ref ray);
				Vector3  normal = mesh.getNormal(ray, distance, inter);
				Color  baze = new Color();
				if (mesh.faces[0].GetTriangle().GetMaterial()!=null)
				{
					Float2 coords = mesh.getUVCoords(normal, inter);
					baze = mesh.faces[0].GetTriangle().GetMaterial().diffuseColor;
				}
				else
				{
					baze = mesh.faces[0].GetTriangle().GetMaterial().diffuseColor;
				}
				float avgColor = (float)((baze.Red() + baze.Green() + baze.Blue()) / 3.0);
				float newRed = (float)(photon.energy[0] * baze.Red() / avgColor);
				float newBlue = (float)(photon.energy[1] * baze.Blue() / avgColor);
				float newGreen = (float)(photon.energy[2] * baze.Green() / avgColor);
				if (mesh.faces[0].GetTriangle().GetMaterial() != null)
				{
					if (!wasCausticReflection && type == PhotonReflection.caustic) break;
					photon.position[0] = inter.x;
					photon.position[1] = inter.y;
					photon.position[2] = inter.z;
					int probability = rnd.Next() % 100;//prawdopodobienstwo odbicia
					if (probability < 40)
					{
						photonMap.AddPhoton(ref photon);
						break;
					}
					else
					{
						//	cout << "odbicie\n";
						//wasReflection = true;
						float[] tab =new float[3];
						do
						{
							tab[0] = (float)((rnd.Next() % 10000 - 5000.0) / 5000.0);
							tab[1] = (float)((rnd.Next() % 10000 - 5000.0) / 5000.0);
							tab[2] = (float)((rnd.Next() % 10000 - 5000.0) / 5000.0);
						} while (tab[0] * tab[0] + tab[1] * tab[1] + tab[2] * tab[2] > 1);
						for (int m = 0; m < 3; m++)
						{
							photon.direction[m] = tab[m];
						}
						if (!wasCausticReflection && type == PhotonReflection.caustic)
						{
							photon.energy[0] = newRed;
							photon.energy[1] = newGreen;
							photon.energy[2] = newBlue;
							continue;
						}
						else
						{
                            photonMap.AddPhoton(ref photon);
							photon.energy[0] = newRed;
							photon.energy[1] = newGreen;
							photon.energy[2] = newBlue;
						}
					}
				}
				else
				{
					wasReflection = true;
					wasCausticReflection = true;
					Ray topoint = new Ray(new Vector3(photon.position[0], photon.position[1], photon.position[2]), inter);
					Ray FromPoint = new Ray();
					if (mesh.faces[0].GetTriangle().GetMaterial().mirror>0)
					{
						FromPoint = mesh.faces[0].GetTriangle().GetMaterial().MirrorRay(ref topoint,ref  normal,ref inter);
					}
					else if (mesh.faces[0].GetTriangle().GetMaterial().refractive>0)
					{

						FromPoint = mesh.faces[0].GetTriangle().GetMaterial().RefractRay(ref topoint, ref normal,ref inter, false);
					}
					photon.position[0] = inter.x;
					photon.position[1] = inter.y;
					photon.position[2] = inter.z;
					Vector3  dir = FromPoint.Direction;
					dir.GetNormalized();
					photon.direction[0] = dir.x;
					photon.direction[1] = dir.y;
					photon.direction[2] = dir.z;
					photon.energy[0] = newRed;
					photon.energy[1] = newGreen;
					photon.energy[2] = newBlue;
					//	cout << "zmienil sie kierunek photonu na " << photon.direction << endl;
				}


			}
		}
        Color tempColor = new Color(0, 0, 0);
        List<Mesh> meshes;
        List<Mesh> CausticMeshes;
        List<Light> lights;
        Scene()
        {

        }
    }
}
