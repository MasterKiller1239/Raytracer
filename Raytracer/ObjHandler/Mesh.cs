using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer
{
    public class Mesh
    {
		public String name = "Mesh";
		public List<Face> faces = new List<Face>() ;
       public  ObjFile obj = new ObjFile();
		/// <summary>
		/// Add Face to mesh
		/// </summary>
		/// <param name="data">data table</param>
		public void AddFace(int[,] data)
        {
			Face f = new Face();
			f.SetMesh(this);
			for (int i = 0; i < 3; i++)
			{
				f.triangleData[i] = data[i,0];
				f.uvData[i] = data[i,1];
				f.normalData[i] = data[i,2];
			}

			f.UpdateTriangle();

			f.GetTriangle().GetMaterial().specularColor = new Color(1, 1, 1);
			f.GetTriangle().GetMaterial().diffuseColor = Color.GetRandom(0,1); 
			//f.GetTriangle().GetMaterial().diffuseColor = new Color(1, 1, 1); ;
			faces.Add(f);
		}
		/// <summary>
		/// well name speaks for itself
		/// </summary>
		/// <param name=" t">texture</param>
		public void SetTextureForAllFaces(Bitmap t)
        {
			foreach(Face f in faces) 
			{
				f.GetTriangle().GetMaterial().texture = t;
			}
		}
    }
}
