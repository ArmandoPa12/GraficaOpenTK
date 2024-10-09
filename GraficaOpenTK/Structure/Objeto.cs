using GraficaOpenTK.Interfaces;
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

        public void escalar(double factor)
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
    }
}
