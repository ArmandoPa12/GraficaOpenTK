using GraficaOpenTK.Interfaces;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficaOpenTK.Structure
{
    public class Objeto : IGrafica
    {
        public IDictionary<string, Parte> listaPartes { get; set; } = new Dictionary<string, Parte>();
        public Punto centro { get; set; } = new Punto();
        public Punto CentroDependiente { get; set; }


        public Objeto()
        {
            centro = new Punto();
            listaPartes = new Dictionary<string, Parte>();
            this.CentroDependiente = new Punto();

        }
        public Objeto(Punto punto)
        {
            centro = punto;
            listaPartes = new Dictionary<string, Parte>();
            this.CentroDependiente = new Punto();

        }
        public Objeto(Punto punto, IDictionary<string, Parte> lista)
        {
            centro = punto;
            listaPartes = lista;
            this.CentroDependiente = new Punto();

        }

        public void setCentro(Punto newCentro)
        {
            centro = newCentro;
            foreach (KeyValuePair<string, Parte> kvp in listaPartes)
            {
                kvp.Value.setCentro(centro);
            }

        }
        public Objeto add(string key, Parte parte)
        {
            this.listaPartes.Add(key, parte);
            this.centro = this.CalcularCentroDeMasa();
            return this;
        }
        public void remove(string key)
        {
            listaPartes.Remove(key);
        }
        public Parte getParte(string key)
        {
            return listaPartes[key];
        }
        public void draw()
        {
            foreach (KeyValuePair<string, Parte> kvp in listaPartes)
            {
                kvp.Value.draw();
            }
        }

        public void rotar(Punto angulo)
        {
            foreach (var item in listaPartes)
            {
                item.Value.setCentro(this.centro);
                item.Value.rotar(angulo);
            }
        }

        public void escalar(Punto factor)
        {
            foreach (var item in listaPartes)
            {
                item.Value.setCentro(this.centro);
                item.Value.escalar(factor);
            }
            this.centro = CalcularCentroDeMasa();

        }

        public void trasladar(Punto valor)
        {
            foreach (var item in listaPartes)
            {
                item.Value.setCentro(this.centro);
                item.Value.trasladar(valor);
            }
            this.centro = CalcularCentroDeMasa();
        }

        public Punto CalcularCentroDeMasa()
        {
            if (listaPartes.Count == 0)
                return new Punto(0, 0, 0);  // Si no hay partes, el centro es el origen.

            // Variables para sumar las coordenadas de los centros de masa de las partes.
            double sumaX = 0;
            double sumaY = 0;
            double sumaZ = 0;

            // Iterar sobre todas las partes del objeto.
            foreach (var kvp in listaPartes)
            {
                // Calcula el centro de masa de cada parte.
                Punto centroParte = kvp.Value.CalcularCentroDeMasa();

                // Sumar las coordenadas de los centros de masa.
                sumaX += centroParte.X;
                sumaY += centroParte.Y;
                sumaZ += centroParte.Z;
            }

            // Promediar las coordenadas de los centros de masa de todas las partes.
            int numPartes = listaPartes.Count;
            double promedioX = sumaX / numPartes;
            double promedioY = sumaY / numPartes;
            double promedioZ = sumaZ / numPartes;

            // Retornar el centro de masa del Objeto.
            return new Punto(promedioX + this.CentroDependiente.X, promedioY + this.CentroDependiente.Y, promedioZ + this.CentroDependiente.Z);
        }

        public void seeCenter()
        {
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

        public void setPrimitiveType(PrimitiveType tipo)
        {
            foreach (var item in listaPartes)
            {
                item.Value.setPrimitiveType(tipo);
            }
        }
    }
}
