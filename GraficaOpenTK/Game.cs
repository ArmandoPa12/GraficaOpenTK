using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using GraficaOpenTK.Structure;
using GraficaOpenTK.Class;
using GraficaOpenTK.Interfaces;
using System.Drawing;
using GraficaOpenTK.Structure.Transformacion;
using System.Diagnostics;
using GraficaOpenTK.Structure.Animation;


namespace GraficaOpenTK
{
    public class Game:GameWindow
    {
        private float angulox;
        private float anguloy;
        private float Rotar = 1.0f;
        private readonly Timer timer;
        private Stopwatch stopwatch;

        private float angle1;
        private float angle2;

        private double move = 0.01;
        private double ejex;
        private double speed = 0.01;

        private Escenario escenario1;
        private Camara camara;

        private IGrafica polimorfico;

        //private List<Accion> acciones = new List<Accion>();
        private int index = 0;
        private double tiempoAcumulado = 0;
        private ITransformacion t;

        private Objeto robot = new Objeto();
        private Parte arriba = new Parte();

        private Queue<ITransformacion> acciones;
        private Libreto libreto1;

        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {
            TargetUpdateFrequency = 60.0;  // Actualizaciones a 60 UPS
            TargetRenderFrequency = 60.0;  // Renderizado a 60 FPS

            ejex = 0.001;
            escenario1 = new Escenario();
            camara = new Camara(this);
            inicializaraCubo();
            inicializarT();
            //iniciarEsfera(0.05, 10, 10);
            acciones = new Queue<ITransformacion>();
            stopwatch = new Stopwatch();
            libreto1 = new Libreto();
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color4.White);
            GL.Enable(EnableCap.DepthTest);

            Poligono.cartesiano();
            stopwatch.Start();


            Parte torso = Serializar.openParte("ITorso");
            torso.draw();
            torso.seeCenter();

            Parte bIzq = Serializar.openParte("iBIzq");
            bIzq.draw();
            bIzq.seeCenter();

            Parte bDer = Serializar.openParte("iBDer");
            bDer.draw();
            bDer.seeCenter();



            //arriba = Serializar.openParte("iBIzq");
            Objeto esfera = Serializar.open("esfera");

            robot.add("torso", torso).add("izq", bIzq).add("der", bDer);
            escenario1.add("robot", robot);
            escenario1.add("esfera", esfera);


            IGrafica letra = escenario1.getObjeto("letraT");
            IGrafica arriba = escenario1.getObjeto("letraT").getParte("arriba");

            letra.escalar(new Punto(1.8));
            letra.trasladar(new Punto(-0.5, 0.1, 0.1));

            robot.escalar(new Punto(0.5));
            robot.trasladar(new Punto(0.7, -0.8, 0.1));

            esfera.escalar(new Punto(.6));
            esfera.trasladar(new Punto(-0.2, 0.365, 0.1));

            escenario1.trasladar(new Punto(0,0.55,0));

            //robot.centro = robot.CalcularCentroDeMasa();
            //Console.WriteLine(robot.centro.ToString());
            //robot.trasladar(new Punto(0.2,0,0));
            //Console.WriteLine(robot.centro.ToString());


            //acciones.Enqueue(new Trasladar(robot, new Punto(-0.4, 0, 0), 1000, 2000, "trasladar A"));
            //acciones.Enqueue(new Trasladar(robot, new Punto(0.1, 0, 0), 2000, 2000, "trasladar B"));
            //acciones.Enqueue(new Trasladar(robot, new Punto(0, 5, 0), 1000, 3000, "trasladar C"));
            //acciones.Enqueue(new Trasladar(robot, new Punto(-2, 0, 0), 6000, 2000, "trasladar D"));

            //Trasladar a = new Trasladar(robot, new Punto(0.5, 0.2, 0.1), 1000, 1000, "trasladar A");


            //Console.WriteLine(a.calcular().ToString());



            //libreto1.add(new Escena(new Accion(b)));

            //acciones.Enqueue(new Trasladar(robot, new Punto(0.5, 0, 0), 1000, 1000, "X"));
            //acciones.Enqueue(new Trasladar(esfera, new Punto(0.5, 0.2, 0.1), 1000, 1000, "Y"));
            //acciones.Enqueue(new Trasladar(esfera, new Punto(0.5, 0.2, 0.1), 1000, 1000, "Z"));



            //acciones.Enqueue(new Rotar(robot, new Punto(45, 0, 0), 4000, 1000, "trasladar A"));


            //libreto1.add(new Escena(new Accion(new Escalar(robot, new Punto(1.2), 1000, 1000, "X"))));
            //libreto1.add(new Escena(new Accion(new Rotar(esfera, new Punto(10, 10, 10), 2000, 1000, "Y"))));




        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            const int targetFPS = 60;
            const double targetFrameTime = 1000.0 / targetFPS;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.LoadIdentity();
            GL.Translate(camara.TlsX, camara.TlsY, -0.2);
            GL.Rotate(camara.AngX, new Vector3d(1.0, 0.0, 0.0));
            GL.Rotate(camara.AngY, new Vector3d(0.0, 1.0, 0.0));
            GL.Rotate(camara.AngZ, new Vector3d(0.0, 0.0, 1.0));
            double scala = camara.Scale;
            GL.Scale(scala, scala, scala);
            Poligono.cartesiano();


            double fps = 1.0 / e.Time;
            //Console.WriteLine($"FPS: {fps:F2}");


            escenario1.draw();
            escenario1.seeCenter();
            //robot.draw();




            long controlTime = stopwatch.ElapsedMilliseconds;
            long x = stopwatch.ElapsedTicks;

            libreto1.play(controlTime);
            //Console.WriteLine(x);

            //if (acciones.Count > 0)
            //{
            //    var transformacion = acciones.Dequeue();
            //    if (controlTime >= transformacion.tInicio && controlTime <= (transformacion.tInicio + transformacion.tTotal))
            //    {
            //        transformacion.ejecutar(controlTime);
            //        //Console.WriteLine(controlTime + transformacion.n);
            //        acciones.Enqueue(transformacion);
            //    }
            //    else
            //    {
            //        // Si aún no ha llegado el tiempo de inicio de la transformación, la volvemos a poner en la cola
            //        acciones.Enqueue(transformacion);
            //        //bi-cola
            //        // getTick
            //        // getFPS
            //    }
            //}


            GL.Flush();

            Context.SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
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

        public void cuboGenerico()
        {
            //// arriba
            //Punto a = new Punto(0.23, 0.53, 0.05);
            //Punto b = new Punto(0.23, 0.47, 0.05);
            //Punto c = new Punto(0.4, 0.47, 0.05);
            //Punto d = new Punto(0.4, 0.53, 0.05);

            //Punto a1 = new Punto(0.23, 0.53, -0.05);
            //Punto b1 = new Punto(0.23, 0.47, -0.05);
            //Punto c1 = new Punto(0.4, 0.47, -0.05);
            //Punto d1 = new Punto(0.4, 0.53, -0.05);

            //Poligono aFront = new Poligono();
            //Poligono aBack = new Poligono();
            //Poligono aLeft = new Poligono();
            //Poligono aRigth = new Poligono();
            //Poligono aUp = new Poligono();
            //Poligono aDown = new Poligono();

            //PrimitiveType tipo = PrimitiveType.LineLoop;
            //Color color = Color.Gray;

            //aFront.add(a).add(b).add(c).add(d).setTipo(tipo).setColor(color);
            //aBack.add(a1).add(b1).add(c1).add(d1).setTipo(tipo).setColor(color);
            //aLeft.add(a).add(a1).add(b1).add(b).setTipo(tipo).setColor(color);
            //aRigth.add(d).add(d1).add(c1).add(c).setTipo(tipo).setColor(color);
            //aUp.add(a).add(d).add(d1).add(a1).setTipo(tipo).setColor(color);
            //aDown.add(b).add(c).add(c1).add(b1).setTipo(tipo).setColor(color);



            //arriba.add("arriba", aUp)
            //    .add("abajo", aDown)
            //    .add("frente", aFront)
            //    .add("atras", aBack)
            //    .add("izquierda", aLeft)
            //    .add("derecha", aRigth);
        }

        public void inicializaraCubo()
        {

            //Punto a2 = new Punto(-0.2, 0.2, 0.0);
            //Punto b2 = new Punto(-0.2, -0.2, 0.0);
            //Punto c2 = new Punto(0.2, -0.2, 0.0);
            //Punto d2 = new Punto(0.2, 0.2, 0.0);

            //Punto a21 = new Punto(-0.2, 0.2, 0.2);
            //Punto b21 = new Punto(-0.2, -0.2, 0.2);
            //Punto c21 = new Punto(0.2, -0.2, 0.2);
            //Punto d21 = new Punto(0.2, 0.2, 0.2);

            //// va2c2ios 
            //Poligono front = new Poligono();
            //Poligono back = new Poligono();
            //Poligono left = new Poligono();
            //Poligono right = new Poligono();
            //Poligono up = new Poligono();
            //Poligono down = new Poligono();


            //front.add(a2);
            //front.add(b2);
            //front.add(c2);
            //front.add(d2);

            //back.add(a21);
            //back.add(b21);
            //back.add(c21);
            //back.add(d21);

            //left.add(a2);
            //left.add(a21);
            //left.add(b21);
            //left.add(b2);

            //right.add(d2);
            //right.add(d21);
            //right.add(c21);
            //right.add(c2);

            //up.add(a2);
            //up.add(a21);
            //up.add(d21);
            //up.add(d2);

            //down.add(b2);
            //down.add(b21);
            //down.add(c21);
            //down.add(c2);


            //Parte cubo2 = new Parte();

            //Objeto cubo = new Objeto();

            //cubo2.add("a", front)
            //    .add("b", back)
            //    .add("c", left)
            //    .add("d", right)
            //    .add("e", up)
            //    .add("f", down);

            //cubo.add("cubo", cubo2);

            //Serializar.save("example",cubo);

            //Objeto p = new Objeto();
            //p = Serializar.open("example");

            //escenario1.add("cubo", p);
            
        }

        public void iniciarEsfera(double radio, int resolucionLatitud, int resolucionLongitud)
        {
            
            radio = Math.Min(radio, 0.9);

            // Crear una parte que contendrá los polígonos (con puntos) de la esfera
            Parte parteEsfera = new Parte();

            // Crear un polígono para la esfera
            Poligono poligonoEsfera = new Poligono();

            // Generar los puntos de la esfera usando latitudes y longitudes
            for (int i = 0; i <= resolucionLatitud; i++)
            {
                double theta = Math.PI * i / resolucionLatitud;  // Ángulo de latitud (de 0 a π)

                for (int j = 0; j <= resolucionLongitud; j++)
                {
                    double phi = 2 * Math.PI * j / resolucionLongitud;  // Ángulo de longitud (de 0 a 2π)

                    // Calcular los puntos en la superficie de la esfera
                    double x = radio * Math.Sin(theta) * Math.Cos(phi);
                    double y = radio * Math.Sin(theta) * Math.Sin(phi);
                    double z = radio * Math.Cos(theta);

                    // Crear un punto con las coordenadas calculadas
                    Punto puntoEsfera = new Punto(x, y, z);
                    // Añadir el punto al polígono
                    poligonoEsfera.add(puntoEsfera);
                }
            }


            // Añadir el polígono generado a la parte de la esfera
            parteEsfera.add("poligono_esfera", poligonoEsfera);

            Objeto esfera = new Objeto();
            esfera.add("esfera", parteEsfera);

            Serializar.save("esfera",esfera);
            //esfera.escalar(new Punto(1.2));
            //escenario1.add("esfera", esfera);

        }

        public void inicializarT()
        {
            //// arriba
            //Punto a = new Punto(-0.3, 0.4, 0.0);
            //Punto b = new Punto(-0.3, 0.2, 0.0);
            //Punto c = new Punto(0.3, 0.2, 0.0);
            //Punto d = new Punto(0.3, 0.4, 0.0);

            //Punto a1 = new Punto(-0.3, 0.4, 0.2);
            //Punto b1 = new Punto(-0.3, 0.2, 0.2);
            //Punto c1 = new Punto(0.3, 0.2, 0.2);
            //Punto d1 = new Punto(0.3, 0.4, 0.2);

            //Poligono aFront = new Poligono();
            //Poligono aBack = new Poligono();
            //Poligono aLeft = new Poligono();
            //Poligono aRigth = new Poligono();
            //Poligono aUp = new Poligono();
            //Poligono aDown = new Poligono();

            //PrimitiveType tipo = PrimitiveType.Quads;

            //aFront.add(a).add(b).add(c).add(d).setTipo(tipo).setColor(Color.Fuchsia);
            //aBack.add(a1).add(b1).add(c1).add(d1).setTipo(tipo).setColor(Color.Red);
            //aLeft.add(a).add(a1).add(b1).add(b).setTipo(tipo).setColor(Color.Black);
            //aRigth.add(d).add(d1).add(c1).add(c).setTipo(tipo).setColor(Color.Green);
            //aUp.add(a).add(d).add(d1).add(a1).setTipo(tipo).setColor(Color.Gray);
            //aDown.add(b).add(c).add(c1).add(b1).setTipo(tipo).setColor(Color.Yellow);

            ////aFront.draw();
            ////aBack.draw();
            ////aLeft.draw();
            ////aRigth.draw();
            ////aUp.draw(); 
            ////aDown.draw();

            //// abajo
            //Punto f = new Punto(-0.1, 0.2, 0.0);
            //Punto g = new Punto(-0.1, -0.3, 0.0);
            //Punto h = new Punto(0.1, -0.3, 0.0);
            //Punto i = new Punto(0.1, 0.2, 0.0);

            //Punto f1 = new Punto(-0.1, 0.2, 0.2);
            //Punto g1 = new Punto(-0.1, -0.3, 0.2);
            //Punto h1 = new Punto(0.1, -0.3, 0.2);
            //Punto i1 = new Punto(0.1, 0.2, 0.2);

            //Poligono bFront = new Poligono();
            //Poligono bBack = new Poligono();
            //Poligono bLeft = new Poligono();
            //Poligono bRigth = new Poligono();
            //Poligono bUp = new Poligono();
            //Poligono bDown = new Poligono();

            //bFront.add(f).add(g).add(h).add(i).setTipo(tipo).setColor(Color.Fuchsia);
            //bBack.add(f1).add(g1).add(h1).add(i1).setTipo(tipo).setColor(Color.Red);
            //bLeft.add(f).add(f1).add(g1).add(g).setTipo(tipo).setColor(Color.Black);
            //bRigth.add(i).add(i1).add(h1).add(h).setTipo(tipo).setColor(Color.Green);
            //bUp.add(f).add(i).add(i1).add(f1).setTipo(tipo).setColor(Color.Gray);
            //bDown.add(g).add(h).add(h1).add(g1).setTipo(tipo).setColor(Color.Yellow);

            ////bFront.draw();
            ////bBack.draw();
            ////bLeft.draw();
            ////bRigth.draw();
            ////bUp.draw();
            ////bDown.draw();

            //Parte arriba = new Parte();
            //arriba.add("arriba", aUp)
            //    .add("abajo", aDown)
            //    .add("frente", aFront)
            //    .add("atras", aBack)
            //    .add("izquierda", aLeft)
            //    .add("derecha", aRigth);


            //Parte abajo = new Parte();
            //abajo.add("arriba", bUp)
            //    .add("abajo", bDown)
            //    .add("frente", bFront)
            //    .add("atras", bBack)
            //    .add("izquierda", bLeft)
            //    .add("derecha", bRigth);


            ////arriba.draw();
            ////abajo.draw();

            //Objeto letraT = new Objeto();
            //letraT.add("arriba", arriba).add("abajo", abajo);
            //letraT.trasladar(new Punto(-0.3,0.2,0));
            ////letraT.draw();
            ////escenario1.add("letraT", letraT);


            //Objeto re = new Objeto();
            //re = Serializar.open("letraTnew");
            //re.trasladar(new Punto(-0.5, 0.2, 0));

            Objeto re2 = new Objeto();
            re2 = Serializar.open("letraTnew");
            re2.escalar(new Punto(0.7));
            re2.trasladar(new Punto(0.5, -0.2, 0));
            re2.setPrimitiveType(PrimitiveType.LineLoop);

            escenario1.add("letraT", re2);

            //Serializar.save("letraTnew", letraT);

        }

        


    }
}
