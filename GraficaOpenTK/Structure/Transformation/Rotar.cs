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
        public Punto final { get; set; }
        public long tiempo { get; set; }
        public long tini { get; set; }
        public string n { get; set; }
        
        public Rotar(IGrafica obj, Punto final, long inicio, long tiempo, string n = "n")
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
            double nuevoX = this.final.X / this.tiempo;
            double nuevoY = this.final.Y / this.tiempo;
            double nuevoZ = this.final.Z / this.tiempo;
            Punto nuevaPosicion = new Punto(nuevoX * 10, nuevoY * 10, nuevoZ * 10);
            this.objeto.rotar(nuevaPosicion);
        }
        public void setOperacion(Punto operacion)
        {
            this.operacion = operacion;
        }
    }
}
