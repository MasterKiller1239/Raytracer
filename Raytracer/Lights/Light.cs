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
        /// <summary>
        /// Light class constructor
        /// </summary>
        /// <param name="position">Position of light</param>
        /// <param name="color">color of light</param>
        public Light(Vector3 position, Color color)
        {
            this.position = position;
            this.color = color;
        }

        /// <summary>
        /// Phong light model
        /// </summary>
        /// <param name="Lightdir">Lightdir</param>
        /// <param name="LookDir">LookDir</param>
        /// /// <param name="normal">normal</param>
        /// <param name="mcolor">mcolor</param>
        /// <param name="material">material</param>
        public virtual void Phong(
       ref Vector3 Lightdir,ref Vector3 LookDir,ref Vector3 normal, ref
        MultiColor mcolor,
        Material material)
        {
             double specForce=0;
          //  mcolor.specular.Show();

           //   this.color.Show();
            double f = (-Lightdir).dot(normal);

      
            //diffuse
            mcolor.diffuse = mcolor.diffuse * material.diffuseColor *color * (-Lightdir).dot(normal);
     
            //specular
            specForce = (LookDir).dot(Lightdir.Reflect(normal));
            
            if (specForce < 0) specForce = 0;
     
            //    mcolor.specular.Show();
            mcolor.specular *=  material.specularColor * color * Math.Pow(specForce, material.shininess);




            }

        public abstract void TestColor(ref Ray ray, ref HitInfo hit, ref Primitive hitObject, ref List<Primitive> shapes,
           ref MultiColor color);
        

    }


}
//if (specForce > 0)
//    Console.WriteLine(specForce);
//if (!mcolor.specular.isZero())
//{
//    Console.WriteLine("1");
//    mcolor.specular.Show();
//    Console.WriteLine("2");
//    material.specularColor.Show();
//    Console.WriteLine("3");
//    Console.WriteLine(specForce);
//}

//Console.WriteLine(specForce);
//Console.WriteLine("-------------------------");
//Console.WriteLine(material.shininess); 
//if (!mcolor.specular.isZero() && !mcolor.specular.isOne())