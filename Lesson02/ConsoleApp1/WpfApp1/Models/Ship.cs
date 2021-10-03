using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShipsDemo.Models
{
    public class Ship
    {
        public event Action<Ship> TonelEnter;
        public event Action<Ship> TonelLeave;
        public event Action<Ship> PierEnter;
        public event Action<Ship> PierLeave;
        public event Action<Ship, int> LoadingProgress;

        public enum Type
        {
            Banana,
            Bread,
            Clothes
        }

        public static int[] AVAILABLE_VOLUMES { get; } = new int[] { 10, 50, 100 };
        const int LOADING_VAL_PER_SEC = 10;
        const int LOADING_DELAY = 1000;
        // для синхронизации тонеля
        static Semaphore semaphore = new Semaphore(5, 5);
        // для синхронизации причалов
        static Dictionary<Type, Mutex> mutexDict = new Dictionary<Type, Mutex>
        {
            { Type.Banana, new Mutex() },
            { Type.Bread, new Mutex() },
            { Type.Clothes, new Mutex() },
        };

        Thread thread;
        public Type ShipType { get; set; }
        public int Volume { get; set; }
        public Ship(Type shipType, int volume)
        {
            ShipType = shipType;
            Volume = volume;
            thread = new Thread(DoWork);
            thread.IsBackground = true;
            thread.Start();
        }

        private void DoWork()
        {
            // Ждем захода в тонель
            semaphore.WaitOne();
            TonelEnter?.Invoke(this);

            // Мы в тонеле, ждем захода на причал
            mutexDict[ShipType].WaitOne();
            PierEnter?.Invoke(this);

            // Освобождаем место в тонеле
            semaphore.Release();
            TonelLeave?.Invoke(this);

            // Мы на причале
            DoLoading();
            // освобождаем причал
            mutexDict[ShipType].ReleaseMutex();
            PierLeave?.Invoke(this);
        }

        private void DoLoading()
        {
            int loadingProgress = 0;

            while(loadingProgress <= Volume)
            {
                LoadingProgress?.Invoke(this, loadingProgress);
                loadingProgress += LOADING_VAL_PER_SEC;
                Thread.Sleep(LOADING_DELAY);
            }
        }

        public override string ToString()
        {
            return $"#{thread.ManagedThreadId} - [{ShipType}] [{Volume}]";
        }
    }
}
