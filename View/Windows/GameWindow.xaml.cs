using ProjectB.Model.Board;
using ProjectB.Model.Help;
using ProjectB.View.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
namespace ProjectB.View.Windows
{
    using R = Properties.Resources;

    public partial class GameWindow : Window
    {

        private Visibility optionsVisibility = Visibility.Collapsed;

        public GameWindow()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Owner?.Show();
        }

        private void ButCloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MaximizingWindow(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                butMaximize.ToolTip = R.maximize;
                WindowStyle = WindowStyle.SingleBorderWindow;
                WindowState = WindowState.Normal;
                Image image = butMaximize.Content as Image;
                image.Source = new BitmapImage(new Uri(App.pathToMaximize));
            }
            else
            {
                butMaximize.ToolTip = R.minimize;
                WindowStyle = WindowStyle.None;
                WindowState = WindowState.Maximized;
                Image image = butMaximize.Content as Image;
                image.Source = new BitmapImage(new Uri(App.pathToMinimize));
            }
        }


        private void ChangeOptionsState()
        {
            butDialogs.Visibility = optionsVisibility;
            butExit.Visibility = optionsVisibility;
            butMaximize.Visibility = optionsVisibility;
            butMusic.Visibility = optionsVisibility;
        }

        private void ButOptionsClick(object sender, RoutedEventArgs e)
        {
            if (optionsVisibility == Visibility.Collapsed)
            {
                optionsVisibility = Visibility.Visible;
                butOptions.ToolTip = R.options_hide;
                Image image = butOptions.Content as Image;
                image.Source = new BitmapImage(new Uri(App.pathToOptionsHide));
            }
            else
            {
                optionsVisibility = Visibility.Collapsed;
                butOptions.ToolTip = R.options;
                Image image = butOptions.Content as Image;
                image.Source = new BitmapImage(new Uri(App.pathToOptions));
            }
            ChangeOptionsState();

        }

        private void ButExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(R.quit_game_confirmation, R.quit_game_title, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                base.OnClosing(e);
            }
        }
    }
}
