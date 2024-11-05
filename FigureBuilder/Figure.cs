using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigureBuilder
{
    public abstract class Figure
    {
        protected Figure() 
        {
            RotationAngle = 0;
        }

        // угол поворота фигуры (в радианах)
        public double RotationAngle { get; set; }

        public abstract void CreateCoordinatesArrays(out List<double> x, out List<double> y);

        protected void RotateFigure(ref List<double> x, ref List<double> y)
        {
            if (x.Count != y.Count)
            {
                throw new Exception("Размерности входных массивов координат должны совпадать!");
            }

            double oldX, oldY;
            for (int i = 0; i < x.Count; i++)
            {
                oldX = x[i];
                oldY = y[i];
                x[i] = oldX * Math.Cos(RotationAngle) - oldY * Math.Sin(RotationAngle);
                y[i] = oldX * Math.Sin(RotationAngle) + oldY * Math.Cos(RotationAngle);
            }
        }
    }
}
