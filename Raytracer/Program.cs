using Raytracer.Lights;
using Raytracer.Primitives;
using System;
using System.Drawing;
using System.IO;

namespace Raytracer
{
    class Program
    {
        static void Main(string[] args)
        {
            //  //Console.WriteLine("ball \n");
            //Sphere ball = new Sphere(new Vector3(0f,0f,0f),10f);`
            ////  Console.WriteLine("ray1 \n");
            //  Ray ray1 = new Ray(new Vector3(0, 0, -20), new Vector3(0f, 0f, 1f));
            // // Console.WriteLine("ray2 \n");
            //  Ray ray2 = new Ray(new Vector3(0, 0, -20), new Vector3(0f, 1f, 0f));

            //  float distance1 = 40.0f;
            //  int intersection1 = ball.countIntersection(ray1, distance1, out float d1);
            //  distance1 = d1;
            //  Console.WriteLine("Intersection between ball and ray1:" + intersection1+"\n");
            //  Console.WriteLine("Closest intersection point:" +ray1.PointAtDistance(distance1).ToString() + "\n");

            //  float distance2 = 20.0f;
            //  int intersection2 = ball.countIntersection(ray2, distance2, out float d2);
            //  distance2 = d2;
            //  Console.WriteLine("Intersection between ball and ray2:" + intersection2 + "\n");

            ////  Console.WriteLine("ray3 \n");
            //  Ray ray3 = new Ray(new Vector3(0, 10, 10), new Vector3(0, -1, 0));
            //  float distance3 = 20.0f;
            //  int intersection3 = ball.countIntersection(ray3, distance3, out float d3);
            //  distance3 = d3;
            //  Console.WriteLine("Intersection between ball and ray3:" + intersection3 + "\n");
            //  Console.WriteLine("Closest intersection point:" + ray3.PointAtDistance(distance3).ToString() + "\n");

            // // Console.WriteLine("Plane \n");
            //  Plane p = new Plane(new Vector3(0, 1, 1), new Vector3(0));
            //  float distance4 = 30.0f;
            //  int intersection4 = p.countIntersection(ray2, distance4, out float d4);
            //  distance4 = d4;
            //  Console.WriteLine("Intersection between plane and ray2:" + intersection4 + "\n");
            //  Console.WriteLine("Closest intersection point:" + ray2.PointAtDistance(distance4).ToString() + "\n");

            //ZAD2
            string path = Directory.GetCurrentDirectory() + "\\1a.bmp";
            Bitmap imagex = new Bitmap(path);
            Bitmap image = new Bitmap(1024, 1024,
System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            Bitmap image1 = new Bitmap(1024, 1024,
System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            string fileToRead = Directory.GetCurrentDirectory() + "\\CubeTex2.obj";
            ObjReader oRead = new ObjReader();
            ObjFile meshes = oRead.ReadFromFile(fileToRead);

            Primitive plane1 = new Plane(new Vector3(0, 1.5f, 0), new Vector3(0, -1, 0));
            plane1.GetMaterial().diffuseColor = new Color(0, 0, 1);

            Primitive plane2 = new Plane(new Vector3(0, 0.0f, 12), new Vector3(0, 0, -1));
            plane2.GetMaterial().diffuseColor = new Color(0.8, 0.8, 0.2);
            // plane2.GetMaterial().mirror = 0.9;

            Primitive plane3 = new Plane(new Vector3(0, -2.5f, 0), new Vector3(0, 1, 0));
            plane3.GetMaterial().diffuseColor = new Color(1, 1, 1);
            //plane3.GetMaterial().mirror = 0.9;

            Primitive plane4 = new Plane(new Vector3(3, 0, 0), new Vector3(-1, 0, 0));
            plane4.GetMaterial().diffuseColor = new Color(0.1, 1, .1);
            //plane3.GetMaterial().mirror = 0.9;

            Primitive plane5 = new Plane(new Vector3(-3, 0, 0), new Vector3(1, 0, 0));
            plane5.GetMaterial().diffuseColor = new Color(1, .1, .1);
            // plane5.GetMaterial().mirror = 0.9;

            Primitive plane6 = new Plane(new Vector3(0, 0, -4), new Vector3(0, 0, 1));
            plane6.GetMaterial().diffuseColor = new Color(0.2, 0.2, 0.2);

            Primitive ball1 = new Sphere(new Vector3(-1, -0.5f, 0), 0.8f);
            Primitive ball2 = new Sphere(new Vector3(1, -0.5f, 1.7f), 0.8f);


            ball1.GetMaterial().diffuseColor = new Color(1.0, 1.0, 1.0);
            ball1.GetMaterial().specularColor = new Color(1.0, 1.0, 1.0);

            ball1.GetMaterial().shininess = 12;
            ball1.GetMaterial().mirror = 0;
            ball1.GetMaterial().refractive = 1;
            ball1.GetMaterial().refractiveIndex = 3.5;
            ball2.GetMaterial().diffuseColor = new Color(0, 1, 0);
            ball2.GetMaterial().specularColor = new Color(1.0, 1.0, 1.0);
            ball2.GetMaterial().shininess = 12;
            ball2.GetMaterial().mirror = 0.9;
            ball2.GetMaterial().refractive = 0;
            ball2.GetMaterial().refractiveIndex = 1.5;
            // Console.WriteLine(meshes.meshes[0].name);
            // Console.WriteLine(oRead.ReadFromFile(fileToRead).meshes.Count);
            Scene scene = new Scene();

            Mesh mesh = meshes.meshes[0];
            //   mesh.obj.Move(new Vector3(0, -1, 0));
            mesh.SetTextureForAllFaces(imagex);
            //  ball1.GetMaterial().texture = imagex;
          

            scene.AddPrimitiveForRendering(ref ball1);
            scene.AddPrimitiveForRendering(ref ball2);
            scene.AddMeshForRendering(ref mesh);
            scene.AddPrimitiveForRendering(ref plane1);
            scene.AddPrimitiveForRendering(ref plane2);
            scene.AddPrimitiveForRendering(ref plane3);
            scene.AddPrimitiveForRendering(ref plane4);
            scene.AddPrimitiveForRendering(ref plane5);
            //    scene.AddPrimitiveForRendering(ref plane6);


            PointLight pl1 = new PointLight(new Vector3(-0.5f, -0.5f, -1), new Color(0.7, 0.7, 0.7));
            PointLight pl0 = new PointLight(new Vector3(-1, 0, 2), new Color(0.9 , 0.9, 0.5));
            PointLight pl12 = new PointLight(new Vector3(-1, 0, 6), new Color(0.9, 0.1, 0.9));

            //PointLight pl1 = new PointLight(new Vector3(1, 0, -1), new Color(1, 1, 1));
            //PointLight pl0 = new PointLight(new Vector3(-1, -0.5f, -1.5f), new Color(0.7, 0.7, 0.7));
            scene.lights.Add(pl1);
            scene.lights.Add(pl0);
  
           // scene.lights.Add(pl12);
           
            Ortho ort = new Ortho(new Vector3(-0.2f, -0.8f, -2f), new Vector3(0.1f, 0.1f, 1));
            ort.adaptiveDepth = 2;

            ort.scene = scene;
            Console.WriteLine("Zapisywanie do pliku obrazu z kamery ort");
            ort.RenderTo(ref image);

            image.Save("D:\\Raytracer\\"  + "ortho.jpg");
            //  Perspective per = new Perspective(new Vector3(0.0f, 0.0f, 1.0f), new Vector3(0.0f, 0.0f, -3));
            Perspective per = new Perspective(new Vector3(-0.2f, -0.8f, -2f), new Vector3(0.1f, 0.1f, 1));
            per.Position = new Vector3(0, 0, -3);
            per.Target = new Vector3(0, 0, 1);
            per.scene = scene;
            per.adaptiveDepth = 2;
            per.fov = 75;
            Console.WriteLine("Zapisywanie do pliku obrazu z kamery per");
            per.RenderTo(ref image1);

            image1.Save("D:\\Raytracer\\" + "perpektywa.jpg");
            Console.WriteLine((pl1.count+ pl0.count));
            Console.WriteLine("Koniec");
            //ort.RenderTo(ref image, spheres);
            //per.RenderTo(image1, spheres);

        }
    }
}
