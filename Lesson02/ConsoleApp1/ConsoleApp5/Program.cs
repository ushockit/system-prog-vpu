using System;
using System.Threading;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            Semaphore semaphore = new Semaphore(3, 3);

            for (int i = 0; i < 5; i++)
            {
                Thread thread = new Thread(() =>
                {
                    semaphore.WaitOne();
                    for (int k = 0; k < 10; k++)
                    {
                        Console.WriteLine($"{Thread.CurrentThread.Name} - {k}");
                        Thread.Sleep(1000);
                    }
                    semaphore.Release();
                });
                thread.Name = $"Thread #{i + 1}";
                thread.Start();
            }

            Console.WriteLine("Hello World!");
        }
    }
}
