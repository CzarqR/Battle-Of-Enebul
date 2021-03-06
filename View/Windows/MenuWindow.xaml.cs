﻿using System.Windows;
using System.Diagnostics;

namespace ProjectB.View.Windows
{

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
            gameWindow.Show();
        }

        private void ButAuthorsClick(object sender, RoutedEventArgs e)
        {
            Process.Start(App.gameInfoWeb);
        }

        private void ButExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(App.comapnyWeb);
        }
    }
}
