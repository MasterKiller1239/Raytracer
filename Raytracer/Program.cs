using System;

namespace Raytracer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ball \n");
          Sphere ball = new Sphere(new Vector3(0f,0f,0f),10f);
            Console.WriteLine("ray1 \n");
            Ray ray1 = new Ray(new Vector3(0, 0, -20), new Vector3(0f, 0f, 1f));
            Console.WriteLine("ray2 \n");
            Ray ray2 = new Ray(new Vector3(0, 0, -20), new Vector3(0f, 1f, 0f));
          
            float distance1 = 40.0f;
            int intersection1 = ball.countIntersection(ray1, distance1, out float d1);
            distance1 = d1;
            Console.WriteLine("Intersection between ball and ray1:" + intersection1+"\n");
            Console.WriteLine("Closest intersection point:" +ray1.PointAtDistance(distance1).ToString() + "\n");
           
            float distance2 = 20.0f;
            int intersection2 = ball.countIntersection(ray2, distance2, out float d2);
            distance2 = d2;
            Console.WriteLine("Intersection between ball and ray2:" + intersection2 + "\n");

            Console.WriteLine("ray3 \n");
            Ray ray3 = new Ray(new Vector3(10, 10, 0), new Vector3(-1, 0, 0));
            float distance3 = 20.0f;
            int intersection3 = ball.countIntersection(ray3, distance3, out float d3);
            distance3 = d3;
            Console.WriteLine("Intersection between ball and ray3:" + intersection3 + "\n");
            Console.WriteLine("Closest intersection point:" + ray3.PointAtDistance(distance3).ToString() + "\n");

            Console.WriteLine("Plane \n");
            Plane p = new Plane(new Vector3(0, 1, 1), new Vector3(0));
            float distance4 = 30.0f;
            int intersection4 = p.countIntersection(ray2, distance4, out float d4);
            distance4 = d4;
            Console.WriteLine("Intersection between plane and ray2:" + intersection4 + "\n");
            Console.WriteLine("Closest intersection point:" + ray2.PointAtDistance(distance4).ToString() + "\n");


        }
    }
}
