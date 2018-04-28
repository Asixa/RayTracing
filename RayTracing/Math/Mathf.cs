using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing.Math
{
    public class Mathf
    {
        public static float Sqrt(float v) => (float) System.Math.Sqrt(Convert.ToDouble(v));
        public static float Range(float v, float min, float max)=>(v <= min)?min: v >= max ? max : v;
    }
}
