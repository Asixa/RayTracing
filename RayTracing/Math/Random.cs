using System;
namespace RayTracing.Math
{
    public static class Random
    {
        public static float Range(float f, float t)
        {
            var random= new System.Random(Guid.NewGuid().GetHashCode());
            return random.Next((int)(f * 10000), (int)(t * 10000)) / 10000f;
        }
    }
}
