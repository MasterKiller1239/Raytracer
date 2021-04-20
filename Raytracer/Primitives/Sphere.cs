using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.Primitives
{
    public class Sphere : Primitive
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
            squareRadius = v * v;
           // Console.WriteLine(Center.ToString() + Radius.ToString());

        }
        public bool ContainsPoint(Vector3 point, double tolerance)
        {
            return (point - Center).dot(point - Center) - squareRadius <= tolerance;
        }
        ///<summary>
        ///Sprawdza, czy dany promień przecina sferę.
        ///Zwraca 0, gdy nie ma przecięcia. Zwraca 1, gdy promień jest styczną. Zwraca 2, gdy promieć jest cięciwą.
        ///Podany przez referencję Vector3 przyjmie wartość bliższego punktu przecięcia.
        ///</summary>
        public override int Intersect(ref Ray ray, ref Vector3 hit)
        {
            double distance=20;
            int result = Intersect(ref ray, ref hit,ref distance);

            return result;
        }
 
    public override int Intersect(ref Ray ray, ref Vector3 hit,ref double distance)
        {
            int result = Intersect(ref ray, ref distance);
           
            if (result > 0)
                hit = ray.Origin + ray.Direction * (float)distance;
            else
                hit = new Vector3(0.0f);

            return result;
        }

        public override int Intersect(ref Ray ray, ref HitInfo hit)
        {
            int result = Intersect(ref ray, ref hit.point,ref hit.distance);
            if (result != 0)
            {
               //Console.WriteLine(result);
                hit.normal = (hit.point - this.Center).GetNormalized();
            
            }
              
            return result;
        }
        int Intersect(ref Ray ray, ref double distance) 
        {

            Vector3 v = ray.Origin - Center;
            double b = -v.dot(ray.Direction);
            double det = (b * b) - v.dot(v) + Radius * Radius;
            distance = ray.distance;
	        int result = 0;

            if (det > 0)
            {
                det = Math.Sqrt(det);
                double t1 = b - det;
                double t2 = b + det;
                if (t2 > 0)
                {
                    if (t1 < 0)
                    {
                        if (t2 < distance)
                        {
                            distance = t2;
                            result = -1;
                        }
                    }
                    else
                    {
                        if (t1 < distance)
                        {
                            distance = t1;
                            result = 2;
                        }
                    }
                }
            }
            else if (det == 0)
            {
                double t0 = b;
                distance = t0;
                result = 1;
            }
            if (result == 0) distance = -1;
            return result;
        }
















        /// <summary>
        /// Counting intersection between sphere and ray
        /// </summary>
        /// <param name="ray">Ray object which with sphere intersects (or not)</param>
        /// <param name="distance">Max detection distance from ray origin</param>
        /// <param name="dist">new distance</param>
        /// <returns>Intersection value</returns>
        public int countIntersection(ref Ray ray, float distance, out float dist)
        {
            dist = distance;
            Vector3 vec = ray.Origin - Center;
            Vector3 rayDirection = ray.Direction;
            float a = rayDirection.dot(rayDirection);
            float b = rayDirection.dot(vec);
            float c = vec.dot(vec) - squareRadius;
            float det = b * b - a * c;
            int returnValue = 0;
            if (det > 0.0f)
            {
                det = (float)Math.Sqrt(det);
                float i1 = (-b - det) / a;
                float i2 = (-b + det) / a;
                if (i2 > 0)
                {
                    returnValue = 2;
                    if (i1 < 0 && i2 < distance)
                    {
                        dist = i2;
                    }
                    else if (i1 < distance)
                    {
                        dist = i1;
                    }
                }
            }
            else if (det == 0)
            {
                float i0 = -b / a;
                if (i0 < distance)
                {
                    dist = i0;
                    returnValue = 1;
                }
            }
            else
                dist = distance;
            return returnValue;
        }
    }


}

