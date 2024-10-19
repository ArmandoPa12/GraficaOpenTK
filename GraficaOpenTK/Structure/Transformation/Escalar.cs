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
        public Punto final { get; set; }
        public long tiempo { get; set; }
        public long tini { get; set; }
        public string n { get; set; }

        public Escalar(IGrafica obj, Punto final, long inicio, long tiempo, string n = "n")
        {
            this.objeto = obj;
            this.operacion = new Punto();
            this.final = final;
            this.tiempo = tiempo;
            this.tini = inicio ;
            this.n = n;
        }
        public void ejecutar(long control)
        {
            // Escala inicial es 1.0 para cada eje
            double escalaInicial = 1.0;

            // Calcular la velocidad de escalación en cada eje (incremento por milisegundo)
            double velocidadEscalarX = (this.final.X - escalaInicial) / this.tiempo;
            double velocidadEscalarY = (this.final.Y - escalaInicial) / this.tiempo;
            double velocidadEscalarZ = (this.final.Z - escalaInicial) / this.tiempo;

            // Multiplicar por un factor constante ajustable (según la escala que necesites)
            double nuevaEscalaX = escalaInicial + velocidadEscalarX * 10;
            double nuevaEscalaY = escalaInicial + velocidadEscalarY * 10;
            double nuevaEscalaZ = escalaInicial + velocidadEscalarZ * 10;

            // Crear un nuevo Punto con los valores de escala
            Punto nuevaEscala = new Punto(nuevaEscalaX, nuevaEscalaY, nuevaEscalaZ);

            // Aplicar la escalación al objeto
            this.objeto.escalar(nuevaEscala);
        }
        public void setOperacion(Punto operacion)
        {
            this.operacion = operacion;
        }
    }
}
