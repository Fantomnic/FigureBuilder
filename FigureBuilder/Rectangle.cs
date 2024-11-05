using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigureBuilder
{
    public class Rectangle : Figure
    {
        private double length, height;

        public Rectangle(double length) // квадрат
        {
            this.length = height = length;
        }

        public Rectangle(double length, double height)
        {
            this.length = length;
            this.height = height;
        }

        //public override double RotationAngle { get; set; }

        public override void CreateCoordinatesArrays(out List<double> x, out List<double> y)
        {
            x = new List<double>() { 0 };
            y = new List<double>() { 0 };

            x.Add(0);
            y.Add(height);

            x.Add(length);
            y.Add(height);

            x.Add(length);
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
