using System;
using System.Threading;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            Mutex mutex = new Mutex();
            for (int i = 0; i < 4; i++)
            {
                Thread thread = new Thread(() =>
                {
                    mutex.WaitOne();
                    for (int k = 0; k < 10; k++)
                    {
                        Console.WriteLine($"{Thread.CurrentThread.Name} - {k}");
                        Thread.Sleep(1000);
                    }
                    mutex.ReleaseMutex();
                });
                thread.Name = $"Thread #{i + 1}";
                thread.Start();
            }
            Console.WriteLine("Hello World!");
        }
    }
}
