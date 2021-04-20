using Raytracer.Lights;
using Raytracer.Primitives;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Raytracer
{
    public abstract class Camera
    {
        public int count = 0;
        public int reflectionDepth = 4;

        public Scene scene = null;
        protected double minimumPixelSizeForAdaptive = -1;
        protected Vector3 u, v;
        public int adaptiveDepth = 4;
        protected Vector3 position;
        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        protected Vector3 direction;
        public Vector3 Target
        {
            get { return direction; }
            set { direction = value; }
        }
        public Vector3 up;
        public Vector3 Up
        {
            get { return up; }
            set { up = value; }
        }
        protected float nearPlane;
        public float NearPlane
        {
            get { return nearPlane; }
            set { nearPlane = value; }
        }
        protected float farPlane;
        public float FarPlane
        {
            get { return farPlane; }
            set { farPlane = value; }
        }
        public float fov = 60;
        public float Fov
        {
            get { return fov; }
            set { fov = value; }
        }
        public Camera()
        {
            this.position = new Vector3(0, 0, 0);
            this.direction = new Vector3(0, 0, 1);
            this.nearPlane = 1;
            this.farPlane = 1000;
            this.up = new Vector3(0, 1, 0);
        }
        public Camera(Vector3 position, Vector3 target)
        {
            this.position = position;
            this.direction = target;
            this.nearPlane = 1;
            this.farPlane = 1000;
            this.up = new Vector3(0, 1, 0);
        }

        public virtual Primitive CastAtPrimitive(ref Ray ray)
        {
            HitInfo hit = new HitInfo();
            return CastAtPrimitive(ref ray, ref hit);
        }
        public virtual Primitive CastAtPrimitive(ref Ray ray, ref HitInfo hit)
        {
            return ray.CastAtPrimitive(ref ray, ref hit, ref scene.primitives);
        }

        public virtual Color ColorCastAtPrimitiveThroughPixel(ref Vector3 subpixel)
        {
            Ray ray = GetRayThroughtPixel(ref subpixel);

            return ColorCastAtPrimitive(ref ray, reflectionDepth);
        }
        public Color ColorCastAtPrimitive(ref Ray ray, int depth)
        {
            HitInfo hi = new HitInfo();
            Primitive nearestHit = null;
             nearestHit = CastAtPrimitive(ref ray, ref hi);
            //if (depth < 3)
            //{
            //    count++;
            //    Console.WriteLine(count);

            //}

            // Console.WriteLine(depth);
            if (nearestHit != null)
            {


                MultiColor baze = new MultiColor();
                baze.diffuse = new Color(1, 1, 1);
                baze.specular = new Color(1, 1, 1);

                Triangle triHit = null;
                Sphere sphHit = null;
                //if (nearestHit is Triangle)
                //    triHit=(Triangle)nearestHit;
                //if (nearestHit is Sphere)

                //    sphHit = (Sphere)nearestHit;
                // Sphere sphHit =null;

                if (nearestHit.GetMaterial().texture != null)
                {

                    if (nearestHit is Triangle)
                    {

                        triHit = nearestHit as Triangle;
                        Vector3 texUV =
                            triHit.UVx * hi.uvw.x +
                            triHit.UVy * hi.uvw.y +
                            triHit.UVz * hi.uvw.z;

                        int texX = (int)((triHit.GetMaterial().texture.Width - 1) * texUV.x);
                        int texY = (int)((triHit.GetMaterial().texture.Height - 1) * texUV.y);

                        System.Drawing.Color tem = triHit.GetMaterial().texture.GetPixel(texX, texY);


                        baze.diffuse.Set(tem.R, tem.G , tem.B);

                    }
                    else if (nearestHit is Sphere)
                    {
                        sphHit = nearestHit as Sphere;
                        Console.WriteLine("xD1");
                        Console.ReadLine();
                        Vector3 hit = (hi.point - sphHit.Center) / sphHit.Radius;
                        double theta, phi;
                        Vector3 texUV = new Vector3();

                        theta = Math.Acos(hit.y);
                        phi = Math.Atan2(hit.x, hit.z);

                        if (phi < 0.0) phi += 6.28318;
                        if (phi > 6.28318) phi -= 6.28318;
                        if (theta < 0.0) theta += 3.14159;
                        if (theta > 3.14159) theta -= 3.14159;

                        texUV.y = (float)(phi * 0.159154); // u
                        texUV.x = (float)(1.0 - theta * 0.318309); // v

                        /*texUV = texUV*4;
                        texUV.x = fmod(texUV.x, 1);
                        texUV.y = fmod(texUV.y, 1);
                        texUV.z = fmod(texUV.z, 1);*/

                        int texY = (int)((sphHit.GetMaterial().texture.Width - 1) * (1 - texUV.x));
                        int texX = (int)((sphHit.GetMaterial().texture.Height - 1) * texUV.y);
                        System.Drawing.Color tem = sphHit.GetMaterial().texture.GetPixel(texX, texY);
                        //Console.WriteLine((tem.R / 255.0) + "," + (tem.G / 255.0) + "," + (tem.B / 255.0));
                        baze.diffuse.Set(tem.R, tem.G, tem.B );
                        //  baze.diffuse.Set(sphHit.GetMaterial().diffuseColor.Red(), sphHit.GetMaterial().diffuseColor.Green(), sphHit.GetMaterial().diffuseColor.Blue());

                    }

                }
              
                Color basicColor = new Color();
                MultiColor result = new MultiColor();
                //if (!baze.specular.isZero() && !baze.specular.isOne())
                //    baze.specular.Show();
                foreach (Light l in scene.lights)
                {
                    MultiColor tmpColor = new MultiColor();
                    tmpColor.diffuse = baze.diffuse;
                    tmpColor.specular = baze.specular;
                   
                    l.TestColor(ref ray, ref hi, ref nearestHit, ref scene.primitives, ref tmpColor);
               
                    result.diffuse = result.diffuse + tmpColor.diffuse;
                    result.specular = result.specular + tmpColor.specular;
                  
                }

   
                basicColor = result.diffuse + result.specular + scene.ambientColor * nearestHit.GetMaterial().diffuseColor;
           
                double mirror = nearestHit.GetMaterial().mirror;
                double refractive = nearestHit.GetMaterial().refractive;

                if (depth > 0)
                {
                 
                    Color mirrorColor = new Color();
                   
                    Color refractColor = new Color();
                    bool combine = false;
                    if (mirror > 0)
                    {

                        Vector3 nextRayDirection = ray.Direction.Reflect(hi.normal);
                        Ray nextRay = new Ray(hi.point + nextRayDirection *0.01, nextRayDirection);
                        mirrorColor = ColorCastAtPrimitive(ref nextRay, depth - 1);
                    
                        combine = true;

                    }
                    if (refractive > 0)
                    {
                        Vector3 refractiveDir = (ray.Direction - hi.normal * (nearestHit.GetMaterial().refractiveIndex - 1)).GetNormalized();
                        Ray insideRay = new Ray(hi.point - refractiveDir, refractiveDir);
                        HitInfo outHit = new HitInfo();
                        int secondResult = nearestHit.Intersect(ref insideRay, ref outHit);
                        if (secondResult == -1)
                        {

                            Vector3 refractiveDir2 = (insideRay.Direction + outHit.normal * (1 - nearestHit.GetMaterial().refractiveIndex)).GetNormalized();
                            Ray movedRay = new Ray(outHit.point + refractiveDir2 * 0.01, refractiveDir2);
                            refractColor = ColorCastAtPrimitive(ref movedRay, depth - 1);
                        }

                        Ray temp = new Ray(hi.point + refractiveDir * 0.01, refractiveDir);
                        refractColor = ColorCastAtPrimitive(ref temp, depth - 1);
                        //if (depth == 1)
                        //if (refractColor.xRed() != 0)
                        //    refractColor.Show();
                        combine = true;

                    }

                    if (combine)
                    {
                        //if (mirrorColor.xRed() == 0 && mirrorColor.xGreen() == 0 && mirrorColor.xBlue() == 0)
                        //    mirror = 0;
                        //if (refractColor.xRed() == 0 && refractColor.xGreen() == 0 && mirrorColor.xBlue() == 0)

                        //    refractive = 0;
                        Color c = basicColor * (1.0 - mirror - refractive) + mirrorColor * mirror + refractColor * refractive;

                        //   c.Show();


                        return c;
                    }

                    else
                    {
                        //if (nearestHit is Plane)
                        //    basicColor.Show();
                        return basicColor;
                    }

                }
                else
                {
                   
                    return basicColor;
                }
                   
            }
            else
            {
                return new Color(0, 0, 0);
            }

        }
        public abstract Ray GetRayThroughtPixel(ref Vector3 pixel);
        /// <summary>
        /// get color of the pixel
        /// </summary>
        public virtual void GetAdaptivePixelColor(ref Vector3 pixel, double widthPixel, double heightPixel, ref List<(Color, double)> colorsAndWeights, ref Color topLeft, ref Color topRight, ref Color botRight, ref Color botLeft, bool[] tab)
        {
           // Console.WriteLine("widthPixel: " + widthPixel);
         //   Console.WriteLine("heightPixel: " + heightPixel);
            Color center = ColorCastAtPrimitiveThroughPixel(ref pixel);
         
            // center.Show();
            // Console.ReadLine();
            bool[] done = tab;
            // Console.WriteLine(done[3]);
            if (heightPixel < minimumPixelSizeForAdaptive)
            {
                colorsAndWeights.Add((center, widthPixel * heightPixel));

                return;
            }

            double startWeight = widthPixel * heightPixel;
          //  Console.WriteLine("Start:            " + startWeight);
            if (!done[0]) //TOP LEFT
            {
                Vector3 temp = pixel + u * (float)(widthPixel * (-0.5)) + v * (float)(heightPixel * 0.5);
                topLeft = ColorCastAtPrimitiveThroughPixel(ref temp
                    );

            }
            if (!done[1]) //TOP RIGHT
            {
                Vector3 temp = pixel + u * (float)(widthPixel * (0.5)) + v * (float)(heightPixel * 0.5);
                topRight = ColorCastAtPrimitiveThroughPixel(ref temp
                      );

            }
            if (!done[2])//DOWN RIGHT
            {
                Vector3 temp = pixel + u * (float)(widthPixel * (0.5)) + v * (float)(heightPixel * -0.5);
                botRight = ColorCastAtPrimitiveThroughPixel(ref temp
                      );

            }
            if (!done[3])//DOWN LEFT
            {
                Vector3 temp = pixel + u * (float)(widthPixel * (-0.5)) + v * (float)(heightPixel * -0.5);
                botLeft = ColorCastAtPrimitiveThroughPixel(ref temp
                       );

            }
       
            
            //RECURSIVE
            double smallerWidthPixel = widthPixel * 0.5, smallerHeightPixel = heightPixel * 0.5;
            double weightModifier = 1;

            double colorDifferenceThreshold = 0.1;

            if (topLeft.Difference(center) > colorDifferenceThreshold)
            {
                Color one = new Color(); Color two = new Color();
                Vector3 temp = pixel + u * widthPixel * (-0.25) + v * heightPixel * 0.25;
                GetAdaptivePixelColor(
                   ref temp,
                    smallerWidthPixel, smallerHeightPixel,
                    ref colorsAndWeights,
                   ref topLeft, ref one, ref center, ref two,
                    new bool[4] { true, false, true, false });
                weightModifier -= 0.25;
            

            }

            if (topRight.Difference(center) > colorDifferenceThreshold)
            {
                Color one = new Color(); Color two = new Color();
                Vector3 temp = pixel + u * widthPixel * (0.25) + v * heightPixel * 0.25;
                GetAdaptivePixelColor(ref temp
                       ,
                        smallerWidthPixel, smallerHeightPixel,
                       ref colorsAndWeights,
                      ref one, ref topRight, ref two, ref center,
                        new bool[4] { false, true, false, true });
                weightModifier -= 0.25;
               
            }

            if (botRight.Difference(center) > colorDifferenceThreshold)
            {
                Color one = new Color(); Color two = new Color();
                Vector3 temp = pixel + u * widthPixel * (0.25) + v * heightPixel * (-0.25);
                GetAdaptivePixelColor(ref temp
                       ,
                        smallerWidthPixel, smallerHeightPixel,
                        ref colorsAndWeights,
                        ref center, ref one, ref botRight, ref two,
                        new bool[4] { true, false, true, false });
                weightModifier -= 0.25;
             
            }

            if (botLeft.Difference(center) > colorDifferenceThreshold)
            {
                Color one = new Color(); Color two = new Color();
                Vector3 temp = pixel + u * widthPixel * (-0.25) + v * heightPixel * (-0.25);
                GetAdaptivePixelColor(ref temp
                      ,
                        smallerWidthPixel, smallerHeightPixel,
                       ref colorsAndWeights,
                     ref center, ref one, ref botLeft, ref two,
                        new bool[4] { true, false, true, false });
                weightModifier -= 0.25;
           
            }
            //Console.WriteLine("Start: " + startWeight);
            //Console.WriteLine("Mod: " + weightModifier);
           
            colorsAndWeights.Add((center, startWeight * weightModifier));

        }
        /// <summary>
        /// Camera Setup
        /// </summary>
        public void RenderInternal(ref Bitmap img, ref Vector3 c, double widthPixel, double heightPixel)
        {
            int imgWidth = img.Width;
            int imgHeight = img.Height;
            
            for (int k = 0; k < imgHeight; k++)
            {
                Color one = new Color();
                Color two = new Color(); Color three = new Color(); Color four = new Color();
                List<(Color, double)> colorsAndWeights = new List<(Color, double)>();
                Vector3 temp = c + u * (0 + 0.5) * widthPixel + v * (k + 0.5) * heightPixel;
                GetAdaptivePixelColor(
                 ref temp,
                    widthPixel, heightPixel,
                    ref colorsAndWeights,
                   ref one, ref two, ref three, ref four,
                    new bool[4] { false, false, false, false });
            
               
                SetPixelColor(ref img, 0, k, Color.WeightedAverage(ref colorsAndWeights));
                colorsAndWeights.Clear();

                for (int j = 1; j < imgWidth; j++)
                {

                    if (j % 2 == 1)
                    {
                        Vector3 tem = c + u * (j + 0.5) * widthPixel + v * (k + 0.5) * heightPixel;
                        //Console.WriteLine(tem);
                        //Console.ReadLine();
                        GetAdaptivePixelColor(ref tem
                          ,
                           widthPixel, heightPixel,
                          ref colorsAndWeights,
                           ref one, ref two, ref three, ref four,
                           new bool[4] { false, true, true, false });
                        //if (!Color.WeightedAverage(ref colorsAndWeights).isZero())
                        //{
                        //    Color.WeightedAverage(ref colorsAndWeights).Show();
                        //    Console.ReadLine();
                        //}
                    }

                    else
                    {
                        Vector3 tem = c + u * (j + 0.5) * widthPixel + v * (k + 0.5) * heightPixel;
                        GetAdaptivePixelColor(
                        ref tem,
                           widthPixel, heightPixel,
                          ref colorsAndWeights,
                          ref two, ref one, ref four, ref three,
                           new bool[4] { false, true, true, false });
                      
                    }
                    //if (!Color.WeightedAverage(ref colorsAndWeights).isZero())
                    //{
                    //    Color.WeightedAverage(ref colorsAndWeights).Show();
                    //    Console.ReadLine();
                    //}
                    // Color.WeightedAverage(ref colorsAndWeights).Show();
                    SetPixelColor(ref img, j, k, Color.WeightedAverage(ref colorsAndWeights));
                    colorsAndWeights.Clear();

                }
            }
        }
        public Vector3 GetRandomUnitPoint()
        {
            Random r = new Random();
            float x = r.Next() % 10000 / 10000;
            float y = r.Next() % 10000 / 10000;

            return new Vector3(x, y, 0);
        }
        public void SetPixelColor(ref Bitmap image, int x, int y, Color c)
        {
            if (!c.isZero())
            {
               // Console.WriteLine(System.Drawing.Color.FromArgb(c.iRed(), c.iGreen(), c.iBlue()).ToString());
               // c.iShow();
               // Console.ReadLine();
            }

            image.SetPixel(x, y, System.Drawing.Color.FromArgb(c.iRed(), c.iGreen(), c.iBlue()));
        }

        public virtual void RenderTo(ref Bitmap image)
        {

        }
        /// <summary>
        /// Fills image
        /// </summary>
        /// <param name="image">Bitmap</param>

        public void Fillimage(ref Bitmap img)
        {
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    img.SetPixel(i, j, System.Drawing.Color.FromArgb(0, 250, 0));

                }
            }
        }
        /// <summary>
        /// Camera Setup
        /// </summary>
        /// <param name="image">Bitmap</param>
        /// <param name="widthPixel">widthPixel</param>
        /// /// <param name="heightPixel">heightPixel</param>
        /// <param name="sfera">Primitives List</param>
        //public void RenderInternal(ref  Bitmap img, Vector3 c,float widthPixel, float heightPixel,List<Sphere> sfera)
        //{
        //    int MaxJitter = 5;
        //    Random r = new Random();
        //    this.Fillimage(ref img);
        //    Console.WriteLine(sfera.Count());
        //    int intersetion = 1;
        //    Random TempRandom = new Random();
        //    for (int j = 0; j < img.Height; j++)

        //    {
        //        for (int i = 0; i < img.Width; i++)
        //        {
        //            float srodekX = -1.0f + (i + 0.5f) * widthPixel;
        //            float srodekY = 1.0f - (j + 0.5f) * heightPixel;
        //            Ray ray = new Ray(new Vector3(srodekX, srodekY, 0),new Vector3(0, 0, 1));

        //            for(int s=0;s<sfera.Count();s++)
        //            {
        //                 intersetion = sfera[s].countIntersection(ref ray);

        //                if (intersetion >0)
        //                {
        //                    //Console.WriteLine(intersetion);
        //                    img.SetPixel(i, j, System.Drawing.Color.FromArgb(250, 0, 0));
        //                }
        //               // else img.SetPixel(i, j, System.Drawing.Color.FromArgb(0, 250, 0));
        //            }

        //             }
        //    }
        //    Bitmap copy = img;
        //    for (int j = 0; j < img.Height; j++)

        //    {
        //        for (int i = 0; i < img.Width; i++)
        //        {
        //            int NewX = TempRandom.Next(-MaxJitter, MaxJitter);
        //            int NewY = TempRandom.Next(-MaxJitter, MaxJitter);
        //            NewX += i;
        //           NewY += j;
        //            NewX = (int)Camera.Clamp(NewX,0, img.Width - 1);
        //           NewY = (int)Camera.Clamp(NewY, 0, img.Height - 1);
        //            img.SetPixel(i, j, copy.GetPixel(NewX, NewY));

        //        }
        //    }

        //    img.Save("D:\\Raytracer\\"+r.Next(0,5000000).ToString()+".bmp");
        //}

        public static double Clamp(double value, double min, double max)
        {
            // First we check to see if we're greater than the max
            value = (value > max) ? max : value;

            // Then we check to see if we're less than the min.
            value = (value < min) ? min : value;

            // There's no check to see if min > max.
            return value;
        }
    }


}