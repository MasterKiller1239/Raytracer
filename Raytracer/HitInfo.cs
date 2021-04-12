using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer
{
    public class HitInfo
    {

        public Vector3 point =new Vector3();
        public double distance = 10000000;
        public Vector3 uvw = new Vector3();
        public Vector3 normal = new Vector3();
    }
}
