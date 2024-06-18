using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Timers;
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
        private static System.Timers.Timer? timer;

        public MainWindow()
        {
            InitializeComponent();
            game.InitializeBoard();
            gameUI.DrawBoardUI(game, LifeBoard);
            timer = new System.Timers.Timer(200);
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        }

        private void OnRandomSeedButtonClicked(object sender, RoutedEventArgs e)
        {
            timer.Enabled = false;
            int seed = Math.Abs(Environment.TickCount);
            SeedTextBlock.Text = $"Seed: {seed}";
            game.PopulateBoard(seed);
            gameUI.DrawBoardUI(game, LifeBoard);
            timer.Enabled = true;
        }

        private void OnSeededRunTextBoxFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = string.Empty;
        }

        private void OnSeededButtonClicked(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(SeedNumberTextBox.Text, out int numTest) == false)
            {   
                MessageBox.Show("Invalid Seed: Seed must be a number between 0-2147483647", "Invalid Seed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int seed = int.Parse(SeedNumberTextBox.Text);

            if (seed <= 0) 
            {
                MessageBox.Show("Invalid Seed: Seed must be a positive number", "Invalid Seed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            timer.Enabled = false;
            SeedTextBlock.Text = $"Seed: {seed}";
            game.PopulateBoard(seed);
            gameUI.DrawBoardUI(game, LifeBoard);
            timer.Enabled = true;
        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    game.AdvanceOneGeneration();
                    gameUI.DrawBoardUI(game, LifeBoard);
                });
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("Task canceled");
            }
        }
    }
}
