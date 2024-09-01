using GraficaOpenTK;

namespace GraficaOpenTK
{
    class Program
    {
        static void Main(string[] args)
        {
            Game g = new Game(800, 600, "Game");
            g.Run(60.0);

           
        }
    }
}

