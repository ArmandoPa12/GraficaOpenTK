using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Diagnostics.CodeAnalysis;
using GraficaOpenTK.Interfaces;

namespace GraficaOpenTK.Structure
{
    public class Poligono : IGrafica
    {
        public Punto centro { get; set; } = new Punto();
        public List<Punto> listaPuntos { get; set; } = new List<Punto>();

        public Color color { get; set; } = new Color();
        public PrimitiveType primitiveType { get; set; } = new PrimitiveType();
        public Punto CentroDependiente { get; set; } 

        public Poligono() 
        {
            this.centro = new Punto();
            this.listaPuntos = new List<Punto>();
            this.color = Color.Red;
            this.primitiveType = PrimitiveType.LineLoop;
            this.CentroDependiente = new Punto();
        }
        public Poligono(Punto centro)
        {
            this.centro = centro;
            this.listaPuntos = new List<Punto>();
            this.color = Color.Red;
            this.primitiveType = PrimitiveType.LineLoop;
            this.CentroDependiente = new Punto();
        }
        public Poligono(List<Punto> lista ,Punto centro,Color color, PrimitiveType tipo = PrimitiveType.LineLoop)
        {
            this.centro = centro;
            this.color = color;
            this.primitiveType = tipo; 
            this.listaPuntos = lista;
            this.CentroDependiente = new Punto();
        }

        public Poligono add(Punto punto)
        {
            this.listaPuntos.Add(punto);
            this.centro = this.CalcularCentroDeMasa();  
            return this; 
        }
        public void remove(Punto punto)
        {
            this.listaPuntos.Remove(punto);
        }
        public Punto getPunto(int x)
        {
            return listaPuntos[x];
        }
        

        public void draw()
        {
            GL.Color4(this.color);
            GL.Begin(this.primitiveType);
            foreach (var item in listaPuntos)
            {   
                //GL.Vertex3(item.X + centro.X, item.Y + centro.Y, item.Z + centro.Z);
                GL.Vertex3(item.X, item.Y, item.Z);
            }
            GL.End();
            GL.Flush();
        }
        public void setCentro(Punto newCentro)
        {
            centro = newCentro;
        }
        public Punto getCentro()
        {
            return this.CalcularCentroDeMasa();
        }

        public Punto CalcularCentroDeMasa()
        {
            if (listaPuntos.Count == 0)
                return new Punto(0, 0, 0);

            double minX = listaPuntos.Min(p => p.X);
            double maxX = listaPuntos.Max(p => p.X);

            double minY = listaPuntos.Min(p => p.Y);
            double maxY = listaPuntos.Max(p => p.Y);

            double minZ = listaPuntos.Min(p => p.Z);
            double maxZ = listaPuntos.Max(p => p.Z);


            // Cálculo del centro en los ejes X y Y
            double centroX = (minX + maxX) / 2;
            double centroY = (minY + maxY) / 2;
            double centroZ = (minZ+ maxZ) / 2;

            centroX = centroX + this.CentroDependiente.X;
            centroY = centroY + this.CentroDependiente.Y;
            centroZ = centroZ + this.CentroDependiente.Z;

            return new Punto(centroX, centroY, centroZ);
        }
        public Poligono setTipo(PrimitiveType tipo)
        {
            primitiveType = tipo;
            return this;
        }   
        public Poligono setColor(Color color)
        {
            this.color = color; 
            return this;
        }
        

        public void seeCenter()
        {
            // Tamaño del cuadrado para representar el centro de masa
            double size = 0.005;  // Ajusta este tamaño para que sea visible pero pequeño

            // Dibuja un pequeño cuadrado centrado en el centro de masa
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(Color.White);  // Color del centro de masa

            // Esquinas del cuadrado
            GL.Vertex3(centro.X - size, centro.Y - size, centro.Z);
            GL.Vertex3(centro.X + size, centro.Y - size, centro.Z);
            GL.Vertex3(centro.X + size, centro.Y + size, centro.Z);
            GL.Vertex3(centro.X - size, centro.Y + size, centro.Z);

            GL.End();

            // Dibuja líneas de referencia para los ejes
            GL.Begin(PrimitiveType.Lines);

            // Eje X en rojo
            GL.Color4(Color.Red);
            GL.Vertex3(centro.X - 0.05f, centro.Y, centro.Z);  // Extremo negativo del eje X
            GL.Vertex3(centro.X + 0.05f, centro.Y, centro.Z);  // Extremo positivo del eje X

            // Eje Y en verde
            GL.Color4(Color.Green);
            GL.Vertex3(centro.X, centro.Y - 0.05f, centro.Z);  // Extremo negativo del eje Y
            GL.Vertex3(centro.X, centro.Y + 0.05f, centro.Z);  // Extremo positivo del eje Y

            // Eje Z en azul
            GL.Color4(Color.Blue);
            GL.Vertex3(centro.X, centro.Y, centro.Z - 0.05f);  // Extremo negativo del eje Z
            GL.Vertex3(centro.X, centro.Y, centro.Z + 0.05f);  // Extremo positivo del eje Z

            GL.End();

            GL.Flush();  // Asegura que todas las operaciones de OpenGL se ejecuten
        }

        public static void cartesiano(double escala = 0.1f)
        {
            PrimitiveType tipo = PrimitiveType.LineLoop;
            double width = 0.02f;

            GL.Begin(tipo);
            GL.Color4(Color.Red);
            GL.Vertex3(4f, 0f, 0);
            GL.Vertex3(-4f, 0f, 0);
            GL.End();

            for (double i = -4f; i <= 4f; i += escala)
            {
                GL.Begin(PrimitiveType.Lines);
                GL.Color4(Color.Red);
                GL.Vertex3(i, -width, 0);
                GL.Vertex3(i, width, 0);
                GL.End();
            }


            GL.Begin(tipo);
            GL.Color4(Color.Green);
            GL.Vertex3(0f, 4f, 0);
            GL.Vertex3(0f, -4f, 0);
            GL.End();
            for (double i = -4f; i <= 4f; i += escala)
            {
                GL.Begin(PrimitiveType.Lines);
                GL.Color4(Color.Green);
                GL.Vertex3(-width, i, 0);
                GL.Vertex3(width, i, 0);
                GL.End();
            }


            GL.Begin(tipo);
            GL.Color4(Color.Blue);
            GL.Vertex3(0f, 0f, 4f);
            GL.Vertex3(0f, 0f, -4f);
            GL.End();
            for (double i = -4f; i <= 4f; i += escala)
            {
                GL.Begin(PrimitiveType.Lines);
                GL.Color4(Color.Blue);
                GL.Vertex3(0, i, -width);
                GL.Vertex3(0, i, width);
                GL.End();
            }
        }

        public void rotar(Punto angulo)
        {

            Punto centroMasa = this.centro;

            Matrix4 matrizX = Matrix4.CreateRotationX((float)MathHelper.RadiansToDegrees(angulo.X));
            Matrix4 matrizY = Matrix4.CreateRotationY((float)MathHelper.RadiansToDegrees(angulo.Y));
            Matrix4 matrizZ = Matrix4.CreateRotationZ((float)MathHelper.RadiansToDegrees(angulo.Z));

            Matrix4 rotacionTotal = matrizZ * matrizY * matrizX;

            foreach (var item in listaPuntos)
            {
                Vector4 punto = new Vector4((float)(item.X - centroMasa.X),(float)(item.Y - centroMasa.Y),(float)(item.Z - centroMasa.Z),
                    1.0f
                );

                Vector4 resultado = rotacionTotal * punto;

                item.X = resultado.X + centroMasa.X;
                item.Y = resultado.Y + centroMasa.Y;
                item.Z = resultado.Z + centroMasa.Z;
            }


        }

        public void escalar(double factor)
        {
            Matrix4 preparar = Matrix4.CreateScale((float)factor);
            Vector4 resultado;
            foreach (var item in listaPuntos)
            {
                Vector4 vector = new Vector4((float)item.X - (float)centro.X, (float)item.Y - (float)centro.Y, (float)item.Z - (float)centro.Z, 1);
                resultado = vector * preparar;
                item.X = resultado.X + centro.X;
                item.Y = resultado.Y + centro.Y;
                item.Z = resultado.Z + centro.Z;
            }
            this.centro = this.CalcularCentroDeMasa();

        }

        public void trasladar(Punto valor)
        {
            Matrix4 preparar = Matrix4.CreateTranslation((float)valor.X, (float)valor.Y, (float)valor.Z);

            Vector4 resultado;
            foreach (var item in listaPuntos)
            {
                Vector4 vector = new Vector4((float)item.X, (float)item.Y, (float)item.Z,1);
                resultado = vector * preparar;

                item.X = resultado.X; 
                item.Y = resultado.Y; 
                item.Z = resultado.Z;
            }
            this.centro = this.CalcularCentroDeMasa();
        }
    }
}
