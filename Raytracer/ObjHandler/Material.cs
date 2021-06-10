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
		float refractionCooef = 1;
		internal Ray MirrorRay(ref Ray toPoint,ref Vector3 normal,ref Vector3 inter)
        {
			toPoint.Direction.GetNormalized();
			normal.GetNormalized();
			Vector3 direction = toPoint.Direction - normal * (normal.dot(toPoint.Direction)) * 2;
			return new Ray(inter, direction);
		}

        internal Ray RefractRay(ref Ray toPoint,ref Vector3 normal, ref Vector3 inter, bool v)
        {
			//http://web.cse.ohio-state.edu/~hwshen/681/Site/Slides_files/reflection_refraction.pdf
			Vector3 directionRay = -toPoint.Direction;
			directionRay.GetNormalized();
			double cosinus = directionRay.dot(normal);
			double n1 = 1;
			double n2 = refractionCooef;
			double n;
			if (!v)
			{
				n = n1 / n2;
			}
			else
			{
				n = n2 / n1;
			}
			double temp = n * cosinus;
			double temp2 = Math.Sqrt(1 - n * n * (1 - cosinus * cosinus));
			Vector3 direction = normal * (temp - temp2) - directionRay * n;
			direction.GetNormalized();
			return new Ray(inter, direction);
		}

      
    }
}
