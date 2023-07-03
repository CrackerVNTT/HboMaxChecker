using HboMax2._0.Consola;
using HboMax2._0.Utils;
using HboMax2._0.Variables;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks; 
using Console = Colorful.Console;

namespace HboMax2._0.Checker
{
    public class Submenu
    {
        public static void Subm()
        {
            for (; ; )
            { 
                Va.checkeds = Va.hits + Va.free + Va.invalids + Va.custom + Va.expired;
                Console.WriteLine(Va.logo, Color.Blue);
                Console.WriteLine("                                Channel: t.me/VinoTintoMX", Color.White);
                Console.WriteLine("                                     By: @CrackerVNTT", Color.White);

                Console.Write("\n\t├───» Checked / Total: [", Color.GhostWhite);
                Console.Write(Va.checkeds + "/" + Va.cooque.Count(), Color.AliceBlue);
                Console.Write("]", Color.GhostWhite);

                Console.Write("\n\t├───» Hits : [", Color.GhostWhite);
                Console.Write(Convert.ToString(Va.hits), Color.FromArgb(0, 255, 0));
                Console.Write("]", Color.GhostWhite);

                Console.Write("\n\t├───» Free : [", Color.GhostWhite);
                Console.Write(Convert.ToString(Va.free), Color.FromArgb(255, 140, 0));
                Console.Write("]", Color.GhostWhite);

                Console.Write("\n\t├───» Custom : [", Color.GhostWhite);
                Console.Write(Convert.ToString(Va.custom), Color.FromArgb(255, 140, 0));
                Console.Write("]", Color.GhostWhite);

                Console.Write("\n\t├───» Expired : [", Color.GhostWhite);
                Console.Write(Convert.ToString(Va.expired), Color.FromArgb(255, 140, 0));
                Console.Write("]", Color.GhostWhite);

                Console.Write("\n\t├───» Invalids : [", Color.GhostWhite);
                Console.Write(Convert.ToString(Va.invalids), Color.FromArgb(255, 0, 0));
                Console.Write("]", Color.GhostWhite);

                Console.Write("\n\t├───» Retrys : [", Color.GhostWhite);
                Console.Write(Convert.ToString(Va.retrys), Color.FromArgb(255, 215, 0));
                Console.Write("]", Color.GhostWhite);

                Console.Write("\n\t├───» CPM : [", Color.GhostWhite);
                Console.Write(Convert.ToString(Util.GetCpm()), Color.FromArgb(30, 144, 255));
                Console.Write("]", Color.GhostWhite);


                if (Va.checkeds >= Va.legnth)
                {

                    GlobalData.Working = false;
                    Util.StopThreads(Va.threads);
                    break;
                }
                 
                Thread.Sleep(2000);
                Console.Clear();
            }

        }
    }
}
