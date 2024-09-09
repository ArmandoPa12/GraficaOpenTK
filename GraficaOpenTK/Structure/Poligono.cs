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
using GraficaOpenTK.Class;

namespace GraficaOpenTK.Structure
{
    public class Poligono : IGrafica
    {
        public Punto centro { get; set; } = new Punto();
        public List<Punto> listaPuntos { get; set; } = new List<Punto>();

        public Color color { get; set; } = new Color();
        public PrimitiveType primitiveType { get; set; } = new PrimitiveType();

        public Poligono() 
        {
            this.centro = new Punto();
            this.listaPuntos = new List<Punto>();
            this.color = Color.Red;
            this.primitiveType = PrimitiveType.LineLoop;
        }
        public Poligono(Punto centro)
        {
            this.centro = centro;
            this.listaPuntos = new List<Punto>();
            this.color = Color.Red;
            this.primitiveType = PrimitiveType.LineLoop;
        }
        public Poligono(List<Punto> lista ,Punto centro,Color color, PrimitiveType tipo = PrimitiveType.LineLoop)
        {
            this.centro = centro;
            this.color = color;
            this.primitiveType = tipo; 
            this.listaPuntos = lista;   
        }

        public Poligono add(Punto punto)
        {
            this.listaPuntos.Add(punto);
            this.centro = CalcularCentroDeMasa();

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
                GL.Vertex3(item.X, item.Y , item.Z);
            }
            GL.End();
            GL.Flush();
        }
        public void setCentro(Punto newCentro)
        {
            Punto currentCentro = this.CalcularCentroDeMasa();
            Console.WriteLine(currentCentro.ToString());
            Console.WriteLine(newCentro.ToString());

            Punto desplazamiento = newCentro - currentCentro;
            for (int i = 0; i < listaPuntos.Count; i++)
            {
                //listaPuntos[i] = listaPuntos[i] + desplazamiento;
                listaPuntos[i] = new Punto(
                    Math.Round(listaPuntos[i].X + desplazamiento.X, 10),
                    Math.Round(listaPuntos[i].Y + desplazamiento.Y, 10),
                    Math.Round(listaPuntos[i].Z + desplazamiento.Z, 10)
                );
            }
            this.centro = currentCentro;
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

            centroX = Math.Round(centroX, 10);
            centroY = Math.Round(centroY, 10);
            centroZ = Math.Round(centroZ, 10);

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

        public void trasladar(Punto desplazamiento)
        {
            // Recorre cada punto del polígono y aplica la traslación
            for (int i = 0; i < listaPuntos.Count; i++)
            {
                // Actualiza cada punto sumando el desplazamiento
                listaPuntos[i] = listaPuntos[i] + desplazamiento;
            }

            // También actualiza el centro del polígono
            this.centro = this.centro + desplazamiento;
        }

        public void escalar(Punto factor)
        {
            // Calcula el centro actual del polígono
            Punto centroActual = this.CalcularCentroDeMasa();

            // Recorre cada punto del polígono y aplica el escalado respecto al centro
            for (int i = 0; i < listaPuntos.Count; i++)
            {
                Punto p = listaPuntos[i];

                // Calcular la distancia desde el punto al centro
                double deltaX = p.X - centroActual.X;
                double deltaY = p.Y - centroActual.Y;
                double deltaZ = p.Z - centroActual.Z;

                // Escalar esa distancia
                deltaX *= factor.X;
                deltaY *= factor.Y;
                deltaZ *= factor.Z;

                // Actualizar la posición del punto escalado
                listaPuntos[i] = new Punto(centroActual.X + deltaX, centroActual.Y + deltaY, centroActual.Z + deltaZ);
            }
        }
        
        public void rotar(double anguloX, double anguloY, double anguloZ)
        {
            // Convertir los ángulos a radianes
            double radX = Serializar.GradosARadianes(anguloX);
            double radY = Serializar.GradosARadianes(anguloY);
            double radZ = Serializar.GradosARadianes(anguloZ);

            // Crear matrices de rotación para cada eje
            Matrix4 rotX = Matrix4.CreateRotationX((float)radX);
            Matrix4 rotY = Matrix4.CreateRotationY((float)radY);
            Matrix4 rotZ = Matrix4.CreateRotationZ((float)radZ);

            // Combinar las matrices de rotación (aplicar en orden Z -> Y -> X)
            Matrix4 matrizRotacion = rotZ * rotY * rotX;

            // Calcular el centro actual del polígono
            Punto centroActual = this.CalcularCentroDeMasa();

            // Recorre cada punto del polígono y aplica la rotación
            for (int i = 0; i < listaPuntos.Count; i++)
            {
                Punto p = listaPuntos[i];

                // Trasladar el punto al origen (relativo al centro del polígono)
                Punto puntoRelativo = p - centroActual;

                // Aplicar la rotación usando la sobrecarga del operador *
                Punto puntoRotado = matrizRotacion * puntoRelativo;

                // Trasladar el punto de vuelta a su posición relativa al centro
                listaPuntos[i] = puntoRotado + centroActual;
            }
        }


        public void seeCenter()
        {
            // Dibuja el eje X centrado en ini
            GL.Begin(PrimitiveType.Lines);
            GL.Color4(Color.Red);
            GL.Vertex3(centro.X - 0.1f, centro.Y, centro.Z); // Extremo negativo del eje X
            GL.Vertex3(centro.X + 0.1f, centro.Y, centro.Z); // Extremo positivo del eje X
            GL.End();

            // Dibuja el eje Y centrado en ini
            GL.Begin(PrimitiveType.Lines);
            GL.Color4(Color.Green);
            GL.Vertex3(centro.X, centro.Y - 0.1f, centro.Z); // Extremo negativo del eje Y
            GL.Vertex3(centro.X, centro.Y + 0.1f, centro.Z); // Extremo positivo del eje Y
            GL.End();

            // Dibuja el eje Z centrado en ini
            GL.Begin(PrimitiveType.Lines);
            GL.Color4(Color.Blue);
            GL.Vertex3(centro.X, centro.Y, centro.Z - 0.1f); // Extremo negativo del eje Z
            GL.Vertex3(centro.X, centro.Y, centro.Z + 0.1f); // Extremo positivo del eje Z
            GL.End();

            GL.Flush();
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
    }
}
