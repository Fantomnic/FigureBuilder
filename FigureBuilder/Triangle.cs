using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigureBuilder
{
    public class Triangle : Figure
    {
        private double side1, side2, angle1, angle2;

        public Triangle(double floor, double sideOrAngle, bool isRectangular = false) // прямоугольный или равнобедренный
        {
            if (isRectangular)
            {
                side1 = sideOrAngle;
                //side2 = Math.Sqrt(Math.Pow(floor, 2) + Math.Pow(side1, 2));
                angle1 = Math.PI / 2;
                angle2 = Math.Atan(side1 / floor);
            }
            else
            {
                angle1 = angle2 = sideOrAngle * Math.PI / 180;
                side1 = side2 = floor / (2 * Math.Cos(angle1));
            }  
        }

        public Triangle(double side1, double angle1, double angle2)
        {
            this.side1 = side1;
            //this.side2 = side2;
            this.angle1 = angle1 * Math.PI / 180;
            this.angle2 = angle2 * Math.PI / 180;
        }

        //public override double RotationAngle { get; set; }

        public override void CreateCoordinatesArrays(out List<double> x, out List<double> y)
        {
            x = new List<double>() { 0 };
            y = new List<double>() { 0 };

            x.Add(side1 * Math.Cos(angle1));
            y.Add(side1 * Math.Sin(angle1));

            x.Add(x.Last() + y.Last() / Math.Tan(angle2));
            //y.Add(y.Last() - side2 * Math.Sin(angle2));
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
