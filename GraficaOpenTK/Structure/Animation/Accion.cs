using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraficaOpenTK.Interfaces;

namespace GraficaOpenTK.Structure.Animation
{
    internal class Accion : IAnimacion
    {
        public List<ITransformacion> listaTransformacion;

        public Accion(ITransformacion transformacion)
        {
            listaTransformacion = new List<ITransformacion> { transformacion };
        }

        public void add(ITransformacion transformacion)
        {
            listaTransformacion.Add(transformacion);
        }

        public void play(long control)
        {
            foreach (var item in listaTransformacion)
            {
                item.ejecutar(control);
            }
        }
    }
}
