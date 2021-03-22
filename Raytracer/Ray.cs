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
        /// <summary>
        /// Point which with ray intersects (it lays on ray's route)
        /// </summary>
        Vector3 destination;
        /// <summary>
        /// Default constructor for Ray
        /// </summary>
        public Ray()
        {
            Origin = new Vector3(0f);
            Direction = new Vector3(0f);
            destination = new Vector3(0f);
        }
        /// <summary>
        /// Setter of Vector3 value destination
        /// </summary>
        /// <param name="newDestination">New value of destination</param>
        public void setDestination(Vector3 newDestination)
        {
            destination = newDestination;
            if (destination.length()!=0)
            {
                Direction = new Vector3(destination.normalizeProduct() - Origin);
               
            }
            else
            {
                Direction = new Vector3(- Origin);

            }

        }
        /// <summary>
        /// Constructor for Ray
        /// </summary>
        /// <param name="origin">Vector3 value assigned to origin</param>
        /// <param name="direction">Vector3 value assigned to direction</param>

        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction.normalizeProduct();
          
            this.destination=new Vector3(0f);
            Console.WriteLine(Origin.ToString() + Direction.ToString());
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
    }
}
