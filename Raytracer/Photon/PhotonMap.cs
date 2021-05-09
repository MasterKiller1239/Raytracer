using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.Photon
{
    class PhotonMap
    {
		public List<Photon> photons;
		public List<Photon> data;
		public List<Photon> knn;
		public int maxPhotons;
		public int currentKNNSize = 1;
		public int median(ref List<Photon> photons, int w, int dimension, int a, int b)
		{
			return 1;
		}
		public int partition(ref List<Photon> photons, int dimension, int a, int b)
		{
			return 1;
		}

		public void AddPhoton(ref Photon p) { photons.Add(p); }
		public void crateDataVector(int size)
        {

        }
		public void createKdTree(List<Photon> photons, int depth, int a, int b, int positionInArray = 1)
        {

        }
		public void findPhotons(ref Vector3 point, int photonsNumer,ref double searchRadius, int positionInArray,ref List<Photon> knnPhotons)
        {

        }
		void insertPhotonToKNN(ref Vector3 p, ref List<Photon> knnPhotons)
        {

        }
		void removePhotonFromKNN(ref List<Photon> knnPhotons)
        {

        }

        PhotonMap()
        {

        }
        PhotonMap(int size)
        {
			maxPhotons = size + 1;
			int temp = 0;
			while (size > 0)
			{
				size /= 2;
				temp++;
			}
			int dataSize = 1 << temp;
            data =  new List<Photon>(dataSize + 1);
			data[0] = null;
		}
        Vector3 GetColor(double searchSquareRarius, ref Vector3 inter, int knnSize, ref Vector3 normal)
        {
			Vector3 colorTemp= new Vector3();
			currentKNNSize = 1;
			knn = new List<Photon>(knnSize + 1);
			for (int i = 0; i <= knnSize; i++)
			{
				knn[i] = null;
			}
			if (data.Count() == 0)
			{
				
				crateDataVector(photons.Count());
				createKdTree(photons, 1, 0, photons.Count(), 1);
			}
			double search = searchSquareRarius;
			findPhotons(ref inter, knnSize,ref search, 1,ref knn);
			if (knn[1] == null)
			{
				return colorTemp;
			}
			double r = knn[1].distance;
			for (int i = 1; i < knn.Count(); i++)
			{
				Photon photon = knn[i];
				if (photon == null) continue;
				double cosinus = normal.x * photon.direction[0] + normal.y * photon.direction[1] + normal.z * photon.direction[2];
				if (cosinus < 0) continue;
				colorTemp.x += photon.energy[0];
				colorTemp.y += photon.energy[1];
				colorTemp.z += photon.energy[2];

			}
			colorTemp *= (1 / (Math.PI * r));
			return colorTemp;
		}
        void ScaleEnergy()
        {
			float counter = 1 / 8;
			foreach (Photon x in data)
			{
				x.energy[0] *= counter;
				x.energy[1] *= counter;
				x.energy[2] *= counter;
			}
		}
    }
}
