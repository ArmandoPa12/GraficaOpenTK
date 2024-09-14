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
using GraficaOpenTK.Class;
using GraficaOpenTK.Interfaces;


namespace GraficaOpenTK
{
    public class Game:GameWindow
    {
        //GLControl

        private float angulox;
        private float anguloy;
        private float Rotar = 1.0f;

        private float angle1;
        private float angle2;

        private double move = 0.01;
        private double ejex;

        //private Thread consoleThread;
        //private bool running = true;
        private Poligono aFront;
        private Objeto cubo;
        private Objeto letraT;
        //private Parte arriba;

        //private Objeto letraT;



        private Escenario escenario1;
        private Camara camara;
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {
            ejex = 0.001;
            escenario1 = new Escenario();
            camara = new Camara(this);
            //iniciar objetos
            //inicializaraCubo();
            aFront = new Poligono();
            letraT = new Objeto();
            cubo = new Objeto();
            inicializarT();
            inicializaraCubo();
            letraT.trasladar(new Punto(-0.3, 0.1, 0.0));
            cubo.trasladar(new Punto(0.2, 0.1, 0.2));

            //arriba = new Parte();
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e); 
            GL.ClearColor(Color4.White);
            GL.Enable(EnableCap.DepthTest);





            //escenario1.escalar(0.7, "letra", "arriba");



            //escenario1.trasladarObjeto("letraT", new Punto(-0.4,0.4,0));
            //escenario1.trasladar(new Punto(0.2,0,0),"letraT","arriba");


        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.LoadIdentity();
            GL.Translate(camara.TlsX, camara.TlsY, -0.2);
            GL.Rotate(camara.AngX, new Vector3d(1.0, 0.0, 0.0));
            GL.Rotate(camara.AngY, new Vector3d(0.0, 1.0, 0.0));
            GL.Rotate(camara.AngZ, new Vector3d(0.0, 0.0, 1.0));
            double scala = camara.Scale;
            GL.Scale(scala, scala, scala);

            Poligono.cartesiano();

            escenario1.draw();
            //escenario1.rotar(new Punto(0, 1, 0),"letraT","arriba");
            escenario1.trasladar(new Punto(0.001,0,0),"letraT","arriba");

            //IGrafica grafico;
            //grafico = escenario1.getObj("letraT").getParte("arriba");
            
            //grafico.rotar();


            GL.Flush();

            Context.SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
        }
        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);          
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            camara.KeyDown(e);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            camara.onMouseDown(e);
        }
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            camara.MouseMove(e);
        }
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            camara.MouseWheel(e);
        }

        public void ControlarConsola()
        {
            while (true)
            {
                Console.WriteLine("Introduce un comando (formato: accion escenario/objeto/parte [valores])");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                string[] tokens = input.Split(' ');

                if (tokens.Length < 2)
                {
                    Console.WriteLine("Formato inválido, usa: accion escenario/objeto/parte [valores]");
                    continue;
                }

                string accion = tokens[0].ToLower();
                string nombreObjeto = tokens[1].ToLower();

                switch (accion)
                {
                    case "rotar" or "r":
                        ProcesarRotar(tokens);
                        break;
                    case "escalar" or "e":
                        ProcesarEscalar(tokens);
                        break;
                    case "trasladar" or "t":
                        ProcesarTrasladar(tokens);
                        break;
                    default:
                        Console.WriteLine($"Acción no reconocida: {accion}");
                        break;
                }
            }
        }

        private void ProcesarRotar(string[] tokens)
        {
            if (tokens.Length < 5)
            {
                Console.WriteLine("Formato inválido para rotar, usa: rotar escenario/objeto/parte anguloX anguloY anguloZ [parte]");
                return;
            }

            double anguloX = double.Parse(tokens[2]);
            double anguloY = double.Parse(tokens[3]);
            double anguloZ = double.Parse(tokens[4]);
            Punto angulo = new Punto(anguloX, anguloY, anguloZ);

            if (tokens[1].ToLower() == "escenario")
            {
                escenario1.rotar(angulo);  // Rotar todo el escenario
            }
            else
            {
                string nombreObjeto = tokens[1];
                if (tokens.Length == 5)
                {
                    escenario1.rotar(angulo, nombreObjeto);  // Rotar objeto
                }
                else if (tokens.Length == 6)
                {
                    string nombreParte = tokens[5];
                    escenario1.rotar(angulo, nombreObjeto, nombreParte);  // Rotar parte específica
                }
            }
        }

        private void ProcesarEscalar(string[] tokens)
        {
            if (tokens.Length < 3)
            {
                Console.WriteLine("Formato inválido para escalar, usa: escalar escenario/objeto/parte factor [parte]");
                return;
            }

            double factor = double.Parse(tokens[2]);

            if (tokens[1].ToLower() == "escenario")
            {
                escenario1.escalar(factor);  // Escalar todo el escenario
            }
            else
            {
                string nombreObjeto = tokens[1];
                if (tokens.Length == 3)
                {
                    escenario1.escalar(factor, nombreObjeto);  // Escalar objeto
                }
                else if (tokens.Length == 4)
                {
                    string nombreParte = tokens[3];
                    escenario1.escalar(factor, nombreObjeto, nombreParte);  // Escalar parte específica
                }
            }
        }

        private void ProcesarTrasladar(string[] tokens)
        {
            if (tokens.Length < 5)
            {
                Console.WriteLine("Formato inválido para trasladar, usa: trasladar escenario/objeto/parte valorX valorY valorZ [parte]");
                return;
            }

            double valorX = double.Parse(tokens[2]);
            double valorY = double.Parse(tokens[3]);
            double valorZ = double.Parse(tokens[4]);
            Punto valor = new Punto(valorX, valorY, valorZ);

            if (tokens[1].ToLower() == "escenario")
            {
                escenario1.trasladar(valor);  // Trasladar todo el escenario
            }
            else
            {
                string nombreObjeto = tokens[1];
                if (tokens.Length == 5)
                {
                    escenario1.trasladar(valor, nombreObjeto);  // Trasladar objeto
                }
                else if (tokens.Length == 6)
                {
                    string nombreParte = tokens[5];
                    escenario1.trasladar(valor, nombreObjeto, nombreParte);  // Trasladar parte específica
                }
            }
        }



        public void inicializaraCubo()
        {
            Punto a2 = new Punto(-0.2, 0.2, 0.0);
            Punto b2 = new Punto(-0.2, -0.2, 0.0);
            Punto c2 = new Punto(0.2, -0.2, 0.0);
            Punto d2 = new Punto(0.2, 0.2, 0.0);

            Punto a21 = new Punto(-0.2, 0.2, 0.2);
            Punto b21 = new Punto(-0.2, -0.2, 0.2);
            Punto c21 = new Punto(0.2, -0.2, 0.2);
            Punto d21 = new Punto(0.2, 0.2, 0.2);

            // va2c2ios 
            Poligono front = new Poligono();
            Poligono back = new Poligono();
            Poligono left = new Poligono();
            Poligono right = new Poligono();
            Poligono up = new Poligono();
            Poligono down = new Poligono();


            front.add(a2);
            front.add(b2);
            front.add(c2);
            front.add(d2);

            back.add(a21);
            back.add(b21);
            back.add(c21);
            back.add(d21);

            left.add(a2);
            left.add(a21);
            left.add(b21);
            left.add(b2);

            right.add(d2);
            right.add(d21);
            right.add(c21);
            right.add(c2);

            up.add(a2);
            up.add(a21);
            up.add(d21);
            up.add(d2);

            down.add(b2);
            down.add(b21);
            down.add(c21);
            down.add(c2);

            Parte cubo2 = new Parte();
            cubo2.add("a", front)
                .add("b", back)
                .add("c", left)
                .add("d", right)
                .add("e", up)
                .add("f", down);

            cubo.add("cubo", cubo2);

            escenario1.add("cubo", cubo);

            
        }

        public void inicializarT()
        {
            // arriba
            Punto a = new Punto(-0.3, 0.4, 0.0);
            Punto b = new Punto(-0.3, 0.2, 0.0);
            Punto c = new Punto(0.3, 0.2, 0.0);
            Punto d = new Punto(0.3, 0.4, 0.0);

            Punto a1 = new Punto(-0.3, 0.4, 0.2);
            Punto b1 = new Punto(-0.3, 0.2, 0.2);
            Punto c1 = new Punto(0.3, 0.2, 0.2);
            Punto d1 = new Punto(0.3, 0.4, 0.2);

            Poligono aFront = new Poligono();
            Poligono aBack = new Poligono();
            Poligono aLeft = new Poligono();
            Poligono aRigth = new Poligono();
            Poligono aUp = new Poligono();
            Poligono aDown = new Poligono();

            PrimitiveType tipo = PrimitiveType.LineLoop;

            aFront.add(a).add(b).add(c).add(d).setTipo(tipo).setColor(Color.Fuchsia);
            aBack.add(a1).add(b1).add(c1).add(d1).setTipo(tipo).setColor(Color.Red);
            aLeft.add(a).add(a1).add(b1).add(b).setTipo(tipo).setColor(Color.Black);
            aRigth.add(d).add(d1).add(c1).add(c).setTipo(tipo).setColor(Color.Green);
            aUp.add(a).add(d).add(d1).add(a1).setTipo(tipo).setColor(Color.Gray);
            aDown.add(b).add(c).add(c1).add(b1).setTipo(tipo).setColor(Color.Yellow);

            //aFront.escalar(2.4);

            Parte arriba = new Parte();

            arriba.add("arriba", aUp)
                .add("abajo", aDown)
                .add("frente", aFront)
                .add("atras", aBack)
                .add("izquierda", aLeft)
                .add("derecha", aRigth);

            //arriba.escalar(0.3);


            //escenario1.add("letraT", letraT);

            // ---------------------------------------------------------------

            //abajo

            Punto f = new Punto(-0.1, 0.2, 0.0);
            Punto g = new Punto(-0.1, -0.3, 0.0);
            Punto h = new Punto(0.1, -0.3, 0.0);
            Punto i = new Punto(0.1, 0.2, 0.0);

            Punto f1 = new Punto(-0.1, 0.2, 0.2);
            Punto g1 = new Punto(-0.1, -0.3, 0.2);
            Punto h1 = new Punto(0.1, -0.3, 0.2);
            Punto i1 = new Punto(0.1, 0.2, 0.2);

            Poligono bFront = new Poligono();
            Poligono bBack = new Poligono();
            Poligono bLeft = new Poligono();
            Poligono bRigth = new Poligono();
            Poligono bUp = new Poligono();
            Poligono bDown = new Poligono();

            bFront.add(f).add(g).add(h).add(i).setTipo(tipo).setColor(Color.Fuchsia);
            bBack.add(f1).add(g1).add(h1).add(i1).setTipo(tipo).setColor(Color.Red);
            bLeft.add(f).add(f1).add(g1).add(g).setTipo(tipo).setColor(Color.Black);
            bRigth.add(i).add(i1).add(h1).add(h).setTipo(tipo).setColor(Color.Green);
            bUp.add(f).add(i).add(i1).add(f1).setTipo(tipo).setColor(Color.Gray);
            bDown.add(g).add(h).add(h1).add(g1).setTipo(tipo).setColor(Color.Yellow);


            Parte abajo = new Parte();
            abajo.add("arriba", bUp)
                .add("abajo", bDown)
                .add("frente", bFront)
                .add("atras", bBack)
                .add("izquierda", bLeft)
                .add("derecha", bRigth);

            letraT.add("arriba", arriba).add("abajo", abajo);


            //Objeto p = new Objeto();
            //p = Serializar.open("letraT");

            escenario1.add("letraT", letraT);

            //letraT.draw();

            //Serializar.save("letraTnew", letraT);

        }

    }
}
