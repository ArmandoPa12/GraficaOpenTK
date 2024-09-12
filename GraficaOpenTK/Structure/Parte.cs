using GraficaOpenTK.Interfaces;
using System;
using System.Collections.Generic;
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

        public Parte()
        {
            centro = new Punto();
            listaPoligonos = new Dictionary<string, Poligono>();
        }
        public Parte(Punto centro)
        {
            this.centro = centro;
            listaPoligonos = new Dictionary<string, Poligono>();
        }

        public Parte add(string key, Poligono value)
        {
            listaPoligonos.Add(key, value);
            this.centro = CalcularCentroDeMasa();
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
            //Punto centroObjeto = this.CalcularCentroDeMasa();

            foreach (KeyValuePair<string, Poligono> kvp in listaPoligonos)
            {
                kvp.Value.setCentro(this.centro);
                kvp.Value.draw();

            }
        }

        public void rotar(Punto angulo)
        {
            //Punto centroObjeto = this.CalcularCentroDeMasa();
            foreach (var item in listaPoligonos)
            {
                //item.Value.setCentro(centro);
                item.Value.rotar(angulo);
            }
        }

        public void escalar(double factor)
        {
            //Punto centroObjeto = this.CalcularCentroDeMasa();
            foreach (var item in listaPoligonos)
            {
                item.Value.setCentro(centro);
                item.Value.escalar(factor);
            }
        }
        public void trasladar(Punto valor)
        {
            //Punto centroObjeto = this.CalcularCentroDeMasa();
            foreach (var item in listaPoligonos)
            {
                //item.Value.setCentro(centroObjeto);
                item.Value.trasladar(valor);
            }
        }

        public Punto getCentro()
        {
            return centro;
        }

        public void setCentro(Punto centro)
        {
            this.centro = centro;
            foreach (KeyValuePair<string, Poligono> kvp in listaPoligonos)
            {
                kvp.Value.setCentro(centro);
            }
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
            return new Punto(promedioX, promedioY, promedioZ);
        }

        
    }
}
