using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Raytracer
{
    public class Camera
    {
        protected double minimumPixelSizeForAdaptive = -1;
        protected Vector3 u, v;
        protected int adaptiveDepth = 4;
        protected Vector3 position;
        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        protected Vector3 target;
        public Vector3 Target
        {
            get { return target; }
            set { target = value; }
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
        protected float fov = 60;
        public float Fov
        {
            get { return fov; }
            set { fov = value; }
        }
        public Camera()
        {
            this.position = new Vector3(0, 0, 0);
            this.target = new Vector3(0, 0, 1);
            this.nearPlane = 1;
            this.farPlane = 1000;
            this.up = new Vector3(0, 1, 0);
        }
        public Camera(Vector3 position, Vector3 target)
        {
            this.position = position;
            this.target = target;
            this.nearPlane = 1;
            this.farPlane = 1000;
            this.up = new Vector3(0, 1, 0);
        }

        public void SetPixelColor(Bitmap image,  int x,  int y,Color c)
        {
            image.SetPixel(x, y, System.Drawing.Color.FromArgb(c.iRed(),c.iGreen(), c.iBlue()));
        }

        public virtual void RenderTo(Bitmap image, List<Sphere> sfera)
        {

        }
        /// <summary>
        /// Fills image
        /// </summary>
        /// <param name="image">Bitmap</param>
        
        public void Fillimage(Bitmap img)
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
        public void RenderInternal(Bitmap img, Vector3 c,float widthPixel, float heightPixel,List<Sphere> sfera)
        {
            int MaxJitter = 5;
            Random r = new Random();
            this.Fillimage(img);
            Console.WriteLine(sfera.Count());
            int intersetion = 1;
            Random TempRandom = new Random();
            for (int j = 0; j < img.Height; j++)
              
            {
                for (int i = 0; i < img.Width; i++)
                {
                    float srodekX = -1.0f + (i + 0.5f) * widthPixel;
                    float srodekY = 1.0f - (j + 0.5f) * heightPixel;
                    Ray ray = new Ray(new Vector3(srodekX, srodekY, 0),new Vector3(0, 0, 1));
                   
                    for(int s=0;s<sfera.Count();s++)
                    {
                         intersetion = sfera[s].countIntersection(ray);
                       
                        if (intersetion >0)
                        {
                            //Console.WriteLine(intersetion);
                            img.SetPixel(i, j, System.Drawing.Color.FromArgb(250, 0, 0));
                        }
                       // else img.SetPixel(i, j, System.Drawing.Color.FromArgb(0, 250, 0));
                    }
                    
                     }
            }
            Bitmap copy = img;
            for (int j = 0; j < img.Height; j++)

            {
                for (int i = 0; i < img.Width; i++)
                {
                    int NewX = TempRandom.Next(-MaxJitter, MaxJitter);
                    int NewY = TempRandom.Next(-MaxJitter, MaxJitter);
                    NewX += i;
                   NewY += j;
                    NewX = (int)Camera.Clamp(NewX,0, img.Width - 1);
                   NewY = (int)Camera.Clamp(NewY, 0, img.Height - 1);
                    img.SetPixel(i, j, copy.GetPixel(NewX, NewY));

                }
            }
                   
            img.Save("D:\\"+r.Next(0,5000000).ToString()+".bmp");
        }

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