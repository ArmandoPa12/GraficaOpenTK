using GraficaOpenTK.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficaOpenTK.Interfaces
{
    public interface ITransformacion
    {
        public Punto operacion { get; set; }
        public long tInicio { get; set; }
        
        public IGrafica objeto { get; set; }
        public void ejecutar(long control);
        public Punto puntoFinal { get; set; }
        public long tTotal { get; set; }
        public string texto {  get; set; }
        
        public void setOperacion(Punto operacion);
    }
}
