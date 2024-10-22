using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace GraficaOpenTK.Structure
{
    public class Punto
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Punto()
        {
            X = Y = Z = 0.0f;
        }
        public Punto(double v)
        {
            X = Y = Z = v;
        }
        public Punto(double X, double Y, double Z)
        {
            this.X = X; this.Y = Y; this.Z = Z;
        }

        public Punto(Punto p)
        {
            X = p.X; Y = p.Y; Z = p.Z;
        }

        public override string ToString()
        {
            //return X.ToString() + "-" + Y.ToString() + "-" + Z.ToString();

            return $"X{X:F2}|Y{Y:F2}|Z{Z:F2}|";

        }
        public static Punto operator +(Punto p1, Punto p2)
        {
            return new Punto(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        }


    }
}
