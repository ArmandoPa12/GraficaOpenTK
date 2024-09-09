using GraficaOpenTK.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficaOpenTK.Interfaces
{
    public interface IGrafica
    {
        public Punto centro { get; set; }
        public void draw();
        public void setCentro(Punto centro);
        public void escalar(Punto factor);
        public void trasladar(Punto desplazamiento);
        public void rotar(double aX, double aY, double aZ);

        //public T add();
    }
}
