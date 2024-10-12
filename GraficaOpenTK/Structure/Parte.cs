using GraficaOpenTK.Interfaces;
using OpenTK.Graphics.ES11;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
            foreach (var item in listaPoligonos)
            {
                item.Value.seeCenter();
            }            
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
