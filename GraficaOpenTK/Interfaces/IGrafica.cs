using GraficaOpenTK.Structure;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace GraficaOpenTK.Interfaces
{
    public interface IGrafica
    {
        public Punto centro { get; set; }
        public void draw();

        public void setCentro(Punto centro);
        public void rotar(Punto angulo);

        public void escalar(Punto factor);

        public void trasladar(Punto valor);
        public Punto CalcularCentroDeMasa();
        public void setPrimitiveType(PrimitiveType tipo);
        public Punto CentroDependiente {  get; set; }
    }
}
