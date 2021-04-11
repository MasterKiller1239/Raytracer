using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Raytracer
{


	public class Ortho : Camera
    {

		protected double height = 4;

	
		public Ortho()
		{
			this.position = new Vector3(0, 0, 0);
			this.target = new Vector3(0, 0, 1);
	
			this.up = new Vector3(0, 1, 0);
		}
		public Ortho(Vector3 position, Vector3 target)
		{
			this.position = position;
			this.target = target;
		
			this.up = new Vector3(0, 1, 0);
		}

        public override Ray GetRayThroughtPixel(ref Vector3 pixel)
        {
			return new Ray(pixel, target);
		}

        /// <summary>
        /// Camera Setup
        /// </summary>
        /// <param name="image">Bitmap</param>
       
        public override void RenderTo(ref Bitmap image)
        {
			int imageWidth = image.Width;
			 int imageHeight = image.Height;

			double aspectRatio = imageWidth / (double)imageHeight;

			double width = height * aspectRatio;

			double widthPixel = width / imageWidth;
			double heightPixel = height / imageHeight;

			minimumPixelSizeForAdaptive = heightPixel / (double)Math.Pow(2.0, adaptiveDepth);

			Vector3 w = -target.normalizeProduct();
			u = -(up.cross(w).normalizeProduct());
			v = w.cross(u);
			Vector3 c = Position - u * (float)(width * 0.5) - v * (float)(height * 0.5);

			//base.RenderInternal(ref image, c, (float)widthPixel, (float)heightPixel, sfera);
			base.RenderInternal(ref image, ref c, (float)widthPixel, (float)heightPixel);

		}

}
}
