using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FinalAccessResult
{
    class Program
    {
        static void Main(string[] args)
        {
            Task[] taskArray = new Task[10];
            for (int i = 0; i < taskArray.Length; i++)
            {
                taskArray[i] = Task.Factory.StartNew((Object obj) => {
                    var data = new CustomData() { Name = i, CreateTime = DateTime.Now.Ticks };
                    data.ThreadNum = Thread.CurrentThread.ManagedThreadId;
                    Console.WriteLine("Task #{0} created at {1} on thread #{2}", data.Name, data.CreateTime, data.ThreadNum);
                }, i);
            }

            Task.WaitAll(taskArray);
            Console.ReadKey();
        }
    }

    public class CustomData
    {
        public long CreateTime;
        public int Name;
        public int ThreadNum;
    }
}
