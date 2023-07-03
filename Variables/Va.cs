using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Figgle;
using Request;

namespace HboMax2._0.Variables
{
    public class Va
    {
        public static string logo = FiggleFonts.Standard.Render("                                        TQM!");
        public static readonly ConcurrentDictionary<long, long> Cps = new ConcurrentDictionary<long, long>();
        public static List<string> list;
        public static ConcurrentQueue<string> cooque;
        public static ProxyType proxyType;
        public static List<Thread> threads;
        public static object fileLock = new object();
        public static int AmountThreads = 0;
        public static int hits = 0;
        public static int free = 0;
        public static int custom = 0;
        public static int expired = 0;
        public static int invalids = 0;
        public static int retrys = 0;
        public static int legnth = 0;
        public static int ban = 0;
        public static int checkeds = 0; 
    }

    public class GlobalData
    {
        public static bool Working;
        public static int LastChecks;
    }
}
