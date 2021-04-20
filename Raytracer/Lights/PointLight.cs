using Raytracer.ObjHandler;
using Raytracer.Primitives;
using System;
using System.Collections.Generic;

namespace Raytracer.Lights
{
    public class PointLight :  Light
    {
        public int count = 0;
        public PointLight(Vector3 position, Color color) : base(position, color) { }

        /// <summary>
        /// Shades checker
        /// </summary>
        /// <param name="ray">ray</param>
        /// <param name="hit">hit</param>
        /// /// <param name="hitObject">hitObject</param>
        /// <param name="shapes">Primitive</param>
        /// <param name="color">color</param>
        public override void TestColor(ref Ray ray, ref HitInfo hit, ref Primitive hitObject, ref List<Primitive> shapes,
       ref MultiColor mcolor)
        {
          //  color.diffuse.Show();
            Vector3 fromMe2Pt = new Vector3();

            bool inShadow = false;
              Ray r = new Ray(position, hit.point - position, hit.distance + 1);
            //Console.WriteLine(hit.normal.ToString());

            inShadow = Ray.CastAtPrimitive(ref r, ref shapes)!=(hitObject);

            //if (inShadow == false)
            //{
            //    color.diffuse.Show();
            //}
            if (inShadow)
            {
                count++;
                mcolor.diffuse *= 0;
                mcolor.specular *= 0;

            }
            //if (!mcolor.specular.isZero()&& !mcolor.specular.isOne())
            //   mcolor.specular.Show();
            // shading
            fromMe2Pt = (hit.point - position).GetNormalized();
                Ray temp = ray;

                temp.Direction = -ray.Direction;
                Material material = hitObject.GetMaterial();
               
                Phong(ref fromMe2Pt, ref temp.Direction, ref hit.normal, ref mcolor,  material);

            
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
