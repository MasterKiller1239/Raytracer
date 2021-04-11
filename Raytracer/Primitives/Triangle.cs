using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.Primitives
{
	public class Triangle :Primitive
    {
        public Vector3 normal;
        public Vector3 e1, e2;
        public Vector3 a, b, c, UVx, UVy, UVz;

        public Triangle()
        {
        }

		public Triangle(Vector3 a, Vector3 b, Vector3 c, bool reversed = false)
		{
			this.a = a;
			this.b = b;
			this.c = c;

			e1 = b - a;
			e2 = c - a;
			try
			{
				this.normal = ((e1).cross(e2)).normalizeProduct();
			}
			catch (Exception e)
		{
				throw new Exception("Wrong triangle vertices", e);
			}
			}
		public Vector3 GetNormal() 
	{
		return this.normal;
	}
		public override int Intersect(ref Ray ray, ref Vector3 hit)
		{
			HitInfo hi =new HitInfo();
			int result = Intersect(ref ray, ref hi);
			hit = hi.point;
			return result;
		}
		
	public override int Intersect(ref Ray ray, ref Vector3 hit, ref double distance)
		{
			HitInfo hi = new HitInfo();
			int result = Intersect(ref ray, ref hi);
			hit = hi.point;
			distance = hi.distance;
			return result;
		}
		public override int Intersect(ref Ray ray, ref HitInfo hit)
		{
			//Algorytm Möller–Trumbore
			double det, inv_det;

			//Zaczynamy liczyć deltę. Jeśli jest bliska zeru, to promień nie przechodzi przez trójkąt
			//Lub jest skierowany równolegle.
			Vector3 P = ray.Direction.cross(e2);
			det = e1.dot(P);
			if (det > -0.00001 && det < 0.00001) return 0;
			inv_det = 1 / det;

			Vector3 AToOrigin = (ray.Origin - a);

			hit.uvw.x = AToOrigin.dot(P) *(float) inv_det;

			if (hit.uvw.x < 0 || hit.uvw.x > 1)
				return 0; //Przecięcie leży poza trójkątem

			Vector3 Q = AToOrigin.cross(e1);

			hit.uvw.y = ray.Direction.dot(Q) *(float) inv_det;
			if (hit.uvw.y < 0 || hit.uvw.x + hit.uvw.y > 1)
				return 0; //Przecięcie leży poza trójkątem

			hit.uvw.z = 1 - hit.uvw.x - hit.uvw.y;

			hit.distance = e2.dot(Q) * inv_det;

			hit.point = ray.Origin + ray.Direction * (float)hit.distance;

			if (hit.distance > 0)
			{
				hit.normal = normal;
				return det > 0 ? 1 : -1;
			}

			return 0;
		}

   
    }
}
	
	
