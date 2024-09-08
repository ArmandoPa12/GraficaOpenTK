using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace GraficaOpenTK
{
    public class Camara
    {

        private double rotaX, rotaY;
        private double transX, transY;
        private double oldX, oldY;
        private readonly Game game;

        private readonly double speed = 0.5;

        public double AngX { get; set; }
        public double AngY { get; set; }
        public double AngZ { get; set; }
        public double TlsX { get; set; }
        public double TlsY { get; set; }
        public double TlsZ { get; set; }
        public double Scale { get; set; }

        public Camara(Game g)
        {
            game = g;
            rotaX = rotaY = 0.0;
            transX = transY = 0.0;
            AngX = AngY = AngZ = 0.0;
            TlsX = TlsY = TlsZ = 0.0;
            oldX = oldY = 0.0;
            Scale = 1;
        }
        public void KeyDown(KeyboardKeyEventArgs e)
        {

            if (e.Key == Key.Escape)
            {
                game.Exit();
            }
        }

        public void onMouseDown(MouseButtonEventArgs e)
        {        
            if (e.Button == MouseButton.Left)
            {
                MouseState m = Mouse.GetCursorState();
                oldX = m.X;
                oldY = m.Y;
            }
        }

        public void MouseMove(MouseMoveEventArgs e)
        {
            if (e.Mouse[MouseButton.Left])
            {
                RotarCamara(e);
            }
            else if (e.Mouse[MouseButton.Right])
            {
                double MovedX = e.X - oldX;
                double MovedY = e.Y - oldY;
                ActualizarTraslacion(MovedX, MovedY);  
                ActualizarPosicionMouse(e.X, e.Y);     
                AplicarTraslacion();                   
            }
        }



        private void RotarCamara(MouseMoveEventArgs e)
        {
            double MovedX = e.X - oldX;
            double MovedY = e.Y - oldY;

            if (MovedX == 0 && MovedY != 0)
            {
                rotaX = MovedY > 0 ? 1 : -1;
                rotaY = 0;
            }
            else if (MovedY == 0 && MovedX != 0)
            {
                rotaY = MovedX > 0 ? 1 : -1;
                rotaX = 0;
            }
            oldX = e.X;
            oldY = e.Y;
            if (rotaX == 1 || rotaX == -1)
            {
                AngX += rotaX * 1.25;
            }
            if (rotaY == 1 || rotaY == -1)
            {
                AngY += rotaY * 1.25;
            }
        }

        private void TrasladarCamara(MouseMoveEventArgs e)
        {

            double MovedX = e.X - oldX;
            double MovedY = e.Y - oldY;


            if (MovedX == 0 && MovedY != 0)
            {
                transY = MovedY > 0 ? -speed : speed;
                transX = 0;
            }
            else if (MovedY == 0 && MovedX != 0)
            {
                transX = MovedX > 0 ? speed : -speed;
                transY = 0;
            }

            oldX = e.X;
            oldY = e.Y;
            double au = 0.01, lim = 1;

            TlsX = transX > 0 && TlsX <= lim ? 
                TlsX + au :
                (transX < 0 && TlsX >= -lim ? TlsX - au : TlsX);
            TlsY = transY > 0 && TlsY <= lim ? TlsY + au :
                (transY < 0 && TlsY >= -lim ? TlsY - au : TlsY);

        }

        public void MouseWheel(MouseWheelEventArgs e)
        {
            int au = e.Delta;
            double df = 0.025;
            Scale = au > 0 && Scale > 0.50 ? Scale - df :
                (Scale < 2.9999f ? Scale + df : Scale);
        }
        void ActualizarTraslacion(double MovedX, double MovedY)
        {
            // Si solo hay movimiento en el eje Y
            if (MovedX == 0 && MovedY != 0)
            {
                transY = MovedY > 0 ? -0.05 : 0.05; 
                transX = 0; 
            }
            // Si solo hay movimiento en el eje X
            else if (MovedY == 0 && MovedX != 0)
            {
                transX = MovedX > 0 ? 0.05 : -0.05; 
                transY = 0; 
            }
        }
        void ActualizarPosicionMouse(double nuevoX, double nuevoY)
        {
            oldX = nuevoX;
            oldY = nuevoY;
        }
        void AplicarTraslacion()
        {
            double incremento = 0.01; 
            double limite = 1.0;      

            // Aplicar traslación en el eje X dentro de los límites
            if (transX > 0 && TlsX <= limite)
            {
                TlsX += incremento;
            }
            else if (transX < 0 && TlsX >= -limite)
            {
                TlsX -= incremento;
            }

            // Aplicar traslación en el eje Y dentro de los límites
            if (transY > 0 && TlsY <= limite)
            {
                TlsY += incremento;
            }
            else if (transY < 0 && TlsY >= -limite)
            {
                TlsY -= incremento;
            }
        }

    }
}
