using RayTracing.Math;

namespace RayTracing
{
    public class Ray
    {
        public readonly Vector3 original;
        public readonly Vector3 direction;
        public readonly Vector3 normalDirection;

        public Ray(Vector3 o, Vector3 d)
        {
            original = o;
            direction = d;
            normalDirection = d.Normalized();
        }

        public Vector3 GetPoint(float t)=>original +  direction*t;
    }
}
