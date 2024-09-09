using GraficaOpenTK.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficaOpenTK.Structure
{
    public class Escenario : IGrafica
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
        public Escenario add(string key, Objeto objeto)
        {
            this.listaObjetos.Add(key, objeto);
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
            throw new NotImplementedException();
        }

        public void escalar(Punto factor)
        {
            throw new NotImplementedException();
        }

        public void trasladar(Punto desplazamiento)
        {
            throw new NotImplementedException();
        }

        public void rotar(double aX, double aY, double aZ)
        {
            throw new NotImplementedException();
        }
    }
}
