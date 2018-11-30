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

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public TicTacToeGame game = new TicTacToeGame();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var tokens = button.Name.Split('_');

            button.Content = game.CurrentTurn;
            var gameOver = game.MakeTurn(int.Parse(tokens[1]), int.Parse(tokens[2]));

            if (gameOver)
            {
                MessageBox.Show($"Winner is {button.Content}!");
                Update();
            }
        }

        private void Update()
        {
            foreach (Button button in grid.Children)
            {
                button.Content = String.Empty;
            }
        }
    }
}
