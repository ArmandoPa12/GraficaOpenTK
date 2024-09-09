using GraficaOpenTK.Class;
using GraficaOpenTK.Interfaces;
using OpenTK;
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
            foreach (KeyValuePair<string, Poligono> kvp in listaPoligonos)
            {
                kvp.Value.draw();
            }
        }
        public string getCentro()
        {
            return centro.ToString();
        }

        public void setCentro(Punto nuevoCentro)
        {
            // Calcula el desplazamiento entre el nuevo centro y el centro actual de la Parte
            Punto desplazamiento = nuevoCentro - this.centro;

            // Aplica ese desplazamiento a cada polígono para mantener su posición relativa
            foreach (KeyValuePair<string, Poligono> kvp in listaPoligonos)
            {
                // En lugar de simplemente setear el centro del polígono, trasladas el polígono
                kvp.Value.trasladar(desplazamiento);
            }

            // Actualiza el centro de la Parte
            this.centro = nuevoCentro;
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



        public void trasladar(Punto desplazamiento)
        {
            foreach (KeyValuePair<string, Poligono> kvp in listaPoligonos)
            {
                // Trasladar cada polígono por el mismo desplazamiento
                kvp.Value.trasladar(desplazamiento);
            }

            // También actualizar el centro de la Parte
            this.centro = this.centro + desplazamiento;
        }

        public void escalar(Punto factor)
        {
            // Recorre cada polígono de la Parte
            foreach (KeyValuePair<string, Poligono> kvp in listaPoligonos)
            {
                Poligono poligono = kvp.Value;

                // Calcula la posición relativa del polígono al centro de la Parte
                Punto centroPoligono = poligono.CalcularCentroDeMasa();
                Punto distanciaAlCentro = centroPoligono - this.centro;

                // Escala la distancia al centro de la Parte
                Punto nuevaDistancia = new Punto(
                    distanciaAlCentro.X * factor.X,
                    distanciaAlCentro.Y * factor.Y,
                    distanciaAlCentro.Z * factor.Z
                );

                // Calcula el desplazamiento que necesita el polígono
                Punto desplazamiento = nuevaDistancia - distanciaAlCentro;

                // Traslada el polígono basado en el escalado
                poligono.trasladar(desplazamiento);

                // Ahora escala el propio polígono respecto a su propio centro
                poligono.escalar(new Punto(factor.X, factor.Y, factor.Z));
                

            }
        }

        public void rotar(double anguloX, double anguloY, double anguloZ)
        {
            // Recorre cada polígono de la Parte
            foreach (KeyValuePair<string, Poligono> kvp in listaPoligonos)
            {
                Poligono poligono = kvp.Value;

                // Calcula la posición relativa del polígono al centro de la Parte
                Punto centroPoligono = poligono.CalcularCentroDeMasa();
                Punto distanciaAlCentro = centroPoligono - this.centro;

                // Crear las matrices de rotación para los tres ejes
                Matrix4 rotacionX = Matrix4.CreateRotationX((float)Serializar.GradosARadianes(anguloX));
                Matrix4 rotacionY = Matrix4.CreateRotationY((float)Serializar.GradosARadianes(anguloY));
                Matrix4 rotacionZ = Matrix4.CreateRotationZ((float)Serializar.GradosARadianes(anguloZ));

                // Combina las matrices de rotación en el orden Z -> Y -> X
                Matrix4 matrizRotacion = rotacionZ * rotacionY * rotacionX;

                // Aplica la rotación a la distancia del polígono al centro de la Parte
                Punto nuevaPosicionPoligono = matrizRotacion * distanciaAlCentro;

                // Calcula el desplazamiento que necesita el polígono
                Punto desplazamiento = nuevaPosicionPoligono - distanciaAlCentro;

                // Traslada el polígono basado en la nueva posición tras la rotación
                poligono.trasladar(desplazamiento);

                // Rota el polígono sobre sí mismo (opcional si quieres rotar el polígono sobre su propio eje)
                poligono.rotar(anguloX, anguloY, anguloZ);
            }
        }



    }
}
