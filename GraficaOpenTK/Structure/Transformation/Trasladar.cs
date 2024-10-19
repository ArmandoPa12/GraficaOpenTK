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
        public Punto final { get; set; }
        public long tiempo { get; set; }
        public long tini { get; set; }
        public string n { get; set; }


        public Trasladar(IGrafica obj, Punto final,long inicio, long tiempo, string n = "n")
        {
            this.objeto = obj;
            this.operacion = new Punto();
            this.final = final;
            this.tiempo = tiempo;
            this.tini = inicio;
            this.n = n;            
        }
        public void ejecutar(long control)
        {
            //long tiempoTranscurrido = control - this.tini;

            //// Verificar que no haya pasado el tiempo total
            //if (tiempoTranscurrido >= this.tiempo)
            //{
            //    // Si el tiempo de la transformación ha terminado, la posición final se alcanza
            //    this.objeto.trasladar(this.final);
            //    Console.WriteLine("Transformación completada");
            //    return;
            //}

            //long tiempoRestante = this.tiempo - tiempoTranscurrido;
            //double nuevoX = this.final.X / tiempoRestante;
            //double nuevoY = this.final.Y / tiempoRestante;
            //double nuevoZ = this.final.Z / tiempoRestante;

            //// Crear la nueva posición
            //Punto nuevaPosicion = new Punto(nuevoX, nuevoY, nuevoZ);

            //// Trasladar el objeto a la nueva posición
            ////Console.WriteLine(nuevaPosicion);

            //this.objeto.trasladar(nuevaPosicion);





            long t = this.tiempo;
            //Console.WriteLine(t);

            double nuevoX = this.final.X / t;
            double nuevoY = this.final.Y / t;
            double nuevoZ = this.final.Z / t;
            Punto nuevaPosicion = new Punto(nuevoX * 10, nuevoY * 10, nuevoZ*10);
            this.objeto.trasladar(nuevaPosicion);
        }

        public void setOperacion(Punto operacion)
        {
            this.operacion = operacion;
        }
    }
}
