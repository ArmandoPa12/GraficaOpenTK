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
using GraficaOpenTK.Structure;
using System.Text.Json;

namespace GraficaOpenTK
{
    public class Game:GameWindow
    {
        private float angulox;
        private float anguloy;
        private float Rotar = 1.0f;
        private double move = 0.01;
        private double ejex;
        //private Poligono poligono;
        private Escenario escario;
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {
            ejex = 0.001;
            escario = new Escenario();

            // iniciar objetos
            inicializaraCubo();
            inicializarT();

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color4.White);
            GL.Enable(EnableCap.DepthTest);            
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Poligono.cartesiano();

            escario.draw();

            GL.Rotate(0.10, 0.0, 0.1, 0.1);
            Context.SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            //angle1 += 10.0f * (float)e.Time; // Rotación en grados por segundo
            //angle2 += 20.0f * (float)e.Time;

            //this.poligono.traslate(ejex, 0.0, 0.0);



            //Console.WriteLine("presionado");

            base.OnUpdateFrame(e);
            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }
            if (input.IsKeyDown(Key.Up))
            {
                //this.angulox += this.Rotar * (float)e.Time;
                this.angulox += this.Rotar;

            }
            if (input.IsKeyDown(Key.Down))
            {
                //this.angulox -= this.Rotar * (float)e.Time;
                this.angulox -= this.Rotar;
            }
            if (input.IsKeyDown(Key.Left))
            {
                //this.anguloy -= this.Rotar * (float)e.Time;
                //this.anguloy -= this.Rotar;
                this.ejex -= 0.01 * move;
                //this.poligono.traslate(ejex, 0.0, 0.0);
                //this.poligon.traslate(new Punto(ejex, 0.0, `0.0));

            }
            if (input.IsKeyDown(Key.Right))
            {
                //this.anguloy += this.Rotar * (float)e.Time;
                //this.anguloy += this.Rotar;
                this.ejex += 0.01 * move;
                //this.poligono.traslate(ejex, 0.0, 0.0);
                //this.poligono.traslate(new Punto(ejex, 0.0, 0.0));

            }

        }
    
        public void inicializaraCubo()
        {
            //Punto a = new Punto(-0.2, 0.2, 0.0);
            //Punto b = new Punto(-0.2, -0.2, 0.0);
            //Punto c = new Punto(0.2, -0.2, 0.0);
            //Punto d = new Punto(0.2, 0.2, 0.0);

            //Punto a1 = new Punto(-0.2, 0.2, 0.2);
            //Punto b1 = new Punto(-0.2, -0.2, 0.2);
            //Punto c1 = new Punto(0.2, -0.2, 0.2);
            //Punto d1 = new Punto(0.2, 0.2, 0.2);

            //// vacios 
            //Poligono front = new Poligono();
            //Poligono back = new Poligono();
            //Poligono left = new Poligono();
            //Poligono right = new Poligono();
            //Poligono up = new Poligono();
            //Poligono down = new Poligono();


            //front.add(a);
            //front.add(b);
            //front.add(c);
            //front.add(d);

            //back.add(a1);
            //back.add(b1);
            //back.add(c1);
            //back.add(d1);

            //left.add(a);
            //left.add(a1);
            //left.add(b1);
            //left.add(b);

            //right.add(d);
            //right.add(d1);
            //right.add(c1);
            //right.add(c);

            //up.add(a);
            //up.add(a1);
            //up.add(d1);
            //up.add(d);

            //down.add(b);
            //down.add(b1);
            //down.add(c1);
            //down.add(c);

            //Objeto cubo = new Objeto();
            //cubo.add("front", front);
            //cubo.add("back", back);
            //cubo.add("left", left);
            //cubo.add("right", right);
            //cubo.setCentro(new Punto(0.4, 0.4, 0.1));

            //this.serializar("prueba2", cubo);

            Objeto p = new Objeto();
            p = this.deserializar("prueba");

            escario.add("cubo", p); 
            

        }

        public void serializar(string name, Objeto objeto, string path = @"C:\Users\Usuario\source\repos\GraficaOpenTK\GraficaOpenTK\files\")
        {
            string save = JsonSerializer.Serialize(objeto);
            path += name + ".json";
            try
            {
                File.WriteAllText(path, save);
                Console.WriteLine("objeto guardado");
            }
            catch (Exception e)
            {
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public Objeto deserializar(string name, string path = @"C:\Users\Usuario\source\repos\GraficaOpenTK\GraficaOpenTK\files\")
        {
            Objeto objeto = new Objeto();
            path += name + ".json";
            try 
            {
                string serie = File.ReadAllText(path);
                objeto = JsonSerializer.Deserialize<Objeto>(serie);
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }
            return objeto;
        }

        public void inicializarT()
        {
            Punto p1 = new Punto(-0.3, 0.4, 0.0);
            Punto p2 = new Punto(-0.3, 0.2, 0.0);
            Punto p3 = new Punto(-0.1, 0.2, 0.0);
            Punto p4 = new Punto(-0.1, -0.3, 0.0);
            Punto p5 = new Punto(0.1, -0.3, 0.0);
            Punto p6 = new Punto(0.1, 0.2, 0.0);
            Punto p7 = new Punto(0.3, 0.2, 0.0);
            Punto p8 = new Punto(0.3, 0.4, 0.0);

            Punto p1a = new Punto(-0.3, 0.4, 0.2);
            Punto p2a = new Punto(-0.3, 0.2, 0.2);
            Punto p3a = new Punto(-0.1, 0.2, 0.2);
            Punto p4a = new Punto(-0.1, -0.3, 0.2);
            Punto p5a = new Punto(0.1, -0.3, 0.2);
            Punto p6a = new Punto(0.1, 0.2, 0.2);
            Punto p7a = new Punto(0.3, 0.2, 0.2);
            Punto p8a = new Punto(0.3, 0.4, 0.2);

            Poligono frontT = new Poligono();
            Poligono backT = new Poligono();

            frontT.add(p1);
            frontT.add(p2);
            frontT.add(p3);
            frontT.add(p4);
            frontT.add(p5);
            frontT.add(p6);
            frontT.add(p7);
            frontT.add(p8);

            backT.add(p1a);
            backT.add(p2a);
            backT.add(p3a);
            backT.add(p4a);
            backT.add(p5a);
            backT.add(p6a);
            backT.add(p7a);
            backT.add(p8a);

            Objeto tipoT = new Objeto();
            tipoT.add("frontT", frontT);
            tipoT.add("backT", backT);

            tipoT.setCentro(new Punto(-0.4, -0.3, 0.0));
            escario.add("T", tipoT);

        }

    }
}
