using GraficaOpenTK.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficaOpenTK.Structure.Transformacion
{
    public class Escalar : ITransformacion
    {
        public Punto operacion { get; set; }
        public IGrafica objeto { get; set; }
        public Punto puntoFinal { get; set; }
        public long tTotal { get; set; }
        public long tInicio { get; set; }
        public string texto { get; set; }

        public Escalar(IGrafica obj, Punto final, long inicio, long tiempo, string n = "n")
        {
            this.objeto = obj;
            this.operacion = new Punto();
            this.puntoFinal = final;
            this.tTotal = tiempo;
            this.tInicio = inicio ;
            this.texto = n;
        }
        public void ejecutar(long control)
        {
            if (control >= this.tInicio && control <= (this.tInicio + this.tTotal))
            {
                long tiempoTranscurrido = control - this.tInicio;
                double _x = puntoFinal.X / this.tTotal;
                _x =+ _x;
                Console.WriteLine(_x);

                // Calcular la fracción del tiempo transcurrido respecto al tiempo total
                double fraccion = (double)tiempoTranscurrido / this.tTotal;

                // Definir la escala inicial como 1.0 (sin escala)
                double escalaInicial = 1.0;

                // Calcular el incremento en función de la fracción de tiempo
                // Esto incrementará progresivamente desde 1.0 hasta el valor final de puntoFinal.X
                double nuevoX = escalaInicial + (this.puntoFinal.X - escalaInicial) * fraccion;
                double nuevoY = escalaInicial + (this.puntoFinal.Y - escalaInicial) * fraccion;
                double nuevoZ = escalaInicial + (this.puntoFinal.Z - escalaInicial) * fraccion;



                //Console.WriteLine(this.texto + "--" + ((this.puntoFinal.X - 1.0) / this.tTotal));
                //double nuevoX = (this.puntoFinal.X - this.objeto.centro.X) / this.tTotal;
                //double nuevoY = (this.puntoFinal.Y - this.objeto.centro.Y) / this.tTotal;
                //double nuevoZ = (this.puntoFinal.Z - this.objeto.centro.Z) / this.tTotal;



                //Punto res = new Punto(nuevoX, nuevoY, nuevoZ);
                //Console.WriteLine(res);

                //this.objeto.escalar(res);
            }



            //// Escala inicial es 1.0 para cada eje
            //double escalaInicial = 1.0;

            //// Calcular la velocidad de escalación en cada eje (incremento por milisegundo)
            //double velocidadEscalarX = (this.puntoFinal.X - escalaInicial) / this.tTotal;
            //double velocidadEscalarY = (this.puntoFinal.Y - escalaInicial) / this.tTotal;
            //double velocidadEscalarZ = (this.puntoFinal.Z - escalaInicial) / this.tTotal;

            //// Multiplicar por un factor constante ajustable (según la escala que necesites)
            //double nuevaEscalaX = escalaInicial + velocidadEscalarX * 10;
            //double nuevaEscalaY = escalaInicial + velocidadEscalarY * 10;
            //double nuevaEscalaZ = escalaInicial + velocidadEscalarZ * 10;

            //// Crear un nuevo Punto con los valores de escala
            //Punto nuevaEscala = new Punto(nuevaEscalaX, nuevaEscalaY, nuevaEscalaZ);

            //// Aplicar la escalación al objeto
            //this.objeto.escalar(nuevaEscala);
        }
        public void setOperacion(Punto operacion)
        {
            this.operacion = operacion;
        }
    }
}
