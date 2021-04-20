using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.ObjHandler
{
	public class Material
    {
		public double shininess = 4;
		public double mirror = 0;
		public double refractive = 0;
		public double refractiveIndex = 1;
		public Color diffuseColor = new Color(1, 1, 1);
		public Color specularColor = new Color(0, 0, 0);
		public Bitmap texture =null;
	}
}
