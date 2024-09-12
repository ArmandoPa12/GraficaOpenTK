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

        public void trasladar(Punto tras)
        {
            this.X += tras.X;
            this.Y += tras.Y;
            this.Z += tras.Z;
        }

        public void escalar(double factor, Punto centro)
        {
            this.X = centro.X + (this.X - centro.X) * factor;
            this.Y = centro.Y + (this.Y - centro.Y) * factor;
            this.Z = centro.Z + (this.Z - centro.Z) * factor;
        }

        public void rotar(double anguloX, double anguloY, double anguloZ, Punto centro)
        {
            // Convertir ángulos a radianes
            double radX = Math.PI * anguloX / 180.0;
            double radY = Math.PI * anguloY / 180.0;
            double radZ = Math.PI * anguloZ / 180.0;

            double cosX = Math.Cos(radX);
            double sinX = Math.Sin(radX);
            double cosY = Math.Cos(radY);
            double sinY = Math.Sin(radY);
            double cosZ = Math.Cos(radZ);
            double sinZ = Math.Sin(radZ);

            // Trasladar el punto al origen respecto al centro
            double xTemp = this.X - centro.X;
            double yTemp = this.Y - centro.Y;
            double zTemp = this.Z - centro.Z;

            // Rotación alrededor del eje X
            double yRotX = yTemp * cosX - zTemp * sinX;
            double zRotX = yTemp * sinX + zTemp * cosX;

            // Rotación alrededor del eje Y
            double xRotY = xTemp * cosY + zRotX * sinY;
            double zRotY = -xTemp * sinY + zRotX * cosY;

            // Rotación alrededor del eje Z
            double xRotZ = xRotY * cosZ - yRotX * sinZ;
            double yRotZ = xRotY * sinZ + yRotX * cosZ;

            // Trasladar el punto de vuelta al centro
            this.X = centro.X + xRotZ;
            this.Y = centro.Y + yRotZ;
            this.Z = centro.Z + zRotY;
        }

        public override string ToString()
        {
            return $"[{X:F2}]-[{Y:F2}]-[{Z:F2}]";
        }


    }
}
