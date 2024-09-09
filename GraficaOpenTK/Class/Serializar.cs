using GraficaOpenTK.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GraficaOpenTK.Class
{
    public class Serializar
    {
        public static void save(string name,Objeto objeto, string path = @"C:\Users\Usuario\source\repos\GraficaOpenTK\GraficaOpenTK\files\")
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

        public static Objeto open(string name, string path = @"C:\Users\Usuario\source\repos\GraficaOpenTK\GraficaOpenTK\files\")
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

        public static double GradosARadianes(double grados)
        {
            return grados * (Math.PI / 180.0);
        }
    }
}
