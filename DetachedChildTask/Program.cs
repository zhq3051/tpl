using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DetachedChildTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var outer = Task.Factory.StartNew(() => {
                Console.WriteLine("Outer task beginning");

                var child = Task.Factory.StartNew(() =>
                {
                    Thread.SpinWait(5000000);
                    Console.WriteLine("Detached task completed");
                });
            });

            outer.Wait();
            Console.WriteLine("Outer task completed");
            Console.ReadKey();
        }
    }
}
