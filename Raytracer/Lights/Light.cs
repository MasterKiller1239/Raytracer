using Raytracer.ObjHandler;
using Raytracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.Lights
{
    public abstract class Light
    {
        public Vector3 position;
        public Color color;
        public Light(Vector3 position, Color color)
        {
            this.position = position;
            this.color = color;
        }

        public virtual void Phong(
       ref Vector3 Lightdir,ref Vector3 LookDir,ref Vector3 normal, ref
        MultiColor mcolor,
       ref Primitive hitObject)
        {
             double specForce;
            
           
          //  color.Show();
            double f = (-Lightdir).dot(normal);

            //if (hitObject is Plane)
            //{
            //    Console.WriteLine("1");
            //    mcolor.diffuse.Show();
            //    Console.WriteLine("2");
            //    hitObject.GetMaterial().diffuseColor.Show();
            //    Console.WriteLine("3");
            //    Console.WriteLine(f);
            //    Console.WriteLine("4");
            //}
            //if (hitObject is Plane)
            //    hitObject.GetMaterial().diffuseColor.Show();
            //diffuse
            mcolor.diffuse = mcolor.diffuse * hitObject.GetMaterial().diffuseColor *color *f  ;

          //  mcolor.diffuse.Show();


            //specular
            specForce = LookDir.dot(Lightdir.Reflect(normal));
            if (specForce < 0) specForce = 0;

            mcolor.specular = mcolor.specular * hitObject.GetMaterial().specularColor * color * Math.Pow(specForce, hitObject.GetMaterial().shininess);

           

        }

        public abstract void TestColor(ref Ray ray, ref HitInfo hit, ref Primitive hitObject, ref List<Primitive> shapes,
           ref MultiColor color);
        

    }


}
