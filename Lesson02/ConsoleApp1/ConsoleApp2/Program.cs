using System;
using System.Threading;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            object locker = new object();
            for (int i = 0; i < 4; i++)
            {
                Thread thread = new Thread(() =>
                {
                    bool isLock = false;
                    try
                    {
                        Monitor.Enter(locker, ref isLock);
                        for (int k = 0; k < 10; k++)
                        {
                            Console.WriteLine($"{Thread.CurrentThread.Name} - {k}");
                            Thread.Sleep(1000);
                        }
                    }
                    finally
                    {
                        if (isLock)
                        {
                            Monitor.Exit(locker);
                        }
                    }
                });
                thread.Name = $"Thread #{i + 1}";
                thread.Start();
            }
            Console.WriteLine("Hello World!");
        }
    }
}
