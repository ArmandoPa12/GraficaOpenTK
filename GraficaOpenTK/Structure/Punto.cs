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
            return X.ToString() +"] ["+ Y.ToString() +"] ["+Z.ToString();

        }

        public Vector3 ToVector3()
        {
            return new Vector3((float)X, (float)Y, (float)Z);
        }
        public static Punto operator +(Punto a, Punto b)
        {
            return new Punto(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static Punto operator -(Punto a, Punto b)
        {
            return new Punto(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        public static Punto operator *(Punto a, double escalar)
        {
            return new Punto(a.X * escalar, a.Y * escalar, a.Z * escalar);
        }
        public static Punto operator *(Matrix4 matriz, Punto punto)
        {
            // Convertir el punto a coordenadas homogéneas (Vector4 con w = 1.0)
            Vector4 puntoHomogeneo = new Vector4((float)punto.X, (float)punto.Y, (float)punto.Z, 1.0f);

            // Multiplicar la matriz por el vector
            Vector4 resultado = Vector4.Transform(puntoHomogeneo, matriz);

            // Devolver el nuevo punto transformado
            return new Punto(resultado.X, resultado.Y, resultado.Z);
        }

        public Punto TransformarPorMatriz(Matrix4 matriz)
        {
            Vector4 puntoHomogeneo = new Vector4((float)X, (float)Y, (float)Z, 1.0f); // Punto en coordenadas homogéneas
            Vector4 resultado = Vector4.Transform(puntoHomogeneo, matriz);

            return new Punto(resultado.X, resultado.Y, resultado.Z); // Devuelve el nuevo punto transformado
        }
        public Punto Trasladar(Punto desplazamiento)
        {
            return this + desplazamiento;
        }
        public Punto Escalar(double factorX, double factorY, double factorZ)
        {
            return new Punto(X * factorX, Y * factorY, Z * factorZ);
        }
        public Punto Rotar(double anguloEnGrados)
        {
            double anguloEnRadianes = Math.PI * anguloEnGrados / 180.0;
            double cosA = Math.Cos(anguloEnRadianes);
            double sinA = Math.Sin(anguloEnRadianes);

            double nuevoX = X * cosA - Y * sinA;
            double nuevoY = X * sinA + Y * cosA;

            return new Punto(nuevoX, nuevoY, Z); // Rotación en el plano XY
        }
    }
}
