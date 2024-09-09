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
        
        public Objeto add(string key, Parte parte)
        {
            this.listaPartes.Add(key, parte);
            this.centro = CalcularCentro();
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
        public Punto getCentro()
        { 
            return centro; 
        }
        public void setCentro(Punto nuevo)
        {
            centro = nuevo;
        }
        public Punto CalcularCentro()
        {
            if (listaPartes.Count == 0)
                return new Punto(0, 0, 0);

            // Sumar los centros de todas las partes
            double sumaX = 0;
            double sumaY = 0;
            double sumaZ = 0;

            foreach (var kvp in listaPartes)
            {
                // Obtener el centro de cada parte
                Punto centroParte = kvp.Value.CalcularCentroDeMasa();

                // Sumar las coordenadas del centro
                sumaX += centroParte.X;
                sumaY += centroParte.Y;
                sumaZ += centroParte.Z;
            }

            // Promediar los centros de todas las partes
            int numPartes = listaPartes.Count;
            double promedioX = sumaX / numPartes;
            double promedioY = sumaY / numPartes;
            double promedioZ = sumaZ / numPartes;

            // Devolver el centro global del objeto
            return new Punto(promedioX, promedioY, promedioZ);
        }

        public void draw()
        {
            foreach (KeyValuePair<string, Parte> kvp in listaPartes)
            {
                kvp.Value.draw();
            }
        }


        public void escalar(Punto factor)
        {
            throw new NotImplementedException();
        }

        public void trasladar(Punto desplazamiento)
        {
            foreach (var kvp in listaPartes)
            {
                kvp.Value.trasladar(desplazamiento);
            }

            // Actualiza el centro global del objeto
            this.centro = this.centro + desplazamiento;
        }
        public void TrasladarParte(string key, Punto desplazamiento)
        {
            // Verifica si la parte existe en el diccionario
            if (listaPartes.ContainsKey(key))
            {
                // Aplica la traslación solo a la parte especificada
                listaPartes[key].trasladar(desplazamiento);
            }
            else
            {
                Console.WriteLine($"La parte con la clave '{key}' no existe en el objeto.");
            }
        }


        public void rotar(double aX, double aY, double aZ)
        {
            throw new NotImplementedException();
        }
    }
}
