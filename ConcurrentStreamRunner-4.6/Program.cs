using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetStandard;

namespace ConcurrentStreamRunner_4._6
{
    class Program
    {
        static void Main(string[] pargs)
        {
            var stream = new SampleStream();

            stream.LineReceived += (sender, args) =>
            {
                Console.WriteLine(args.Value.ToString());
            };

            for (var i = 0; i < 30; ++i)
            {
                stream.StartStreamAsync();

                Thread.Sleep(500);

                stream.StopStream();
            }


            Console.WriteLine("Press a key to exit");
            Console.ReadLine();
        }
    }
}
