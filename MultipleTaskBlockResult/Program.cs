using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleTaskBlockResult
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<double>[] taskArray = {
                Task<double>.Factory.StartNew(()=>{ return DoComputation(1.0); }),
                Task<double>.Factory.StartNew(()=>{ return DoComputation(100.0); }),
                Task<double>.Factory.StartNew(()=>{ return DoComputation(1000.0); })
            };

            var result = new double[taskArray.Length];
            double sum = 0;
            for (int i = 0; i < taskArray.Length; i++)
            {
                result[i] = taskArray[i].Result;
                Console.Write("{0:N1} {1}", result[i], i == taskArray.Length - 1 ? "= " : "+ ");
                sum += result[i];
            }
            Console.WriteLine("{0:N1}", sum);
            Console.ReadKey();
        }

        private static double DoComputation(double start)
        {
            double sum = 0;
            for (var value = start; value < start + 10; value += .1)
            {
                sum += value;
            }
            return sum;
        }
    }
}
