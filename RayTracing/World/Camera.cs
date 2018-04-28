
using RayTracing.Math;

namespace RayTracing.World
{
    class Camera
    {
        readonly Vector3 low_left_corner,horizontal,vertical,original;
        public Camera(Vector3 l,Vector3 h,Vector3 v,Vector3 o)
        {
            low_left_corner = l;
            horizontal = h;
            vertical = v;
            original = o;
        }
        public Ray CreateRay(float x, float y) => new Ray(original,low_left_corner + horizontal * x  + vertical * y);
    }
}
