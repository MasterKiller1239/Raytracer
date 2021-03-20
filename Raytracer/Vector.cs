using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer
{
    public struct Vector
    {
        private float x;
        public float X
        {
            get { return x; }
            set { x = value; }
        }
        private float y;
        public float Y
        {
            get { return y; }
            set { y = value; }
        }
        private float z;
        public float Z
        {
            get { return z; }
            set { z = value; }
        }

        public Vector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vector(Vector p1, Vector p2)
        {
            this.x = p2.X - p1.X;
            this.y = p2.Y - p1.Y;
            this.z = p2.Z - p1.Z;
        }
        public Vector(Vector v)
        {
            this.x = v.X;
            this.y = v.Y;
            this.z = v.Z;
        }


        public static float Dot(Vector vector1, Vector vector2) { return vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z; }
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
        public Vector normalizeProduct()
        {
            Vector newV = new Vector(this.x, this.y, this.z);
            float n = this.length();
            if (n != 0)
            {
                newV.div(n);
                return newV;
            }
            else
                return newV;// throw new Exception("Couldn't normalize");
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
        public float dot(Vector v)
        {
            return (this.x * v.x + this.y * v.y + this.z * v.z);
        }
        public Vector cross(Vector v)
        {
            return new Vector(this.y * v.z - this.z * v.y, this.z * v.x - this.x * v.z,
            this.x * v.y - this.y * v.x);
        }
        public void negate()
        {
            this.x = -this.x;
            this.y = -this.y;
            this.z = -this.z;
        }
        public void add(Vector v)
        {
            this.x += v.X;
            this.y += v.Y;
            this.z += v.Z;
        }
        public void sub(Vector v)
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
        public static Vector operator *(float scalar, Vector right)
        {
            return new Vector(right.x * scalar, right.y * scalar, right.z * scalar);
        }
        public static Vector operator *(Vector left, float scalar)
        {
            return new Vector(left.x * scalar, left.y * scalar, left.z * scalar);
        }
        public static Vector operator *(Vector left, Vector right)
        {
            return new Vector(left.x * right.x, left.y * right.y, left.z * right.z);
        }
        public static Vector operator +(Vector left, Vector right)
        {
            return new Vector(left.x + right.x, left.y + right.y, left.z + right.z);
        }
        public static Vector operator -(Vector left, Vector right)
        {
            return new Vector(left.x - right.x, left.y - right.y, left.z - right.z);
        }
        public static Vector operator -(Vector left)
        {
            return new Vector(-left.x, -left.y, -left.z);
        }
        public static bool operator ==(Vector left, Vector right)
        {
            return (left.x == right.x && left.y == right.y && left.z == right.z);
        }
        public static bool operator !=(Vector left, Vector right)
        {
            return (left.x != right.x || left.y != right.y || left.z != right.z);
        }
        public static Vector operator /(Vector left, float scalar)
        {
            Vector vector = new Vector();
            // get the inverse of the scalar up front to avoid doing multiple divides later

            float inverse = 1.0f / scalar;
            vector.x = left.x * inverse;
            vector.y = left.y * inverse;
            vector.z = left.z * inverse;
            return vector;
        }
        #endregion Operators
        public Vector reflect(Vector normal)
        {
            return this - (2 * this.dot(normal) * normal);
        }
        public static Vector magProduct(Vector v, float f)
        {
            return new Vector(v.X * f, v.Y * f, v.Z * f);
        }
        public Vector toPoint()
        {
            Vector p = new Vector(this.X, this.Y, this.Z);
            return p;
        }
        public Vector lerp(Vector v, float t)
        {
            Vector vector = new Vector();
            vector.x = this.x + t * (v.x - this.x);
            vector.y = this.y + t * (v.y - this.y);
            vector.z = this.z + t * (v.z - this.z);
            return vector;
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }
    }
}

