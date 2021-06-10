using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.Photon
{
    class Point : Vector3
    {
        public List<float> list;

        public Point()
        {
       

        }

        public Point(Vector3 v) : base(v)
        {

        }

        public Point(float newVal) : base(newVal)
        {
        }

        public Point(Vector3 p1, Vector3 p2) : base(p1, p2)
        {
        }

        public Point(float x, float y, float z) : base(x, y, z)
        {

        }
    }
}
