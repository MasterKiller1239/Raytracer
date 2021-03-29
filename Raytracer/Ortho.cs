using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Raytracer
{
   

	class Ortho : Camera
    {

		protected double height = 4;

	
		public Ortho()
		{
			this.position = new Vector3(0, 0, 0);
			this.target = new Vector3(0, 0, 1);
			this.nearPlane = 1;
			this.farPlane = 1000;
			this.up = new Vector3(0, 1, 0);
		}
		public Ortho(Vector3 position, Vector3 target)
		{
			this.position = position;
			this.target = target;
			this.nearPlane = 1;
			this.farPlane = 1000;
			this.up = new Vector3(0, 1, 0);
		}
		/// <summary>
		/// Camera Setup
		/// </summary>
		/// <param name="image">Bitmap</param>
		/// <param name="sfera">Primitives List</param>
		public override void RenderTo(Bitmap image, List<Sphere> sfera)
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

			base.RenderInternal(image, c, (float)widthPixel, (float)heightPixel, sfera);


		}

}
}
