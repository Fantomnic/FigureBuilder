using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigureBuilder
{
    public class Trapeze : Figure
    {
        private double floor, height, angle1, angle2;

        public Trapeze(double floor, double height, double angle) // прямоугольная
        {
            this.floor = floor;
            this.height = height;
            angle1 = Math.PI / 2;
            angle2 = angle * Math.PI / 180;
        }

        public Trapeze(double floor, double height, double angle1, double angle2) : this(floor, height, angle2)
        {
            this.angle1 = angle1 * Math.PI / 180;
        }

        //public override double RotationAngle { get; set; }

        public override void CreateCoordinatesArrays(out List<double> x, out List<double> y)
        {
            x = new List<double>() { 0 };
            y = new List<double>() { 0 };

            //x.Add(angle1 == Math.PI / 2 ? 0 : height / Math.Tan(angle1));
            x.Add(height / Math.Tan(angle1));
            y.Add(height);

            x.Add(floor - height / Math.Tan(angle2));
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
