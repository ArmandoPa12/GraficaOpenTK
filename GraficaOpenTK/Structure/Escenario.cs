using GraficaOpenTK.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        #region acciones basicas
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

        #endregion

        public void draw()
        {
            //Punto centroObjeto = this.CalcularCentroDeMasa();

            foreach (KeyValuePair<string, Objeto> kvp in listaObjetos)
            {
                kvp.Value.setCentro(this.centro);
                kvp.Value.draw();
            }
        }

        public void setCentro(Punto centro)
        {
            this.centro = centro;
        }
        public Punto getCentro()
        { 
            return centro; 
        }

        #region transformaciones

        public void rotar(Punto angulo, string claveObj = null, string clavePart = null)
        {
            if (claveObj == null && clavePart == null)
            {
                foreach (KeyValuePair<string, Objeto> kvp in listaObjetos)
                {
                    kvp.Value.setCentro(centro);
                    kvp.Value.rotar(angulo);
                }
            }
            else if (claveObj != null && clavePart == null)
            {
                if (listaObjetos.ContainsKey(claveObj))
                {
                    Objeto objeto = listaObjetos[claveObj];
                    objeto.setCentro(objeto.CalcularCentroDeMasa());
                    objeto.rotar(angulo);
                }
            }
            else if (claveObj != null && clavePart != null)
            {
                if (listaObjetos.ContainsKey(claveObj))
                {
                    Objeto objeto = listaObjetos[claveObj];
                    if (objeto.listaPartes.ContainsKey(clavePart))
                    {
                        Parte parte = objeto.listaPartes[clavePart];
                        parte.setCentro(parte.CalcularCentroDeMasa());
                        parte.rotar(angulo);
                    }
                }
            }
            
        }

        public void escalar(double factor, string claveObj = null, string clavePart = null)
        {
            if (claveObj == null && clavePart == null)
            {
                foreach (KeyValuePair<string, Objeto> kvp in listaObjetos)
                {
                    kvp.Value.setCentro(centro);
                    kvp.Value.escalar(factor);
                }
            }else if (claveObj != null && clavePart == null)
            {
                if (listaObjetos.ContainsKey(claveObj)){                    
                    Objeto objeto = listaObjetos[claveObj];
                    objeto.setCentro(objeto.CalcularCentroDeMasa());
                    objeto.escalar(factor);
                }
            }else if (claveObj != null && clavePart != null)
            {
                if (listaObjetos.ContainsKey(claveObj))
                {
                    Objeto objeto = listaObjetos[claveObj];
                    if (objeto.listaPartes.ContainsKey(clavePart))
                    {
                        Parte parte = objeto.listaPartes[clavePart];
                        parte.setCentro(parte.CalcularCentroDeMasa());
                        parte.escalar(factor);
                    }
                }
            }
            
        }

        public void trasladar(Punto valor, string claveObj = null, string clavePart = null)
        {
            if (claveObj == null && clavePart == null)
            {
                foreach (KeyValuePair<string, Objeto> kvp in listaObjetos)
                {
                    kvp.Value.trasladar(valor);
                }
            }
            else if (claveObj != null && clavePart == null)
            {
                if (listaObjetos.ContainsKey(claveObj))
                {
                    Objeto objeto = listaObjetos[claveObj];
                    objeto.setCentro(objeto.CalcularCentroDeMasa());
                    objeto.trasladar(valor);
                }
            }
            else if (claveObj != null && clavePart != null)
            {
                if (listaObjetos.ContainsKey(claveObj))
                {
                    Objeto objeto = listaObjetos[claveObj];
                    if (objeto.listaPartes.ContainsKey(clavePart))
                    {
                        Parte parte = objeto.listaPartes[clavePart];
                        parte.setCentro(parte.CalcularCentroDeMasa());
                        parte.trasladar(valor);
                    }
                }
            }
            
        }
        
        #endregion

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
            return new Punto(promedioX, promedioY, promedioZ);
        }






        public void verObjeto()
        {
            string a ="";
            foreach (KeyValuePair<string, Objeto> kvp in listaObjetos)
            {
                a += kvp.Key + "\n";
            }
            Console.WriteLine(a);
        }
        public void verParte(string nombreObjeto)
        {
            string a = "";
            foreach (KeyValuePair<string, Objeto> kvp in listaObjetos)
            {
                //a += kvp.Key + "\n";
                if (kvp.Key == nombreObjeto)
                { a = Objeto.verPartes(kvp.Value); }
            }
            Console.WriteLine(a);
        }

    }
}
