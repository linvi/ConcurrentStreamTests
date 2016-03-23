using System;
using System.Threading;
using PCL;

namespace ConcurrentStreamRunner
{
    class Program
    {
        static void Main(string[] mainArgs)
        {
            var stream = new SampleStream();

            stream.LineReceived += (sender, args) =>
            {
                Console.WriteLine(args.Value);
            };

            for (var i = 0; i < 10; ++i)
            {
                stream.StartStreamAsync();

                Thread.Sleep(3000);

                stream.StopStream();
            }
           

            Console.WriteLine("Press a key to exit");
            Console.ReadLine();
        }
    }
}