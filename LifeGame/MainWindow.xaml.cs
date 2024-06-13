using System;
using System.Collections.Generic;
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

namespace LifeGame
{
    public partial class MainWindow : Window
    {
        readonly Board game = new();
        readonly BoardUI gameUI = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnRandomSeedButtonClicked(object sender, RoutedEventArgs e)
        {
            game.InitializeBoard();
            gameUI.InitializeBoardUI(game, LifeBoard);
        }
    }
}
