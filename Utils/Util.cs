using HboMax2._0.Checker;
using HboMax2._0.Consola;
using HboMax2._0.Variables;
using Request;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Console = Colorful.Console;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HboMax2._0.Utils
{
    public class Util
    {
        public static int ThreadsAmount()
        {
            int Threads;

            for (; ; )
            {
                try
                {


                    Console.WriteLine(Va.logo, Color.Blue);

                    E.ConsolaE("Threads: ", "+");
                    Threads = Convert.ToInt32(Console.ReadLine());


                    if (Threads > 0)
                    {
                        break;
                    }
                }
                catch
                {

                    Console.Clear();
                    continue;
                }
            }

            return Threads;
        }

         
        public static void CrearCarpeta()
        {
            string rootFolder = "Results";
            string now = DateTime.Now.ToString("yyyy-MM-dd_h.mm.ss");
            string folderPath = Path.Combine(rootFolder, now);
            string hitsFolderPath = Path.Combine(folderPath, "Hits");
            string freesFolderPath = Path.Combine(folderPath, "Frees");

            if (!Directory.Exists(rootFolder))
            {
                Directory.CreateDirectory(rootFolder);
            }

            Directory.CreateDirectory(folderPath);
            Directory.CreateDirectory(hitsFolderPath);
            Directory.CreateDirectory(freesFolderPath);
        }

        public static void GuardarInfo(string combo, string capture, string folderName)
        {
            string folderPath = Path.Combine(Environment.CurrentDirectory, "Results", folderName);
            string fileName = (folderName == "hits") ? "Hits.txt" : "Frees.txt";
            string filePath = Path.Combine(folderPath, fileName);

            lock (Va.fileLock)
            {
                Directory.CreateDirectory(folderPath);

                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, combo + " | " + capture + Environment.NewLine);
                }
                else
                {
                    File.AppendAllText(filePath, combo + " | " + capture + Environment.NewLine);
                }
            }
        }



        public static ProxyType Proxy()
        {
            for (; ; )
            {
                try
                {
                    Console.WriteLine(Va.logo, Color.Blue);
                    E.ConsolaE("HTTP", "1");
                    Console.WriteLine();
                    E.ConsolaE("SOCKS4", "2");
                    Console.WriteLine();
                    E.ConsolaE("SOCKS5", "3");
                    Console.WriteLine();
                    E.ConsolaE("NO", "4");
                    Console.WriteLine();
                    E.ConsolaE("Seleccione la tecla con el proxy que va trabajar.....: ", "+");

                    ConsoleKeyInfo key = Console.ReadKey();
                    Console.WriteLine();

                    switch (key.KeyChar)
                    {
                        case '1':
                            return ProxyType.Http;
                        case '2':
                            return ProxyType.Socks4;
                        case '3':
                            return ProxyType.Socks5;
                        case '4':
                            return ProxyType.No;
                        default:
                            E.ConsolaE("Opción inválida. Por favor, selecciona una opción válida.", "-");
                            Thread.Sleep(1000);
                            Console.Clear();

                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    continue;
                }
            }
        }


        public static List<Thread> GetThreads(int amount, ProxyType proxy)
        {
            List<Thread> threadList = new List<Thread>(); 
            threadList.Add((new Thread(new ThreadStart(Submenu.Subm))));
            for (int i = 0; i < amount; i++)
            {
                Thread thread = new Thread(() =>
                {
                    ChH.Checker(proxy); 
                }); 
                threadList.Add(thread);
            }
  
            return threadList;
        }
        public static void StopThreads(List<Thread> threads)
        {
            foreach (Thread thread in threads)
            {
                thread.Join();
            }

        }

        public static List<string> ProxyList()
        {
            Console.WriteLine(Va.logo, Color.Blue);

            E.ConsolaE("Importar Proxilist", "+");
            List<string> lista = new List<string>();
            string proxy = GetUtils("ProxyList");


            if (string.IsNullOrEmpty(proxy))
            {
                E.ConsolaE("La ruta de acceso al archivo de proxies está vacía.", "-");
                return lista;
            }


            foreach (string text in File.ReadAllLines(proxy))
            {
                bool flag = text.Contains(":");
                if (flag)
                {
                    lista.Add(text);
                }
            }

            return lista ?? new List<string>();
        }

        public static ConcurrentQueue<string> Combos()
        {
            Console.WriteLine(Va.logo, Color.Blue);

            E.ConsolaE("Importar Combolist", "+");
            ConcurrentQueue<string> cooque = new ConcurrentQueue<string>();
            string data = GetUtils("ComboList");


            if (string.IsNullOrEmpty(data))
            {
                E.ConsolaE("La ruta de acceso al archivo de proxies está vacía.", "-");
                return cooque;
            }

            foreach (string text in File.ReadAllLines(data))
            {
                bool flag = text.Contains(":");

                if (flag)
                {
                    cooque.Enqueue(text);
                }
            }
            return cooque;
        }

        public static string GetUtils(string title)
        {
            string filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos Txt (*.txt) | *.txt";
                openFileDialog.Title = title;
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string text in File.ReadAllLines(openFileDialog.FileName))
                    {
                        text.Replace("\t", "");
                        text.Replace("\r", "");
                        text.Trim();
                        text.Replace(" ", "");
                    }

                    filePath = openFileDialog.FileName;
                }
            }
            return filePath;
        }
        public static void Start()
        {
            Task.Factory.StartNew(delegate ()
            {
                while (GlobalData.Working)
                {
                    Va.Cps.TryAdd(DateTimeOffset.Now.ToUnixTimeSeconds(), (long)GlobalData.LastChecks);
                    GlobalData.LastChecks = 0;
                    Thread.Sleep(1000);
                }
            });
        }

        public static long GetCpm()
        {
            long num = 0L;
            foreach (KeyValuePair<long, long> keyValuePair in Va.Cps)
            {
                bool flag = keyValuePair.Key >= DateTimeOffset.Now.ToUnixTimeSeconds() - 60L;
                if (flag)
                {
                    num += keyValuePair.Value;
                }
            }
            return num;
        }
    }
}
