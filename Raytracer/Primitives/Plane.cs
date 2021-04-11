using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.Primitives
{
     class Plane : Primitive
    {
        int INT_MAX = 2147483646;
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
        public Plane(Vector3 planePoint, Vector3 normal) : this()
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

        public Plane()
        {
        }



        public override int Intersect(ref Ray ray, ref Vector3 hit)
        {
            double distance = 0;

            return Intersect(ref ray, ref hit,ref distance);
        }

        public override int Intersect(ref Ray ray, ref Vector3 hit, ref double distance)
        {
            int atFront = 0;
            distance = 0;
            Vector3 hitPoint = new Vector3();
          
            hitPoint = Intersect(ref ray,ref atFront,ref distance);
         
                if (hitPoint.isZero())
                {
                    hit = hitPoint;
                }

                if (atFront == INT_MAX)
                    hit = ray.Origin;
            

            return atFront;
        }
        public Vector3 Intersect(ref Ray ray,ref int atFront,ref double distance) 
{

    double up = (planePoint - ray.Origin).dot(Normal);
        double denom = Normal.dot(ray.Direction);

        distance = -1;
           //  Console.WriteLine(distance);
	if (Math.Abs(denom) > 0.00001)
	{
		double t = up / denom;
		if (t >= 0 && t<ray.distance)
		{
			if (denom< 0) atFront = 1;
			else atFront = -1;

			distance = t;
                  // Console.WriteLine(t);
			return new Vector3(ray.Origin + (ray.Direction *(float) t));
		}
} else if (up < 0.00001)
{
    atFront = INT_MAX;
}
return new Vector3(0.0f);
}

        public override int Intersect(ref Ray ray, ref HitInfo hit)
        {
            int result = Intersect(ref ray, ref hit.point,ref hit.distance);
            hit.normal = Normal;
            return result;
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
            if (nDotV != 0)
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