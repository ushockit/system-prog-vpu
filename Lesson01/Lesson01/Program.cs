using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Lesson01
{
    class DownloadManager
    {
        const int COMPLETE_PROGRESS = 100_000;
        string _path = string.Empty;
        bool isActive = false;
        Thread thread;

        private bool paused = false;
        public bool Paused { set => paused = value; }

        public DownloadManager(string downloadPath)
        {
            _path = downloadPath;
            
        }

        public void Downloading()
        {
            int progress = 0;

            while(progress < COMPLETE_PROGRESS)
            {
                while(paused)
                {
                    Thread.SpinWait(100000000);
                }
                Console.WriteLine($"Donwloading...Progress = {progress}");
                progress++;
                // Thread.Sleep(1000);
            }
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            // Ex01();

            // Ex02();

            Ex03();
        }

        public static void Ex01()
        {
            Thread mainThread = Thread.CurrentThread;
            mainThread.Name = "Main thread";

            Console.WriteLine($"{mainThread.Name} start");

            Console.WriteLine($"Name = {mainThread.Name}");
            Console.WriteLine($"Id = {mainThread.ManagedThreadId}");
            Console.WriteLine($"DomainID = {Thread.GetDomainID()}");

            // Создание нового потока
            Thread thread = new Thread(new ThreadStart(() =>
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} start!");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name} is work!");
                    Thread.Sleep(500);
                }
                Console.WriteLine($"{Thread.CurrentThread.Name} end!");
            }));
            // Имя для нового потока
            thread.Name = "My custom thread";
            // Запуск потока
            // thread.Start();
            // Ожидание завершения выполнения потока
            // thread.Join();

            // Ожидание завершения выполнения потока на протяжении 3х секунд
            // Если в течении 3х секунд поток не завершает свою работу, то блокировка снимается
            // thread.Join(3000);


            Thread thread2 = new Thread(new ParameterizedThreadStart((obj) =>
            {
                var prms = obj as Tuple<int, int>;
                for (int i = prms.Item1; i <= prms.Item2; i++)
                {
                    Console.WriteLine($"Running - {i}");
                    // Thread.Sleep(1000);
                }
            }));
            thread2.Name = "Second name";
            thread2.Start(new Tuple<int, int>(10, 2_000_000));

            thread2.Join(500);
            // Отмена  работы потока
            thread2.Abort();

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} is work!");
                Thread.Sleep(1000);
            }


            Console.WriteLine($"{mainThread.Name} end");
        }

        public static void Ex02()
        {
            var downloadManager = new DownloadManager("some download path");
            var thread = new Thread(new ThreadStart(downloadManager.Downloading));

            thread.Start();

            downloadManager.Paused = true;

            Thread.Sleep(1500);

            downloadManager.Paused = false;

            thread.Interrupt();
        }

        public static void Ex03()
        {
            Random rnd = new Random();
            for(int i = 1; i <= 5; i++)
            {
                Thread thread = new Thread(new ThreadStart(() =>
                {
                    var curr = Thread.CurrentThread;
                    using (var fs = new FileStream($"{curr.Name}.txt", FileMode.Create, FileAccess.Write))
                    {
                        for (int k = 0; k < 20; k++)
                        {
                            var bytes = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
                            fs.Write(bytes, 0, bytes.Length);
                            Thread.Sleep(rnd.Next(500, 1500));
                        }
                    }
                }));
                thread.Name = $"Thread #{i}";
                // thread.IsBackground = true;
                thread.Start();
            }
        }
    }
}
