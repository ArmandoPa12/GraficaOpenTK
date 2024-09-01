using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficaOpenTK.Structure
{
    public class Objeto
    {
        public IDictionary<string, Poligono> listaPoligonos { get; set; } = new Dictionary<string,Poligono>();
        public Punto centro { get; set; } = new Punto();

        public Objeto()
        {
            centro = new Punto();
            listaPoligonos = new Dictionary<string, Poligono>();
        }
        public Objeto(Punto punto)
        {
            centro = punto;
            listaPoligonos = new Dictionary<string, Poligono>();
        }
        public Objeto(Punto punto, IDictionary<string,Poligono> lista)
        {
            centro = punto;
            listaPoligonos = lista;
        }
        
        public void setCentro(Punto newCentro)
        {
            centro = newCentro;
            foreach (KeyValuePair<string, Poligono> kvp in listaPoligonos)
            {
                kvp.Value.setCentro(centro);
            }
        }
        public void add(string key, Poligono poligono)
        {
            listaPoligonos.Add(key,poligono);
        }
        public void remove(string key)
        {
            listaPoligonos.Remove(key);
        }
        public Poligono getPoligono(string key)
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
    }
}
