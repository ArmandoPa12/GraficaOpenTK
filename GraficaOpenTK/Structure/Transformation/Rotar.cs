using GraficaOpenTK.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficaOpenTK.Structure.Transformacion
{
    public class Rotar : ITransformacion
    {
        public Punto operacion { get; set; }
        public IGrafica objeto { get; set; }
        public Punto puntoFinal { get; set; }
        public long tTotal { get; set; }
        public long tInicio { get; set; }
        public string texto { get; set; }
        
        public Rotar(IGrafica obj, Punto final, long inicio, long tiempo, string n = "n")
        {
            this.objeto = obj;
            this.operacion = new Punto();
            this.puntoFinal = final;
            this.tTotal = tiempo;
            this.tInicio = inicio;
            this.texto = n;
        }
        public void ejecutar(long control)
        {
            if (control >= this.tInicio && control <= (this.tInicio + this.tTotal))
            {
                //Console.WriteLine(this.texto + "--" + ((this.puntoFinal.X - this.objeto.centro.X) / this.tTotal) * 100);
                double nuevoX = (this.puntoFinal.X - this.objeto.centro.X) / this.tTotal;
                double nuevoY = (this.puntoFinal.Y - this.objeto.centro.Y) / this.tTotal;
                double nuevoZ = (this.puntoFinal.Z - this.objeto.centro.Z) / this.tTotal;

                Punto res = new Punto(nuevoX * 100, nuevoY * 100, nuevoZ * 100);

                this.objeto.rotar(res);
            }
            //double nuevoX = this.puntoFinal.X / this.tTotal;
            //double nuevoY = this.puntoFinal.Y / this.tTotal;
            //double nuevoZ = this.puntoFinal.Z / this.tTotal;
            //Punto nuevaPosicion = new Punto(nuevoX * 10, nuevoY * 10, nuevoZ * 10);
            //this.objeto.rotar(nuevaPosicion);
        }
        public void setOperacion(Punto operacion)
        {
            this.operacion = operacion;
        }
    }
}
