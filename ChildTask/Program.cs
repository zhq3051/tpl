using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChildTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var parentTask = Task.Factory.StartNew(() => {
                Console.WriteLine("Parent task beginning");
                for (int ctr = 0; ctr < 10; ctr++)
                {
                    int taskNo = ctr;
                    Task.Factory.StartNew((x) => {
                        Thread.SpinWait(500000);
                        Console.WriteLine("Attached child #{0} completed", x);
                    }, taskNo, TaskCreationOptions.AttachedToParent);
                }
            });

            parentTask.Wait();
            Console.WriteLine("Parent task completed");
            Console.ReadKey();
        }
    }
}
