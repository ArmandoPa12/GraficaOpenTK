using GraficaOpenTK.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficaOpenTK.Structure.Transformacion
{
    public class Trasladar : ITransformacion
    {
        public Punto operacion { get; set; }
        public IGrafica objeto { get; set; }
        public Punto puntoFinal { get; set; }
        public long tTotal { get; set; }
        public long tInicio { get; set; }
        public string texto { get; set; }


        public Trasladar(IGrafica obj, Punto final,long inicio, long tiempo, string n = "n")
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
                Console.WriteLine(this.texto+"--"+ ((this.puntoFinal.X - this.objeto.centro.X)/this.tTotal)*100);
                double nuevoX = (this.puntoFinal.X - this.objeto.centro.X) / this.tTotal;
                double nuevoY = (this.puntoFinal.Y - this.objeto.centro.Y) / this.tTotal;
                double nuevoZ = (this.puntoFinal.Z - this.objeto.centro.Z) / this.tTotal;

                Punto res = new Punto(nuevoX * 100, nuevoY * 100, nuevoZ * 100);

                this.objeto.trasladar(res);
            }




            //long tiempoTranscurrido = control - this.tInicio;
            //if (tiempoTranscurrido >= this.tTotal)
            //{
            //    //Console.WriteLine($"{this.texto} - Transformación completada");
            //    return;
            //}
            //double fraccion = (double)tiempoTranscurrido / this.tTotal;

            //double nuevoX = this.objeto.centro.X + (this.puntoFinal.X - this.objeto.centro.X) * fraccion;
            //double nuevoY = this.objeto.centro.Y + (this.puntoFinal.Y - this.objeto.centro.Y) * fraccion;
            //double nuevoZ = this.objeto.centro.Z + (this.puntoFinal.Z - this.objeto.centro.Z) * fraccion;

            //Punto res = new Punto(nuevoX / 100, nuevoY / 100, nuevoZ / 100);

            //Console.WriteLine(this.texto + "--" + control );
            //Console.WriteLine(this.objeto.centro.ToString());

            //this.objeto.trasladar(res);
        }

        public void setOperacion(Punto operacion)
        {
            this.operacion = operacion;
        }

        public Punto calcular()
        {
            double _x = this.objeto.centro.X - this.puntoFinal.X;
            double _y = this.objeto.centro.Y - this.puntoFinal.Y;
            double _z = this.objeto.centro.Z - this.puntoFinal.Z;
            return new Punto(_x, _y, _z);
        }
    }
}
