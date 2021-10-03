using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShipsDemo.Models
{
    public class ShipsGenerator
    {
        static Random rnd = new Random();

        public event Action<Ship> GenerateNewShip;
        public void RunGenerator()
        {
            const int DELAY = 500;
            Thread thread = new Thread(() => 
            {
                while(true)
                {
                    GenerateNewShip?.Invoke(Generate());
                    Thread.Sleep(DELAY);
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }
        public Ship Generate()
        {
            Ship.Type type = (Ship.Type)Enum.Parse(typeof(Ship.Type), rnd.Next(0, 3).ToString());

            int volume = Ship.AVAILABLE_VOLUMES[rnd.Next(0, 3)];
            return new Ship(type, volume);
        }
    }
}
