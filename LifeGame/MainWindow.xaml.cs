using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
            game.InitializeBoard();
            gameUI.DrawBoardUI(game, LifeBoard);
        }

        private async void OnRandomSeedButtonClicked(object sender, RoutedEventArgs e)
        {
            int seed = Math.Abs(Environment.TickCount);
            SeedTextBlock.Text = $"Seed: {seed}";
            game.PopulateBoard(seed);
            gameUI.DrawBoardUI(game, LifeBoard);
            await Task.Delay(5000);
            game.AdvanceOneGeneration();
            gameUI.DrawBoardUI(game, LifeBoard);
        }

        private void OnSeededRunTextBoxFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = string.Empty;
        }

        private async void OnSeededButtonClicked(object sender, RoutedEventArgs e)
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

            SeedTextBlock.Text = $"Seed: {seed}";
            game.PopulateBoard(seed);
            gameUI.DrawBoardUI(game, LifeBoard);
            await Task.Delay(5000);
            game.AdvanceOneGeneration();
            gameUI.DrawBoardUI(game, LifeBoard);
        }
    }
}
