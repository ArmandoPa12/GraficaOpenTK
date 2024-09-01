using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
