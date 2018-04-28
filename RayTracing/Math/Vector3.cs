
namespace RayTracing.Math
{
    public class Vector3
    {
        public float x, y, z;
        public static Vector3 one=new Vector3(1,1,1);
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3()
        {
        }

        public override string ToString()
        {
            return "<" + x + "," + y + "," + z + ">";
        }

        public float Magnitude()=>Mathf.Sqrt(x * x + y * y + z * z);

        public Vector3 Normalized()
        {
            var magnitude = Magnitude();
            return new Vector3(x/magnitude,y/magnitude,z/magnitude);
        }
        public static Vector3 Normalize(Vector3 v)
        {
            var magnitude = v.Magnitude();
            return new Vector3(v.x / magnitude, v.y / magnitude, v.z / magnitude);
        }

        public static Vector3 operator +(Vector3 lhs, Vector3 rhs) => new Vector3
        {
            x = lhs.x + rhs.x,
            y = lhs.y + rhs.y,
            z = lhs.z + rhs.z
        };

        public static Vector3 operator *(Vector3 lhs, float v) => new Vector3
        {
            x = lhs.x * v,
            y = lhs.y * v,
            z = lhs.z * v
        };

        public static Vector3 operator /(Vector3 lhs, float v) => new Vector3
        {
            x = lhs.x / v,
            y = lhs.y / v,
            z = lhs.z / v
        };

        public static Vector3 operator -(Vector3 lhs, Vector3 rhs) => new Vector3
        {
            x = lhs.x - rhs.x,
            y = lhs.y - rhs.y,
            z = lhs.z - rhs.z
        };

        public static float Dot(Vector3 lhs, Vector3 rhs)=>lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
        
        public static Vector3 Cross(Vector3 lhs, Vector3 rhs) => new Vector3(lhs.y * rhs.z - lhs.z * rhs.y,
            lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);


    }
}
