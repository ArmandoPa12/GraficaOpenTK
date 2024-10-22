using GraficaOpenTK.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficaOpenTK.Structure.Animation
{
    internal class Libreto : IAnimacion
    {
        public List<Escena> listaEscena;
        public Libreto(Escena escena)
        {
            listaEscena = new List<Escena> { escena };
        }
        public Libreto()
        {
            listaEscena = new List<Escena> ();
        }
        public void add(Escena escena)
        {
            listaEscena.Add(escena);
        }

        public void play(long control)
        {
            foreach (var item in listaEscena)
            {
                item.play(control);
            }
        }
    }
}
