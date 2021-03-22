using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer
{
    public class Sphere
    {
        /// <summary>
        /// Center point of sphere
        /// </summary>
        public Vector3 Center;
        /// <summary>
        /// Radius of sphere
        /// </summary>
        public float Radius;
        /// <summary>
        /// Squared value of radius (optimization purposes)
        /// </summary>
        float squareRadius;
        /// <summary>
        /// Default constuctor of sphere with center in (0,0,0) and radius 1
        /// </summary>
        public Sphere()
        {
            this.Center = new Vector3(0f);
            this.Radius = 1.0f;
            squareRadius = 1.0f;
        }

        /// <summary>
        /// Sphere class constructor
        /// </summary>
        /// <param name="vector">Center point of sphere</param>
        /// <param name="v">Radius of sphere</param>

        public Sphere(Vector3 vector, float v) 
        {
            this.Center = vector;
            this.Radius = v;
            squareRadius = this.Radius * this.Radius;
            Console.WriteLine(Center.ToString() + Radius.ToString());

        }
        /// <summary>
        /// Counting intersection between sphere and ray
        /// </summary>
        /// <param name="ray">Ray object which with sphere intersects (or not)</param>
        /// <param name="distance">Max detection distance from ray origin</param>
        /// <returns>Intersection value</returns>
        public int countIntersection(Ray ray, float distance)
        {
            Vector3 vec = ray.Origin - Center;
            Vector3 rayDirection = ray.Direction;
            float a = rayDirection.dot(rayDirection);
            float b = rayDirection.dot(vec);
            float c = vec.dot(vec) - squareRadius;
            float det = (b * b) - a * c;
            int returnValue = 0;
            if (det > 0.0f)
            {
                det = MathF.Sqrt(det);
                float i1 = (-b - det) / a;
                float i2 = (-b + det) / a;
                if (i2 > 0)
                {
                    returnValue =2;
                    if (i1 < 0 && i2 < distance)
                    {
                        distance = i2;
                    }
                    else if (i1 < distance)
                    {
                        distance = i1;
                    }
                }
            }
            else if (det==0)
            {
                float i0 = -b / a;
                if (i0 < distance)
                {
                    distance = i0;
                    returnValue = 1;
                }
            }

            return returnValue;
        }

      
      

    }
}

