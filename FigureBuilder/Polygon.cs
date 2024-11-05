using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigureBuilder
{
    public class Polygon : Figure
    {
        private int n;
        private double side;

        public Polygon(int n, double side)
        {
            this.n = n;
            this.side = side;
        }

        //public override double RotationAngle { get; set; }

        public override void CreateCoordinatesArrays(out List<double> x, out List<double> y)
        {
            x = new List<double>();
            y = new List<double>();

            for (int i = 0; i <= n; i++)
            {
                x.Add(side * Math.Cos(i * 2* Math.PI / n));
                y.Add(side * Math.Sin(i * 2 * Math.PI / n));
            }

            if (RotationAngle % 360 != 0)
            {
                RotateFigure(ref x, ref y);
            }
        }
    }
}
