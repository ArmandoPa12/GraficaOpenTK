using GraficaOpenTK.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficaOpenTK.Structure
{
    public class Objeto : IGrafica
    {
        public IDictionary<string, Parte> listaPartes { get; set; } = new Dictionary<string, Parte>();
        public Punto centro { get; set; } = new Punto();

        public Objeto()
        {
            centro = new Punto();
            listaPartes = new Dictionary<string, Parte>();
        }
        public Objeto(Punto punto)
        {
            centro = punto;
            listaPartes = new Dictionary<string, Parte>();
        }
        public Objeto(Punto punto, IDictionary<string, Parte> lista)
        {
            centro = punto;
            listaPartes = lista;
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
            return this;
        }
        public void remove(string key)
        {
            listaPartes.Remove(key);
        }
        public Parte getPoligono(string key)
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
    }
}
