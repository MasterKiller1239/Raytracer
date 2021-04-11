using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Raytracer
{
	public class Perspective :Camera
    {
		public Perspective()
		{
			this.position = new Vector3(0, 0, 0);
			this.target = new Vector3(0, 0, 1);
			this.nearPlane = 1;
			this.farPlane = 1000;
			this.up = new Vector3(0, 1, 0);
		}
		public Perspective(Vector3 position, Vector3 target)
		{
			this.position = position;
			this.target = target;
			this.nearPlane = 1;
			this.farPlane = 1000;
			this.up = new Vector3(0, 1, 0);
		}

        public override Ray GetRayThroughtPixel(ref Vector3 pixel)
        {
			Ray ray= new Ray(position,new Vector3(0, 0, 0));
			ray.distance = farPlane;
			ray.LookAt(pixel);
			return ray;
		}

        /// <summary>
        /// Camera Setup
        /// </summary>
        /// <param name="image">Bitmap</param>
        /// <param name="sfera">Primitives List</param>

        public override void RenderTo(ref Bitmap image)
		{
			int imageWidth = image.Width;
			int imageHeight = image.Height;

			double aspectRatio = imageWidth / (double)imageHeight;
			double tang = Math.Tan(fov * 0.5 * 3.141f / 180.0);
			double height = 2 * nearPlane * tang;
			double width = height * aspectRatio;

			double widthPixel = width / imageWidth;
			double heightPixel = height / imageHeight;

			minimumPixelSizeForAdaptive = heightPixel / (double)Math.Pow(2.0, adaptiveDepth);

			Vector3 w = -target.normalizeProduct();
			u = -(up.cross(w).normalizeProduct());
			v = w.cross(u);
			Vector3 c = Position - u * (float)(width * 0.5) - v * (float)(height * 0.5) + target * nearPlane;

			//base.RenderInternal(ref image, c, (float)widthPixel, (float)heightPixel,sfera);
			base.RenderInternal(ref image, ref c, (float)widthPixel, (float)heightPixel);

		}
		
	}
}
