using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer
{
    struct Sphere
    {
        public Vector Center;
        public float Radius;

        public static Sphere Create(Vector center, float radius)
        {
            Sphere s;
            s.Center = center;
            s.Radius = radius;
            return s;
        }

        public static bool Hit(Sphere sphere, Ray ray, float tMin, float tMax, out RayHit hit)
        {
            Vector center = sphere.Center;
            Vector oc = ray.Origin - center;
            Vector rayDir = ray.Direction;
            float a = Vector.Dot(rayDir, rayDir);
            float b = Vector.Dot(oc, rayDir);
            float radius = sphere.Radius;
            float c = Vector.Dot(oc, oc) - radius * radius;
            float discriminant = b * b - a * c;
            if (discriminant > 0)
            {
                float tmp = MathF.Sqrt(b * b - a * c);
                float t = (-b - tmp) / a;
                if (t < tMax && t > tMin)
                {
                    Vector position = Ray.PointAt(ray, t);
                    Vector normal = (position - center) / radius;
                    hit = RayHit.Create(Ray.PointAt(ray, t), t, normal);
                    return true;
                }
                t = (-b + tmp) / a;
                if (t < tMax && t > tMin)
                {
                    Vector position = Ray.PointAt(ray, t);
                    Vector normal = (position - center) / radius;
                    hit = RayHit.Create(position, t, normal);
                    return true;
                }
            }

            hit.Position = new Vector();
            hit.Normal = new Vector();
            hit.T = 0;
            return false;
        }
    }
}
}
