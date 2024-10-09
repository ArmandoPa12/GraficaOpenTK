using GraficaOpenTK.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GraficaOpenTK.Structure
{
    public class Escenario : IGrafica
    {
        public IDictionary<string, Objeto> listaObjetos { get; set; } = new Dictionary<string, Objeto>();
        public Punto centro { get; set; } = new Punto();
        public Punto CentroDependiente { get; set ; }

        public Escenario()
        {
            centro = new Punto();
            listaObjetos = new Dictionary<string, Objeto>();
        }

        public Escenario(Dictionary<string,Objeto> objetos)
        {
            centro = new Punto();
            listaObjetos = objetos;
        }
        public Escenario add(string key, Objeto objeto)
        {
            this.listaObjetos.Add(key, objeto);
            this.centro = this.CalcularCentroDeMasa();
            return this;
        }
        public void remove(string key)
        {
            listaObjetos.Remove(key);
        }
        public Objeto get(string key)
        {
            return listaObjetos[key];
        }
        //remove
        //get
        public void draw()
        {
            foreach (KeyValuePair<string, Objeto> kvp in listaObjetos)
            {
                kvp.Value.draw();
            }
        }

        public void setCentro(Punto centro)
        {
            this.centro = centro;
            foreach(KeyValuePair<string, Objeto> kvp in listaObjetos)
            {
                kvp.Value.setCentro(centro);
            }
        }
        public Objeto getObjeto(string key)
        {
            if (listaObjetos.ContainsKey(key))
            {
                return listaObjetos[key];
            }
            return null;
        }

        public void rotar(Punto angulo)
        {
            foreach (var item in listaObjetos)
            {
                item.Value.setCentro(this.centro);
                item.Value.rotar(angulo);
            }
        }

        public void escalar(double factor)
        {
            foreach (var item in listaObjetos)
            {
                item.Value.setCentro(this.centro);
                item.Value.escalar(factor);
            }
            this.centro = CalcularCentroDeMasa();
        }

        public void trasladar(Punto valor)
        {
            foreach (var item in listaObjetos)
            {
                item.Value.setCentro(this.centro);
                item.Value.trasladar(valor);
            }
            this.centro = CalcularCentroDeMasa();
        }

        public Punto CalcularCentroDeMasa()
        {
            if (listaObjetos.Count == 0)
                return new Punto(0, 0, 0);  // Si no hay objetos en el escenario, el centro es el origen.

            // Variables para sumar las coordenadas de los centros de masa de los objetos.
            double sumaX = 0;
            double sumaY = 0;
            double sumaZ = 0;

            // Iterar sobre todos los objetos en el escenario.
            foreach (var kvp in listaObjetos)
            {
                // Calcula el centro de masa de cada objeto.
                Punto centroObjeto = kvp.Value.CalcularCentroDeMasa();

                // Sumar las coordenadas de los centros de masa.
                sumaX += centroObjeto.X;
                sumaY += centroObjeto.Y;
                sumaZ += centroObjeto.Z;
            }

            // Promediar las coordenadas de los centros de masa de todos los objetos.
            int numObjetos = listaObjetos.Count;
            double promedioX = sumaX / numObjetos;
            double promedioY = sumaY / numObjetos;
            double promedioZ = sumaZ / numObjetos;

            // Retornar el centro de masa del Escenario.
            //return new Punto(promedioX, promedioY, promedioZ);
            return new Punto();
        }
    }
}
