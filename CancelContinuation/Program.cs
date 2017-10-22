using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CancelContinuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            var cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            Timer timer = new Timer(Elapsed, cts, 5000, Timeout.Infinite);

            var t = Task.Run(()=> {
                List<int> product33 = new List<int>();
                for (int ctr = 0; ctr < Int16.MaxValue; ctr++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("\nCancellation requested in antecedent...\n");
                        token.ThrowIfCancellationRequested();
                    }

                    if (ctr % 2000 == 0)
                    {
                        int delay = rnd.Next(16, 501);
                        Thread.Sleep(delay);
                    }

                    if (ctr % 33 == 0)
                    {
                        product33.Add(ctr);
                    }
                }
                return product33.ToArray();
            }, token);

            Task continuation = t.ContinueWith((antecedent) => {
                Console.WriteLine("Multiple of 33:\n");
                var arr = antecedent.Result;
                for (int ctr = 0; ctr < arr.Length; ctr++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("\nCancellation requested in continuation...\n");
                        token.ThrowIfCancellationRequested();
                    }

                    if (ctr % 100 == 0)
                    {
                        int delay = rnd.Next(16, 251);
                        Thread.Sleep(delay);
                    }

                    Console.Write("{0:N0}{1}", arr[ctr], ctr != arr.Length -1 ? ", " : "");
                    if (Console.CursorLeft > 74)
                    {
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
            }, token);

            try
            {
                continuation.Wait();
            }
            catch (AggregateException e)
            {
                foreach (Exception ie in e.InnerExceptions)
                {
                    Console.WriteLine("{0}: {1}", ie.GetType().Name, ie.Message);
                }
            }
            finally
            {
                cts.Dispose();
            }

            Console.WriteLine("\nAntecedent Status: {0}", t.Status);
            Console.WriteLine("Continuation Status: {0}", continuation.Status);

            Console.ReadKey();

        }

        private static void Elapsed(object state)
        {
            CancellationTokenSource cts = state as CancellationTokenSource;
            if (cts == null)
            {
                return;
            }

            cts.Cancel();
            Console.WriteLine("\nCancellation request issued..\n");
        }
    }
}
