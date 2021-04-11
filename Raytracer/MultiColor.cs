using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer
{
    public class MultiColor
    {
        public Color diffuse = new Color(0, 0, 0);
        public Color specular = new Color(0, 0, 0);

        public MultiColor()
        {
            diffuse = new Color(0, 0, 0);
            specular = new Color(0, 0, 0);
        }

        public MultiColor(Color diffuse, Color specular)
        {
            this.diffuse = diffuse;
            this.specular = specular;
        }
    }
}
