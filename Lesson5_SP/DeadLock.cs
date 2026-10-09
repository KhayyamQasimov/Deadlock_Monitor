using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson5_SP
{
    public class DeadLock
    {
        static readonly object godOfWar = new object();   // Omarın hesabındakı oyun
        static readonly object uncharted = new object();  // Khayyamın hesabındakı oyun
        public static void DeadlockExample()
        {
            var khayyam = new Thread(() =>
            {
                lock (uncharted)
                {
                    Console.WriteLine("Khayyam Uncharted-e girdi");
                    Thread.Sleep(500);
                    Console.WriteLine("Khayyam God of War-a girmek isteyir");
                    lock (godOfWar) Console.WriteLine("Khayyam God of War oynayır");
                }
            })
            { IsBackground = true };

            var omar = new Thread(() =>
            {
                lock (godOfWar)
                {
                    Console.WriteLine("Omar God of War-a girdi");
                    Thread.Sleep(500);
                    Console.WriteLine("Omar Uncharted-e girmek isteyir");
                    lock (uncharted) Console.WriteLine("Omar Uncharted oynayır");
                }
            })
            { IsBackground = true };

            khayyam.Start();
            omar.Start();

            if (!khayyam.Join(3000) || !omar.Join(3000))
                Console.WriteLine("DEADLOCK Oldu!!!");
        }


        //Mentiq: Xeyyamda Omerde steam aile hesabindadirlar amma beledirki bir oyuna biri girende digeri gire bilmir hemin oyuna buna oxsar bir deadlock yazdim

    }
}
