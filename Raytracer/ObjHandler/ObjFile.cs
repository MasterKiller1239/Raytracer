using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer
{
   public class ObjFile
    {
        public List<Mesh> meshes = new List<Mesh>();
        public List<Vector3> vertices = new List<Vector3>();
        public List<Vector3> normals = new List<Vector3>();
        public List<Vector3> uvs = new List<Vector3>();
        /// <summary>
        /// well name speaks for itself
        /// </summary>
        
        public Mesh CreateAndAddMesh()
        {
            Mesh m = new Mesh();
            AddMesh(ref m);
            return m;
        }
        /// <summary>
		/// Adds mesh to list
		/// </summary>
		/// <param name=" t">texture</param>
        public Mesh AddMesh(ref Mesh m)
        {
            m.obj = this;
            meshes.Add(m);
            return m;
        }

        public void Move(Vector3 move)
        {
           foreach(var vert in vertices)
            {
                vert.add(move);
            }
        }
    }
}
