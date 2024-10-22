using GraficaOpenTK.Interfaces;
using OpenTK.Graphics.ES11;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace GraficaOpenTK.Structure
{
    public class Parte : IGrafica
    {
        public Punto centro {  get; set; }
        public Dictionary<string, Poligono> listaPoligonos { get; set; }
        public Punto CentroDependiente { get; set ; }

        public Parte()
        {
            centro = new Punto();
            listaPoligonos = new Dictionary<string, Poligono>();
            this.CentroDependiente = new Punto();

        }
        public Parte(Punto centro)
        {
            this.centro = centro;
            listaPoligonos = new Dictionary<string, Poligono>();
            this.CentroDependiente = new Punto();

        }

        public Parte add(string key, Poligono value)
        {
            listaPoligonos.Add(key, value);
            this.centro = this.CalcularCentroDeMasa();
            return this;
        }
        public void remove(string key) 
        {
            listaPoligonos.Remove(key);
        }
        public Poligono get(string key) 
        {
            return listaPoligonos[key];
        }
 

        public void draw()
        {
            foreach (KeyValuePair<string, Poligono> kvp in listaPoligonos)
            {
                kvp.Value.draw();
            }
        }
        public string getCentro()
        {
            return centro.ToString();
        }

        public void setCentro(Punto centro)
        {
            foreach (KeyValuePair<string, Poligono> kvp in listaPoligonos)
            {
                kvp.Value.setCentro(centro);
            }
            this.centro = centro;
        }

        public void rotar(Punto angulo)
        {
            foreach (var item in listaPoligonos)
            {
                item.Value.setCentro(this.centro);
                item.Value.rotar(angulo);
            }
        }

        public void escalar(Punto factor)
        {
            foreach (var item in listaPoligonos)
            {
                item.Value.setCentro(this.centro);
                item.Value.escalar(factor);
            }
            this.centro = CalcularCentroDeMasa();

        }
        public void seeCenter()
        {
            double size = 0.005;  // Ajusta este tamaño para que sea visible pero pequeño

            //// Dibuja un pequeño cuadrado centrado en el centro de masa
            //GL.Begin(PrimitiveType.Quads);
            //GL.Color4(Color.White);  // Color del centro de masa

            //// Esquinas del cuadrado
            //GL.Vertex3(centro.X - size, centro.Y - size, centro.Z);
            //GL.Vertex3(centro.X + size, centro.Y - size, centro.Z);
            //GL.Vertex3(centro.X + size, centro.Y + size, centro.Z);
            //GL.Vertex3(centro.X - size, centro.Y + size, centro.Z);

            //GL.End();

            //// Dibuja líneas de referencia para los ejes
            //GL.Begin(PrimitiveType.Lines);

            //// Eje X en rojo
            //GL.Color4(Color.Red);
            //GL.Vertex3(centro.X - 0.05f, centro.Y, centro.Z);  // Extremo negativo del eje X
            //GL.Vertex3(centro.X + 0.05f, centro.Y, centro.Z);  // Extremo positivo del eje X

            //// Eje Y en verde
            //GL.Color4(Color.Green);
            //GL.Vertex3(centro.X, centro.Y - 0.05f, centro.Z);  // Extremo negativo del eje Y
            //GL.Vertex3(centro.X, centro.Y + 0.05f, centro.Z);  // Extremo positivo del eje Y

            //// Eje Z en azul
            //GL.Color4(Color.Blue);
            //GL.Vertex3(centro.X, centro.Y, centro.Z - 0.05f);  // Extremo negativo del eje Z
            //GL.Vertex3(centro.X, centro.Y, centro.Z + 0.05f);  // Extremo positivo del eje Z

            //GL.End();

            //GL.Flush();  // Asegura que todas las operaciones de OpenGL se ejecuten           
        }
        public void trasladar(Punto valor)
        {

            foreach (var item in listaPoligonos)
            {
                item.Value.setCentro(this.centro);
                item.Value.trasladar(valor);
            }
            this.centro = CalcularCentroDeMasa();

        }
        public Punto CalcularCentroDeMasa()
        {
            if (listaPoligonos.Count == 0)
                return new Punto(0, 0, 0);

            // Sumar los centros de masa de todos los polígonos
            double sumaX = 0;
            double sumaY = 0;
            double sumaZ = 0;

            // Iterar por cada polígono
            foreach (KeyValuePair<string, Poligono> kvp in listaPoligonos)
            {
                // Calcula el centro de masa de cada polígono
                Punto centroPoligono = kvp.Value.CalcularCentroDeMasa();

                // Sumar las coordenadas de los centros
                sumaX += centroPoligono.X;
                sumaY += centroPoligono.Y;
                sumaZ += centroPoligono.Z;
            }

            // Promediar los centros de masa de los polígonos
            int numPoligonos = listaPoligonos.Count;
            double promedioX = sumaX / numPoligonos;
            double promedioY = sumaY / numPoligonos;
            double promedioZ = sumaZ / numPoligonos;

            // Retornar el centro de masa de la Parte
            return new Punto(promedioX + this.CentroDependiente.X, promedioY + this.CentroDependiente.Y, promedioZ + this.CentroDependiente.Z);
        }

        public void setPrimitiveType(OpenTK.Graphics.OpenGL.PrimitiveType tipo)
        {
            foreach (var item in listaPoligonos)
            {
                item.Value.setPrimitiveType(tipo);
            }
        }
    }
}
