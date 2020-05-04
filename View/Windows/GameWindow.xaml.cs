using ProjectB.Model.Board;
using ProjectB.Model.Help;
using ProjectB.View.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
namespace ProjectB.View.Windows
{

    public partial class GameWindow : Window
    {

        public GameWindow()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Owner?.Show();
        }
    }
}
