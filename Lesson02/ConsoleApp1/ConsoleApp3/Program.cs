using System;
using System.Threading;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            AutoResetEvent waitHandler = new AutoResetEvent(true);

            object locker = new object();
            for (int i = 0; i < 4; i++)
            {
                Thread thread = new Thread(() =>
                {
                    waitHandler.WaitOne();
                    for (int k = 0; k < 10; k++)
                    {
                        Console.WriteLine($"{Thread.CurrentThread.Name} - {k}");
                        Thread.Sleep(1000);
                    }
                    waitHandler.Set();
                });
                thread.Name = $"Thread #{i + 1}";
                thread.Start();
            }

            Console.WriteLine("Hello World!");
        }
    }
}
