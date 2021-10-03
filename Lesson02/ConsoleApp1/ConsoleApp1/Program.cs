using System;
using System.Threading;

namespace ConsoleApp1
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
                    // Сначала проверяет не захвачена ли ссылка другим потоком? 
                    lock(locker)
                    {
                        for (int k = 0; k < 10; k++)
                        {
                            Console.WriteLine($"{Thread.CurrentThread.Name} - {k}");
                            Thread.Sleep(1000);
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
