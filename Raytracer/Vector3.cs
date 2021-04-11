using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer
{
    /// <summary>
    /// Implementation of 3D vector (or point)
    /// </summary>
    public class Vector3
    {
        public float x;
        public float X
        {
            get { return x; }
            set { x = value; }
        }
        public float y;
        public float Y
        {
            get { return y; }
            set { y = value; }
        }
        public float z;
        public float Z
        {
            get { return z; }
            set { z = value; }
        }
        /// <summary>
        /// Constructor for Vector3
        /// </summary>
        /// <param name="x">Float value assigned to x</param>
        /// <param name="y">Float value assigned to y</param>
        /// <param name="z">Float value assigned to z</param>

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vector3(Vector3 p1, Vector3 p2)
        {
            this.x = p2.X - p1.X;
            this.y = p2.Y - p1.Y;
            this.z = p2.Z - p1.Z;
        }
        public Vector3(Vector3 v)
        {
            this.x = v.X;
            this.y = v.Y;
            this.z = v.Z;
        }
        /// <summary>
        /// Default constructor for Vector3. All coordinates are zeros.
        /// </summary>
        public Vector3()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
        }
        /// <summary>
        /// Constructor for Vector3 that makes all coordinates the same
        /// </summary>
        /// <param name="newVal">Value that will be assigned to all three coordinates</param>

        public Vector3(float newVal)
        {
            x = y = z = newVal;
        }
        public static float Dot(Vector3 vector1, Vector3 vector2) { return vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z; }
        public override string ToString()
        {
            return "Vector(" + x.ToString() + "," + y.ToString() + "," + z.ToString() +
            ")";
        }
        public void normalize()
        {
            float n = this.length();
            if (n != 0)
            {
                this.div(n);
            }
            //else
        }
        public Vector3 normalizeProduct()
        {
            Vector3 newV = new Vector3(this.x, this.y, this.z);
            float n = this.length();
            if (n != 0)
            {
                newV.div(n);
                return newV;
            }
            else
                return new Vector3(newV);
        }
        public float length()
        {
            return (float)Math.Sqrt(Math.Pow(this.x, 2) + Math.Pow(this.y, 2) + Math.Pow
            (this.z, 2));
        }
        public float lengthSquared()
        {
            return (float)(Math.Pow(this.x, 2) + Math.Pow(this.y, 2) + Math.Pow(this.z,
            2));
        }
        public float dot(Vector3 v)
        {
            return (this.x * v.x + this.y * v.y + this.z * v.z);
        }
        public Vector3 cross(Vector3 v)
        {
            return new Vector3(this.y * v.z - this.z * v.y, this.z * v.x - this.x * v.z,
            this.x * v.y - this.y * v.x);
        }
        public void negate()
        {
            this.x = -this.x;
            this.y = -this.y;
            this.z = -this.z;
        }
        public void add(Vector3 v)
        {
            this.x += v.X;
            this.y += v.Y;
            this.z += v.Z;
        }
        public void sub(Vector3 v)
        {
            this.x -= v.X;
            this.y -= v.Y;
            this.z -= v.Z;
        }
        public void div(float f)
        {
            if (f != 0)
            {
                this.x /= f;
                this.y /= f;
                this.z /= f;
            }
            else
                throw new Exception("Cant divide by 0");
        }
        public void mag(float f)
        {
            this.x *= f;
            this.y *= f;
            this.z *= f;
        }
        #region Operators
        public static Vector3 operator *(float scalar, Vector3 right)
        {
            return new Vector3(right.x * scalar, right.y * scalar, right.z * scalar);
        }
        public static Vector3 operator *(Vector3 left, float scalar)
        {
            return new Vector3(left.x * scalar, left.y * scalar, left.z * scalar);
        }
        public static Vector3 operator *(double scalar, Vector3 right)
        {
            return new Vector3(right.x * (float)scalar, right.y * (float)scalar, right.z * (float)scalar);
        }
        public static Vector3 operator *(Vector3 left, double scalar)
        {
            return new Vector3(left.x * (float)scalar, left.y * (float)scalar, left.z * (float)scalar);
        }
        public static Vector3 operator *(Vector3 left, Vector3 right)
        {
            return new Vector3(left.x * right.x, left.y * right.y, left.z * right.z);
        }
        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(left.x + right.x, left.y + right.y, left.z + right.z);
        }
        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(left.x - right.x, left.y - right.y, left.z - right.z);
        }
        public static Vector3 operator -(Vector3 left)
        {
            return new Vector3(-left.x, -left.y, -left.z);
        }
        public static bool operator ==(Vector3 left, Vector3 right)
        {
            return (left.x == right.x && left.y == right.y && left.z == right.z);
        }
        public static bool operator !=(Vector3 left, Vector3 right)
        {
            return (left.x != right.x || left.y != right.y || left.z != right.z);
        }
        public static Vector3 operator /(Vector3 left, float scalar)
        {
            Vector3 vector = new Vector3();
            // get the inverse of the scalar up front to avoid doing multiple divides later

            float inverse = 1.0f / scalar;
            vector.x = left.x * inverse;
            vector.y = left.y * inverse;
            vector.z = left.z * inverse;
            return vector;
        }
        #endregion Operators
        public Vector3 reflect(Vector3 normal)
        {
            return this - (2 * this.dot(normal) * normal);
        }
        public static Vector3 magProduct(Vector3 v, float f)
        {
            return new Vector3(v.X * f, v.Y * f, v.Z * f);
        }
        public Vector3 toPoint()
        {
            Vector3 p = new Vector3(this.X, this.Y, this.Z);
            return p;
        }
        public Vector3 lerp(Vector3 v, float t)
        {
            Vector3 vector = new Vector3();
            vector.x = this.x + t * (v.x - this.x);
            vector.y = this.y + t * (v.y - this.y);
            vector.z = this.z + t * (v.z - this.z);
            return vector;
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }
       public  bool isZero()
        {
            if (x==0 && y == 0 && z == 0)
            {
                return true;
            }
            return false;
        }
        public Vector3 Reflect(Vector3 normal) 
{
	return this - (normal* 2 * this.dot(normal));
}
}
}

