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
using System.Windows.Shapes;

namespace ProjectB.View.Windows
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void ButNewGameClick(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow
            {
                Owner = this
            };
            Hide();
            gameWindow.ShowDialog();
        }

        private void ButAuthorsClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(App.webPageUrl);
        }

        private void ButExitClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
