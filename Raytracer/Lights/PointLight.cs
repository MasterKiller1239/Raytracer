using Raytracer.ObjHandler;
using Raytracer.Primitives;
using System;
using System.Collections.Generic;

namespace Raytracer.Lights
{
    public class PointLight : Light
    {
        public PointLight(Vector3 position, Color color) : base(position, color) { }


        public override void TestColor(ref Ray ray, ref HitInfo hit, ref Primitive hitObject, ref List<Primitive> shapes,
       ref MultiColor color)
        {
          //  color.diffuse.Show();
            Vector3 fromMe2Pt = new Vector3();

            bool inShadow = false;
            {

                Ray r = new Ray(position, hit.point - position, hit.distance + 1);
                inShadow = Ray.CastAtPrimitive(ref r, ref shapes) != hitObject;
                //if(hitObject is Plane && inShadow==true)
                //{
                //    Console.WriteLine(inShadow);
                //}
            }
            // && !(hitObject is Plane)
            if (inShadow)
            {

                color.diffuse *= 0;
                color.specular *= 0;

            }
            {
                // shading
                fromMe2Pt = (hit.point - position).normalizeProduct();
                Ray temp = ray;
                //if (hitObject is Plane && color.diffuse.xRed()==1)
                //{
                //	color.diffuse.Show();
                //}
                temp.Direction = -ray.Direction;
                Material material = hitObject.GetMaterial();
                //if (hitObject is Plane)
                //    color.diffuse.Show();
                Phong(ref fromMe2Pt, ref temp.Direction, ref hit.normal, ref color, ref hitObject);

            }
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
