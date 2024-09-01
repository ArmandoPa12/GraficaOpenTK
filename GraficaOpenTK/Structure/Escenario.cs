using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficaOpenTK.Structure
{
    public class Escenario
    {
        public IDictionary<string, Objeto> listaObjetos { get; set; } = new Dictionary<string, Objeto>();
        public Punto centro { get; set; } = new Punto();
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
        public void add(string key, Objeto objeto)
        {
            listaObjetos.Add(key, objeto);
        }
        public void remove(string key)
        {
            listaObjetos.Remove(key);
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
    }
}
