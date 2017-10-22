using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskBaseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main";

            //Task taskA = new Task(() => { Console.WriteLine("hello from task A"); });

            Task taskA = Task.Run(() => { Console.WriteLine("hello from task A");  });

            //taskA.Start();

            Console.WriteLine("hello from thread {0}", Thread.CurrentThread.Name);

            taskA.Wait();

            Console.WriteLine("done");
            Console.ReadKey();
        }
    }
}
