using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer
{
    public struct RayHit
    {
        public Vector Position;
        public float T;
        public Vector Normal;

        public static RayHit Create(Vector position, float t, Vector normal)
        {
            RayHit hit;
            hit.Position = position;
            hit.T = t;
            hit.Normal = normal;
            return hit;
        }
    }
}
