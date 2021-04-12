using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer
{
    public class Ray
    {
        /// <summary>
        /// Origin of ray
        /// </summary>
        public Vector3 Origin;
        /// <summary>
        /// Ray direction
        /// </summary>
        public Vector3 Direction;
 
        public double distance;

        /// <summary>
        /// Default constructor for Ray
        /// </summary>
        public Ray()
        {
            Origin = new Vector3(0f);
            Direction = new Vector3(0f);

        }

        /// <summary>
        /// Constructor for Ray
        /// </summary>
        /// <param name="origin">Vector3 value assigned to origin</param>
        /// <param name="direction">Vector3 value assigned to direction</param>

        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            if(!direction.isZero())
            Direction = direction.normalizeProduct();

            this.distance = 10000000;
          // Console.WriteLine(Origin.ToString() + Direction.ToString());
        }

        public Ray(Vector3 origin, Vector3 direction, double distance)
        {
            this.Origin = origin;
            this.Direction = direction.normalizeProduct();

            this.distance = distance;
            //  Console.WriteLine(Origin.ToString() + Direction.ToString());
        }

        /// <summary>
        /// Get point at distance from ray
        /// </summary>
        /// <param name="distance">Distance from ray's origin</param>
        /// <returns>point shiftd from origin by distance</returns>

        public Vector3 PointAtDistance(float distance)
        {
              Vector3 temp = new Vector3(Origin +Direction*distance);
            return temp;
        }
        public void LookAt(Vector3 destination)
        {
            this.Direction = (destination - Origin).normalizeProduct();
        }
        public  Primitive CastAtPrimitive(ref Ray ray,ref  HitInfo hitInfo, ref List<Primitive> primitives)
        {
            Primitive nearestHit = null;

            hitInfo.distance = 10000000;

            foreach (var p in primitives)
	{
                HitInfo hi=new HitInfo();
                int res = p.Intersect(ref ray, ref hi);
             
                if (res > 0)
                {
                    if (hi.distance < hitInfo.distance)
                    {
                        
                        nearestHit = p;
                     //   nearestHit.GetMaterial().diffuseColor.Show();
                        hitInfo = hi;
                    }
                }
            }

            return nearestHit;

        }
        public static Primitive CastAtPrimitive(ref Ray ray, ref List<Primitive> primitives)
        {
            HitInfo hi = new HitInfo();
            return ray.CastAtPrimitive(ref ray, ref hi, ref primitives);

        }

    }
}
