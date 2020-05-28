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
using WPFCustomMessageBox;

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
                butMaximizeTT.Text = R.maximize;
                WindowStyle = WindowStyle.SingleBorderWindow;
                WindowState = WindowState.Normal;
            }
            else
            {
                butMaximizeTT.Text = R.minimize;
                WindowStyle = WindowStyle.None;
                WindowState = WindowState.Maximized;
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
                butOptionTT.Text = R.options_hide;
                Image image = butOptions.Content as Image;
                image.Source = new BitmapImage(new Uri(App.pathToOptionsHide));
            }
            else
            {
                optionsVisibility = Visibility.Collapsed;
                butOptionTT.Text = R.options;
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
            MessageBoxResult result = CustomMessageBox.ShowYesNo(R.quit_game_confirmation, R.quit_game_title, R.stay_battle, R.end_battle, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                base.OnClosing(e);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                Image image = butMaximize.Content as Image;
                image.Source = new BitmapImage(new Uri(App.pathToMinimize));
            }
            else
            {
                Image image = butMaximize.Content as Image;
                image.Source = new BitmapImage(new Uri(App.pathToMaximize));
            }
        }
    }
}
