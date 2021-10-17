using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lesson03
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // Task task = new Task(() => {
            //     for (int i = 0; i < 10; i++)
            //     {
            //         Console.WriteLine("Hi");
            //         Thread.Sleep(500);
            //     }
            // });
            // task.Start();
            // task.Wait();

            // var task = Task.Run(() => {
            //     for (int i = 0; i < 10; i++)
            //     {
            //         Console.WriteLine("Hi");
            //         Thread.Sleep(500);
            //     }
            // });
            // task.Wait();

            // var task = Task.Factory.StartNew(() =>
            // {
            //     for (int i = 0; i < 10; i++)
            //     {
            //         Console.WriteLine("Hi");
            //         Thread.Sleep(500);
            //     }
            // });
            // task.Wait();

            // var parent = Task.Factory.StartNew(() => {
            //     Console.WriteLine("Start a parent task");
            // 
            //     var child = Task.Factory.StartNew(() =>
            //     {
            //         Console.WriteLine("Start a child task");
            //         Thread.Sleep(1000);
            //         Console.WriteLine("End a child task");
            //     }, TaskCreationOptions.AttachedToParent);
            // 
            //     Console.WriteLine("End a parent task");
            // });
            // 
            // parent.Wait();

            // var taskWithResult = Task.Run(() =>
            // {
            //     StringBuilder @string = new StringBuilder();
            // 
            //     for (int i = 0; i < 50; i++)
            //     {
            //         Thread.Sleep(100);
            //         @string.Append(i.ToString("X2"));
            //     }
            // 
            //     return @string.ToString();
            // });
            // taskWithResult.Start();

            // taskWithResult.Wait();

            // Console.WriteLine(taskWithResult.Result);

            // Task t = Task.Run(() =>
            // {
            //     Console.WriteLine("First start");
            //     Thread.Sleep(500);
            //     Console.WriteLine("First end");
            // });
            // 
            // Task t2 = t.ContinueWith((task) =>
            // {
            //     Console.WriteLine("Second start");
            //     Thread.Sleep(1000);
            //     Console.WriteLine("Second end");
            // });
            // 
            // t2.Wait();


            // Parallel.Invoke(() =>
            // {
            //     for (int i = 0; i < 10; i++)
            //     {
            //         Console.WriteLine("First run...");
            //         Thread.Sleep(500);
            //     }
            // }, () =>
            // {
            //     for (int i = 0; i < 10; i++)
            //     {
            //         Console.WriteLine("Second run...");
            //         Thread.Sleep(500);
            //     }
            // });

            // Parallel.For(1, 5, (n) =>
            // {
            //     Console.WriteLine(n);
            // });

            // List<string> paths = new List<string> { "Path 1", "Path 2", "Path 3" };
            // 
            // var result = Parallel.ForEach(paths, (path, pls) =>
            // {
            //     Console.WriteLine($"Start download with: {path}");
            //     for (int i = 0; i < 10; i++)
            //     {
            //         Console.WriteLine($"Downloading... '{path}'");
            //         Thread.Sleep(100);
            //     }
            //     // pls.Break();
            // });
            // result.

            // CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            // CancellationToken token = cancellationTokenSource.Token;
            // 
            // Task task = Task.Run(() => {
            //     Console.WriteLine("Task run");
            //     Thread.Sleep(3000);
            //     Console.WriteLine("Task end");
            // }, token);
            // 
            // Thread.Sleep(1500);
            // cancellationTokenSource.Cancel();

            // try
            // {
            //     var task = Task.Run(() =>
            //     {
            //         Console.WriteLine("Task start");
            //         Thread.Sleep(1000);
            //         throw new FormatException("Error format");
            //         Console.WriteLine("Task end");
            //     });
            // 
            //     task.Wait();
            // }
            // catch (AggregateException ex)
            // {
            //     Console.WriteLine("Exception - {0}", ex.InnerException.Message);
            // }


            //AsyncDemo();
            // Thread.Sleep(3000);

            int maxThreads, completitionPortThreads;
            ThreadPool.GetMaxThreads(out maxThreads, out completitionPortThreads);

            Console.WriteLine(maxThreads);
            Console.WriteLine(completitionPortThreads);

            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem((state) =>
                {
                    for (int k = 0; k < 5; k++)
                    {
                        Console.WriteLine($"Work - {Thread.CurrentThread.ManagedThreadId}");
                        Thread.Sleep(100);
                    }
                });
                Thread.Sleep(500);
            }

            Console.WriteLine("Hello World!");
        }

        static async void AsyncDemo()
        {
            Console.WriteLine("AsyncDemo start");
            await AsyncAction();
            Console.WriteLine("AsyncDemo end");
        }

        static async Task<int> AsyncAction()
        {
            return await Task.Run(() =>
            {
                int res = 0;
                for (int i = 0; i < 10; i++)
                {
                    res += i;
                    Thread.Sleep(100);
                }
                return res;
            });
        }
    }
}
