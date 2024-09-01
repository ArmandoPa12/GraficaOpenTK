using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Diagnostics.CodeAnalysis;

namespace GraficaOpenTK.Structure
{
    public class Poligono
    {
        private Punto ini;
        private List<Punto> listaPuntos;

        private Color color;
        private PrimitiveType primitiveType;

        public Poligono() 
        {
            this.ini = new Punto();
            this.listaPuntos = new List<Punto>();
            this.color = Color.Red;
            this.primitiveType = PrimitiveType.LineLoop;
        }
        public Poligono(Punto centro)
        {
            this.ini = centro;
            this.listaPuntos = new List<Punto>();
            this.color = Color.Red;
            this.primitiveType = PrimitiveType.LineLoop;
        }
        public Poligono(List<Punto> lista ,Punto centro,Color color, PrimitiveType tipo = PrimitiveType.LineLoop)
        {
            this.ini = centro;
            this.color = color;
            this.primitiveType = tipo; 
            this.listaPuntos = lista;   
        }
        

        // agregar
        public void add(Punto punto)
        {
            this.listaPuntos.Add(punto);
        }
        public void remove(Punto punto)
        {
            this.listaPuntos.Remove(punto);
        }
        public Punto getPunto(int x)
        {
            return listaPuntos[x];
        }

        public void draw()
        {
            GL.Color4(this.color);
            //GL.Begin(this.primitiveType);
            GL.Begin(PrimitiveType.Polygon);

            foreach (var item in listaPuntos)
            {   
                GL.Vertex3(item.X + ini.X, item.Y + ini.Y, item.Z + ini.Z);
            }
            GL.End();
            GL.Flush();
        }















        public void translate2() 
        {
            GL.PushMatrix();
            GL.Translate(0.2, 0.4, 0.0);
            this.draw();
            GL.PopMatrix();

        }
        public void setCentro(Punto newCentro)
        {
            ini = newCentro;
        }
        //public void traslate(double X,double Y, double Z)
        //{
        //    foreach (KeyValuePair<string, Punto> kvp in ListaPuntos)
        //    {
        //        ini.X += X;
        //        ini.Y += Y;
        //        ini.Z += Z;                
        //    }          
        //}
        public static void cartesiano(double escala = 0.1f)
        {
            PrimitiveType tipo = PrimitiveType.LineLoop;
            double width = 0.02f;

            GL.Begin(tipo);
            GL.Color4(Color.Red);
            GL.Vertex3(4f, 0f, 0);
            GL.Vertex3(-4f, 0f, 0);
            GL.End();

            for (double i = -4f; i <= 4f; i += escala)
            {
                GL.Begin(PrimitiveType.Lines);
                GL.Color4(Color.Red);
                GL.Vertex3(i, -width, 0);
                GL.Vertex3(i, width, 0);
                GL.End();
            }


            GL.Begin(tipo);
            GL.Color4(Color.Green);
            GL.Vertex3(0f, 4f, 0);
            GL.Vertex3(0f, -4f, 0);
            GL.End();
            for (double i = -4f; i <= 4f; i += escala)
            {
                GL.Begin(PrimitiveType.Lines);
                GL.Color4(Color.Green);
                GL.Vertex3(-width, i, 0);
                GL.Vertex3(width, i, 0);
                GL.End();
            }


            GL.Begin(tipo);
            GL.Color4(Color.Blue);
            GL.Vertex3(0f, 0f, 4f);
            GL.Vertex3(0f, 0f, -4f);
            GL.End();
            for (double i = -4f; i <= 4f; i += escala)
            {
                GL.Begin(PrimitiveType.Lines);
                GL.Color4(Color.Blue);
                GL.Vertex3(0, i, -width);
                GL.Vertex3(0, i, width);
                GL.End();
            }
        }
    }
}
