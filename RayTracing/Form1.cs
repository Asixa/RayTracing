using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using RayTracing.Math;
using RayTracing.World;
using RayTracing.World.Primitives;
using Random=RayTracing.Math.Random;

namespace RayTracing
{
    public partial class Form1 : Form
    {
        public static Form1 main;
        private readonly Bitmap buff;
        private readonly HitableList hitableList = new HitableList();
        private const int Samples = 1;

        private readonly System.Timers.Timer main_timer;
        private int seconds = 0;
        private int j, i;
        private int Width = 512, Height = 512;
        public Form1()
        {

            ClientSize = new Size(Width, Height);
            main = this;
            InitializeComponent();
            buff = new Bitmap(Width, Height);
            hitableList.list.Add(new Sphere(new Vector3(0, 0, -1), 0.5f));
            hitableList.list.Add(new Sphere(new Vector3(0, -100.5f, -1), 100f));
            main_timer = new System.Timers.Timer(1000)
            {
                AutoReset = true,
                Enabled = true
            };
            main_timer.Elapsed += (s, e) => BeginInvoke(new Action(() =>
            {
                seconds++;
                Text = "RTRenderer  " + Samples + " spp [" +
                       ( (i + j * Height * 100f) / (Width * Height)).ToString("#0.00") +"%] - " 
                       + seconds + "s";
            }));
            main_timer.Start();
            Start();
        }

        private async void Start()
        {
            var t = new Task<int>(Scan);
            t.Start();
            await t;
            CreateGraphics().DrawImage(buff, 0, 0);
            Text = "RTRenderer " + Samples + "spp - " + seconds + "s";
            main_timer.Stop();
        }

        private int Scan()
        {
            var low_left_corner = new Vector3(-1, -1, -1);
            var horizontal = new Vector3(2, 0, 0);
            var vertical = new Vector3(0, 2, 0);
            var original = new Vector3(0, 0, 0);
            var camera = new Camera(low_left_corner, horizontal, vertical, original);
            var recip_width = 1f / Width;
            var recip_height = 1f / Height;
            for (j = 0; j <Height; j++)
            for (i = 0; i < Width; i++)
            {
                var color = new Color32(0, 0, 0, 0);
                for (var s = 0; s < Samples; s++)
                    color += Diffusing(camera.CreateRay((i + Random.Range(0, 1f)) * recip_width,
                        (j + Random.Range(0, 1f)) * recip_height), hitableList);
                color /= Samples;
                color *= 1f;
                buff.SetPixel(i, Height - j - 1, color.ToSystemColor());
            }
            return 0;
        }
        private Color32 Antialiasing(Ray ray, HitableList hitableList)
        {
            var record = new HitRecord();
            if (hitableList.Hit(ray, 0f, float.MaxValue, ref record))
                return 0.5f * new Color32(record.normal.x + 1, record.normal.y + 1, record.normal.z + 1, 2f);
            var t = 0.5f * ray.normalDirection.y + 1f;
            return (1 - t) * new Color32(1, 1, 1) + t * new Color32(0.5f, 0.7f, 1);
        }

        private Color32 Diffusing(Ray ray, HitableList hitableList)
        {
            Vector3 GetRandomPIU()
            {
                var p = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)) * 2 - Vector3.one;
                return p.Normalized() * Random.Range(0f, 1f);
            }
            var record = new HitRecord();
            if (hitableList.Hit(ray, 0.0001f, float.MaxValue, ref record))
                return 0.9f * Diffusing(new Ray(record.p, record.p + record.normal + GetRandomPIU() - record.p),hitableList);
            var t = 0.5f * ray.normalDirection.y + 1f;
            return (1 - t) * new Color32(1, 1, 1) + t * new Color32(0.5f, 0.7f, 1);
        }
    }
}
