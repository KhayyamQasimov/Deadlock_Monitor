using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson5_SP
{
    public class MonitorNumunesi
    {
        static bool gowBusy = false;
        static readonly object godOfWar = new object();
        static void PlayGow(string name)
        {
            Monitor.Enter(godOfWar);
            try
            {
                while (gowBusy)
                {
                    Console.WriteLine($"{name} gozleyir (oyun mesguldur)...");
                    Monitor.Wait(godOfWar);
                }
                gowBusy = true;
            }
            finally { Monitor.Exit(godOfWar); }

            Console.WriteLine($"{name} God of War oynayir");
            Thread.Sleep(1000);
            Console.WriteLine($"{name} oyundan cixdi");

            Monitor.Enter(godOfWar);
            try
            {
                gowBusy = false;
                Monitor.Pulse(godOfWar);
            }
            finally { Monitor.Exit(godOfWar); }
        }

        public static void MonitorExample()
        {
            var khayyam = new Thread(() => PlayGow("Khayyam"));
            var omar = new Thread(() => PlayGow("Omar"));

            khayyam.Start();
            omar.Start();
            khayyam.Join();
            omar.Join();
        }
    }
}
