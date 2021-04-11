using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer
{
    public class Color
    {
        double r, g, b;
        public Color(double R, double G, double B)
        {
            SetRed(R);
            SetGreen(G);
            SetBlue(B);
        }
        public Color()
        {
            r = g = b = 0.0;
        }
        public void Set(double R, double G, double B)
        {
            //Console.WriteLine("Czerwony:" + R + "Zielony: " + G + "Niebieski " + B);
            SetRed(R);
            SetGreen(G);
            SetBlue(B);
        }
        public double xRed() { return r ; }
        public double xGreen() { return g ; }
        public double xBlue() { return b ; }
        public double Red()  { return r * 255; }
        public double Green()  { return g * 255; }
        public double Blue()  { return b * 255; }
        public int iRed() { return (int)(r * 255); }
        public int iGreen() { return (int)g * 255; }
        public int iBlue() { return (int)b * 255; }

        public void SetRed(double v)
        {
            if (v < 0) r = 0;
            else if (v > 1) r = 1;
            else r = v;
        }
        public void SetGreen(double v)
        {
            if (v < 0) g = 0;
            else if (v > 1) g = 1;
            else g = v;
        }
        public  void SetBlue(double v)
        {
            if (v < 0) b = 0;
            else if (v > 1) b = 1;
            else b = v;
        }
        public void Add(double R, double G, double B)
        {
            SetRed(Red() + R);
            SetGreen(Green() + G);
            SetBlue(Blue() + B);
        }

       public static  Color WeightedAverage(ref List<(Color, double)> colorsAndWeights)
{
	double r = 0, g = 0, b = 0;
        double weightSum = 0;
	foreach (var tuple in colorsAndWeights)
	{

                //tuple.Item1.Show();
                r = r + tuple.Item1.r * tuple.Item2;
                g = g + tuple.Item1.g * tuple.Item2;
		        b = b + tuple.Item1.b * tuple.Item2;

		weightSum += tuple.Item2;
	}

    weightSum = 1 / weightSum;
            Color baze = new Color(r * weightSum, g * weightSum, b * weightSum);
          //  baze.Show();
            return baze;
}
public void Subtract(double R, double G, double B)
        {
            SetRed(Red() - R);
            SetGreen(Green() - G);
            SetBlue(Blue() - B);
        }
        public static Color operator+(Color basic, Color li) 
        {
	        return new Color(basic.r + li.r, basic.g + li.g, basic.b + li.b);
        }
        public static Color operator -(Color basic, Color li) 
        {
	        return new Color(basic.r - li.r, basic.g - li.g, basic.b - li.b);
        }

        public static Color operator*(Color basic, Color li) 
        {
	        return new Color(basic.r* li.r, basic.g* li.g, basic.b* li.b);
        }
        public static Color operator /(Color basic, double num)
        {
            return new Color(basic.r / num, basic.g / num, basic.b / num);
        }
        public static Color operator *(Color basic, double num)
        {
            return new Color(basic.r * num, basic.g * num, basic.b * num);
        }
        public  Color Average(Color[] colors)
        {
	double r = 0, g = 0, b = 0;
        double count = 0;
            foreach (Color c in colors)
            {
                r = r + c.r;
		    g = g+ c.g;
		    b =b+ c.b;
		++count;
	}

    count = 1 / count;

	return new Color(r* count, g* count, b* count);
        }
        public void Show()
        {
            Console.WriteLine("Red:" + this.r + "Green:" + this.g + "Blue:" + this.b);
            }
        public Color GetRandom(double minimum)
        {
            Random rnd = new Random();
            return new Color(Color.NextRandomRange(minimum, 1), Color.NextRandomRange(minimum, 1), Color.NextRandomRange(minimum, 1));
        }

        public double Difference( Color second) 
        {
	return Math.Abs(second.Red() - Red()) + Math.Abs(second.Green() - Green()) + Math.Abs(second.Blue() - Blue());
        }
    public static double NextRandomRange(double minimum, double maximum)
        {
            Random rand = new Random();
            return rand.NextDouble() * (maximum - minimum) + minimum;
        }

        public static Color GetRandom(double minimum, double maximum)
        {
           
            Random rand = new Random();
            //return new Color(rand.NextDouble() * (maximum - minimum) + minimum, rand.NextDouble() * (maximum - minimum) + minimum, rand.NextDouble() * (maximum - minimum) + minimum);
            return new Color(rand.NextDouble() * (maximum - minimum) + minimum, rand.NextDouble() * (maximum - minimum) + minimum, rand.NextDouble() * (maximum - minimum) + minimum);
        }
    }
    }

