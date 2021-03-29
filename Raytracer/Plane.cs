using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer
{
    struct Plane
    {

        /// <summary>
        /// Shift the plane along the normalVector from (0,0,0): -D
        /// </summary>
        public float D;
        /// <summary>
        /// Normal vector of plane
        /// </summary>
        public Vector3 Normal;
        /// <summary>
        /// Point that belongs to plane
        /// </summary>
        public Vector3 planePoint;
        /// <summary>
        /// Constructor of plane using normal vector of plane and D factor
        /// </summary>
        /// <param name="normal"></param>
        ///  /// <param name="d"></param>
        public Plane(Vector3 normal, float d)
        {
            Normal = normal;
            D = d;
            planePoint = new Vector3(0f);
        }
        /// <summary>
        /// Constuctor of plane using general equation.
        /// </summary>
        /// <param name="a, b, c, d">General equation of plane factors</param>
        public Plane(float a, float b, float c, float d)
            
        {
            Vector3 temp= new Vector3(a, b, c);
            if (temp.isZero())
            {
               Console.WriteLine("Plane normal vector cannot be zero!");
            }
            Normal = temp.normalizeProduct();
            if (c==0)
            {
                planePoint = new Vector3(0.0f, 0.0f, -d / c);
            }
            else if (b == 0)
            {
                planePoint = new Vector3(0.0f, -d / b, 0.0f);
            }
            else
            {
                planePoint = new Vector3(-d / a, 0.0f, 0.0f);
            }
            this.D = -d;
            Console.WriteLine(Normal.ToString() + planePoint.ToString());

        }
        /// <summary>
        /// Constructor of plane using normal vector of plane and one of plane points
        /// </summary>
        /// <param name="normal"></param>
        /// <param name="planePoint"></param>
        public Plane(Vector3 normal, Vector3 planePoint) : this()
        {
            if (normal.isZero())
            {
                
                Console.WriteLine("Plane normal vector cannot be zero!");
            }
            Normal = normal.normalizeProduct(); 
            this.planePoint = planePoint;
            D = normal.dot(planePoint);
          //  Console.WriteLine(Normal.ToString() + planePoint.ToString());

        }


        /// <summary>
        /// Counting intersection between plane and ray
        /// </summary>
        /// <param name="ray">Ray object which with plane intersects (or not)</param>
        /// <param name="distance">Max detection distance from ray origin</param>
        /// <param name="dist">new distance</param>
        /// <returns>Intersection value</returns>
        public int countIntersection(Ray ray, float distance, out float dist)
        {
            dist = distance;
            float nDotV = Normal.dot(ray.Direction);
            int returnValue = 0;
            if (nDotV!=0)
            {
                float t = (D - Normal.dot(ray.Origin)) / nDotV;
                if (t >= 0.001 && distance > t)
                {
                    dist = t;
                    returnValue = 1;
                }
            }
            return returnValue;
            ;
        }
    }        
}