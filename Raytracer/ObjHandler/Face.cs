using Raytracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer
{
    public class Face
    {
        private Triangle triangle = null;
        public  Mesh mesh;
      public ObjFile obj;
        public bool hasUvs = true;
        public bool hasNormals = true;

        public int[] triangleData = new int[3];
        public int[] uvData = new int[3];
        public int[] normalData = new int[3];
        /// <summary>
        /// mesh setter
        /// </summary>
        /// <param name="m">mesh</param>
     
        public void SetMesh(Mesh m)
        {
            mesh = m;
            ObjFile obj1 = m.obj;
            obj = obj1;
        }
        /// <summary>
        /// Get Triangle
        /// </summary>

        public Triangle GetTriangle()
        {
            if (triangle == null)
                UpdateTriangle();
            return  triangle;
        }
        /// <summary>
        ///  Update Triangle
        /// </summary>

        public void UpdateTriangle()
        {
            //if (triangle != null)
                
            triangle = new Triangle(
                obj.vertices[triangleData[0] - 1],
                obj.vertices[triangleData[1] - 1],
                obj.vertices[triangleData[2] - 1]);
            if (obj.uvs.Count() >= uvData[0] && obj.uvs.Count() >= uvData[1] && obj.uvs.Count() >= uvData[2])
            {
                triangle.UVx = obj.uvs[uvData[0] - 1];
                triangle.UVy = obj.uvs[uvData[1] - 1];
                triangle.UVz = obj.uvs[uvData[2] - 1];
              
            }
        }
    }
}
