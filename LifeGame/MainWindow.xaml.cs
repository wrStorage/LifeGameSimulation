using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Timers;

namespace LifeGame
{
    public partial class MainWindow : Window
    {
        readonly Board game = new();
        readonly BoardUI gameUI = new();
        readonly int generations = 60;
        int currentGeneration = 0;
        private static readonly System.Timers.Timer timer = new(200);

        public MainWindow()
        {
            InitializeComponent();
            game.InitializeBoard();
            gameUI.DrawBoardUI(game, LifeBoard);
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        }

        private void OnRandomSeedButtonClicked(object sender, RoutedEventArgs e)
        {
            timer.Enabled = false;
            int seed = Math.Abs(Environment.TickCount);
            SeedTextBlock.Text = $"Seed: {seed}";
            game.PopulateBoard(seed);
            gameUI.DrawBoardUI(game, LifeBoard);
            currentGeneration = 0;
            GenerationTextBlock.Text = $"Generation: {currentGeneration}";
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
            currentGeneration = 0;
            GenerationTextBlock.Text = $"Generation: {currentGeneration}";
            timer.Enabled = true;
        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            if (currentGeneration == generations)
            {
                timer.Enabled = false;
            }
            try
            {
                Dispatcher.Invoke(() =>
                {
                    game.AdvanceOneGeneration();
                    gameUI.DrawBoardUI(game, LifeBoard);
                    currentGeneration++;
                    GenerationTextBlock.Text = $"Generation: {currentGeneration}";
                });
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("Task canceled");
            }
        }
    }
}
