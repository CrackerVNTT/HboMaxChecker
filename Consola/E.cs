using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Console = Colorful.Console;
using System.Threading.Tasks;

namespace HboMax2._0.Consola
{
    public class E
    {
        public static void ConsolaE(string mensaje, object data = null)
        {
            Console.Write("     [", Color.White);
            Console.Write(data, Color.Orange);
            Console.Write("]", Color.White);
            Console.Write(" " + mensaje, Color.GhostWhite);
        }
    }
}
