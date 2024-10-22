using GraficaOpenTK.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficaOpenTK.Structure.Animation
{
    internal class Escena : IAnimacion
    {
        public List<Accion> listaAcciones;

        public Escena(Accion accion)
        {
            listaAcciones = new List<Accion>();
            listaAcciones.Add(accion);
        }

        public void add( Accion accion)
        {
            listaAcciones.Add(accion);
        }

        public void play(long control)
        {
            foreach (var item in listaAcciones)
            {
                item.play(control);
            }
        }
    }
}
