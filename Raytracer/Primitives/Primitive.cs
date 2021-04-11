using Raytracer.ObjHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer
{
   public abstract class Primitive
    {
        Material material = new Material();

        public abstract int Intersect(ref Ray ray, ref Vector3 hit);
     
        public abstract int Intersect(ref Ray ray, ref Vector3 hit, ref double distance);
        public abstract int Intersect(ref Ray ray, ref HitInfo hit) ;

        public Material GetMaterial()
        {
           
            return material;
        }
        public void SetMaterial(Material mat)
        {
            material = mat;
        }
    }

}
