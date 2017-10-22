using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace continuewith
{
    class Program
    {
        static void Main(string[] args)
        {
            var getData = Task.Factory.StartNew(()=> {
                Random rnd = new Random();
                int[] values = new int[100];
                for (int ctr = 0; ctr < values.GetUpperBound(0); ctr++)
                {
                    values[ctr] = rnd.Next();
                }

                return values;
            });

            var processData = getData.ContinueWith((x) => {
                int n = x.Result.Length;
                long sum = 0;
                double mean;
                for (int ctr = 0; ctr < x.Result.GetUpperBound(0); ctr++)
                {
                    sum += x.Result[ctr];
                }

                mean = sum / (double)n;
                return Tuple.Create(n, sum, mean);
            });

            var displayData = processData.ContinueWith((x) => {
                return string.Format("N={0:N0}, total={1:N0}, mean={2:N0}", x.Result.Item1, x.Result.Item2, x.Result.Item3);
            });

            Console.WriteLine(displayData.Result);
            Console.ReadKey();
        }
    }
}
