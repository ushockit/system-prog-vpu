using ShipsDemo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ShipsGenerator generator = new ShipsGenerator();
        Dictionary<Ship.Type, ProgressBar> progressBars = new Dictionary<Ship.Type, ProgressBar>();

        public ObservableCollection<Ship> SeaShips { get; set; } = new ObservableCollection<Ship>();
        public ObservableCollection<Ship> TonelShips { get; set; } = new ObservableCollection<Ship>();


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            progressBars.Add(Ship.Type.Banana, pbBanana);
            progressBars.Add(Ship.Type.Bread, pbBread);
            progressBars.Add(Ship.Type.Clothes, pbClothes);

            generator.GenerateNewShip += Generator_GenerateNewShip;
            generator.RunGenerator();
        }

        private void Generator_GenerateNewShip(Ship newShip)
        {
            newShip.LoadingProgress += NewShip_LoadingProgress;
            newShip.PierEnter += NewShip_PierEnter;
            newShip.PierLeave += NewShip_PierLeave;
            newShip.TonelEnter += NewShip_TonelEnter;
            newShip.TonelLeave += NewShip_TonelLeave;

            Dispatcher.Invoke(new Action(() => SeaShips.Add(newShip)));
        }

        private void NewShip_TonelLeave(Ship ship)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                TonelShips.Remove(ship);
            }));
        }

        private void NewShip_TonelEnter(Ship ship)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                SeaShips.Remove(ship);
                TonelShips.Add(ship);
            }));
        }

        private void NewShip_PierLeave(Ship ship)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                progressBars[ship.ShipType].Value = 0;
            }));
        }

        private void NewShip_PierEnter(Ship ship)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                progressBars[ship.ShipType].Maximum = ship.Volume;
            }));
        }

        private void NewShip_LoadingProgress(Ship ship, int progress)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                progressBars[ship.ShipType].Value = progress;
            }));
        }
    }
}
