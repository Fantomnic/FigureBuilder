using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigureBuilder
{
    public class Parallelogram : Figure
    {
        private double floor, height, angle;

        public Parallelogram(double side, double angle) // ромб
        {
            floor = side;
            this.angle = angle * Math.PI / 180; // в радианах
            height = side * Math.Sin(this.angle);
        }

        public Parallelogram(double floor, double height, double angle) : this(floor, angle)
        {
            this.height = height;
        }

        //public override double RotationAngle { get; set; }

        public override void CreateCoordinatesArrays(out List<double> x, out List<double> y)
        {
            x = new List<double>() { 0 };
            y = new List<double>() { 0 };

            x.Add(height / Math.Tan(angle));
            y.Add(height);

            x.Add(height / Math.Tan(angle) + floor);
            y.Add(height);

            x.Add(floor);
            y.Add(0);

            if (RotationAngle % 360 != 0)
            {
                RotateFigure(ref x, ref y);
            }

            x.Add(0);
            y.Add(0);
        }
    }
}
