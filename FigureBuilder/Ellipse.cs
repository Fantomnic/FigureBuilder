using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigureBuilder
{
    public class Ellipse : Figure
    {
        private double radius1, radius2;

        public Ellipse(double radius) // круг
        {
            radius1 = radius2 = radius;
        }
        public Ellipse(double radius1, double radius2)
        {
            this.radius1 = radius1;
            this.radius2 = radius2;
        }

        //public override double RotationAngle { get; set; }

        public override void CreateCoordinatesArrays(out List<double> x, out List<double> y)
        {
            x = new List<double>();
            y = new List<double>();

            for (int i = 0; i < 361; i++)
            {
                x.Add(radius1 * Math.Sin(i * Math.PI / 180));
                y.Add(radius2 * Math.Cos(i * Math.PI / 180));
            }

            if (RotationAngle % 360 != 0)
            {
                RotateFigure(ref x, ref y);
            }
        }
    }
}
