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


namespace GraficaOpenTK
{
    public class Game:GameWindow
    {
        private float angulox;
        private float anguloy;
        private float Rotar = 1.0f;

        private float angle1;
        private float angle2;

        private double move = 0.01;
        private double ejex;
        private double speed = 0.01;

        private Escenario escenario1;
        private Camara camara;

        private IGrafica polimorfico;

        private List<Accion> acciones = new List<Accion>();
        private int index = 0;
        private double tiempoAcumulado = 0;

        private Objeto robot = new Objeto();
        private Parte arriba = new Parte();
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {
            ejex = 0.001;
            escenario1 = new Escenario();
            camara = new Camara(this);
            inicializaraCubo();
            inicializarT();

        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color4.White);
            GL.Enable(EnableCap.DepthTest);

            Poligono.cartesiano();



            //Parte antBrazoIzq = Serializar.openParte("IAntBraIzq");
            //antBrazoIzq.draw();
            //antBrazoIzq.seeCenter();
            //antBrazoIzq.escalar(new Punto(2.5,1,1));

            //Parte BrazoIzq = Serializar.openParte("IBraIzq");
            //BrazoIzq.draw();
            //BrazoIzq.seeCenter();

            //robot.add("torso", torso).add("antBrazo", antBrazoIzq);

            //escenario1.add("robot",robot);


            //IGrafica temp1 = escenario1.getObjeto("robot").getParte("antBrazo");
            //IGrafica temp2 = escenario1.getObjeto("robot").getParte("brazo");


            //acciones.Add(new Accion("l","r",new Punto(0,0,40),temp1));


            //Punto copiaC = temp2.centro;
            //temp2.setCentro(temp1.centro);

            //acciones.Add(new Accion("l", "r", new Punto(0, 0, 40),temp2,temp1.centro));


            //// arriba
            //Punto a = new Punto(0.1, 0.55, 0.05);
            //Punto b = new Punto(0.1, 0.45, 0.05);
            //Punto c = new Punto(0.35, 0.45, 0.05);
            //Punto d = new Punto(0.35, 0.55, 0.05);

            //Punto a1 = new Punto(0.1, 0.55, -0.05);
            //Punto b1 = new Punto(0.1, 0.45, -0.05);
            //Punto c1 = new Punto(0.35, 0.45, -0.05);
            //Punto d1 = new Punto(0.35, 0.55, -0.05);

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

            //arriba.CentroDependiente = new Punto(-0.075,0.0,0);
            //arriba.setCentro( arriba.CalcularCentroDeMasa());

            //Parte parte = new Parte();
            //parte.add("arriba", aUp)
            //    .add("abajo", aDown)
            //    .add("frente", aFront)
            //    .add("atras", aBack)
            //    .add("izquierda", aLeft)
            //    .add("derecha", aRigth);
            //arriba.rotar(new Punto(0, 0, -90));
            //Serializar.saveParte("iBIzq", parte);


            //arriba = Serializar.openParte("iBIzq");

            //arriba.CentroDependiente = new Punto(0, 0.075, 0);
            ////arriba.trasladar(new Punto(-0.3, 0, 0));
            //arriba.setCentro(arriba.CalcularCentroDeMasa());
            //Serializar.saveParte("iBIzq", arriba);


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

            robot.add("torso", torso).add("izq", bIzq).add("der", bDer);
            escenario1.add("robot", robot);

            IGrafica poli;


           

            //--------------------
            poli = escenario1.getObjeto("robot").getParte("der");
            //poli.setCentro(poli.CalcularCentroDeMasa());
            for (int i = 0; i < 17; i++)
            {
                acciones.Add(new Accion("l", "r", new Punto(0, 0, i), poli));
            }

            //--------------------
            poli = escenario1.getObjeto("robot").getParte("izq");
            //poli.CalcularCentroDeMasa();

            for (int i = 0; i < 17; i++)
            {
                acciones.Add(new Accion("l", "r", new Punto(0, 0, -i), poli));
            }
            //--------------------
            poli = escenario1.getObjeto("robot");
            for (int i = 0; i < 17; i++)
            {
                acciones.Add(new Accion("l", "r", new Punto(0, i, 0), poli));
            }


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
            escenario1.seeCenter();
            this.libreto(index, e);


            //Parte torso = Serializar.openParte("ITorso");
            //torso.draw();
            //torso.seeCenter();

            //arriba.draw();
            //arriba.seeCenter();
            //arriba.rotar(new Punto(1, 0, 0));



            //robot.draw();
            //robot.seeCenter();

            GL.Flush();

            Context.SwapBuffers();
        }

        private void libreto(int i, FrameEventArgs e)
        {
            IGrafica polimorfico;
            tiempoAcumulado += e.Time;
            if (i < acciones.Count() && tiempoAcumulado >= 0.05)
            {
                if (acciones[i].accion == "t")
                {
                    polimorfico = acciones[i].polimorfico;
                    polimorfico.trasladar(acciones[i].punto);
                }
                if (acciones[i].accion == "r")
                {
                    //Punto copia = acciones[i].polimorfico.centro;

                    polimorfico = acciones[i].polimorfico;
                    polimorfico.setCentro(polimorfico.CalcularCentroDeMasa());
                    //polimorfico.setCentro(acciones[i].centroTemp);
                    //Punto copia = polimorfico.centro; 
                    ////acciones[i].polimorfico.setCentro();

                    polimorfico.rotar(acciones[i].punto);

                    //polimorfico.setCentro(copia);
                    //polimorfico.CalcularCentroDeMasa();

                }
                if (acciones[i].accion == "e")
                {
                    polimorfico = acciones[i].polimorfico;
                    polimorfico.escalar(acciones[i].punto);
                }
                this.index++;
                tiempoAcumulado = 0;
            }
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
