using GraficaOpenTK.Interfaces;
using GraficaOpenTK.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficaOpenTK.Class
{
    public class Accion
    {
        public string nombre { get; set; }
        public string accion { get; set; }
        public Punto punto { get; set; }
        public IGrafica polimorfico { get; set; }
        //public Punto centroTemp {  get; set; }

        public Accion(string nombre, string accion, Punto punto, IGrafica poli)
        {
            this.nombre = nombre;
            this.accion = accion;
            this.punto = punto;
            this.polimorfico = poli;
            //this.centroTemp = centroTemp;
        }
    }
}
