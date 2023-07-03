using HboMax2._0.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HboMax2._0.Utils;
using System.Threading;

namespace HboMax2._0.Consola
{
    public class Ve
    {

        public static void Entrar()
        {
             

            Va.AmountThreads = Util.ThreadsAmount();
            Console.Clear();
            Va.proxyType = Util.Proxy();
            

 
            Console.Clear();

            for (; ; )
            {
                
                Va.cooque = Util.Combos();
                if (Va.cooque.Count() > 0)
                {
                    Va.legnth = Va.cooque.Count();
                    break;
                }

                Console.Clear();
            }


            Console.Clear();

            bool flag = Va.proxyType != Request.ProxyType.No;

            if(flag)
            {
                for(; ; )
                {
                    Va.list = Util.ProxyList();
                    if(Va.list.Count() > 0)
                    {
                        break;
                    }
                    Console.Clear();
                }
               
            }

            Console.Clear();
            GlobalData.Working = true;
            Util.Start();
            Va.threads = Util.GetThreads(Va.AmountThreads, Va.proxyType);
            Util.CrearCarpeta();
            foreach(Thread threads in Va.threads)
            {
                threads.Start();
            } 


        }

    }
}
