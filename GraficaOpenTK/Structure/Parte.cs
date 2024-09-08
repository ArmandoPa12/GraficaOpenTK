using GraficaOpenTK.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }
    }
}
