﻿using System.Windows;
using ViewModels.Windows;

namespace UI.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new vmWinMain();
        }

        private void ClickCorrection(object sender, RoutedEventArgs e)
        {
            new StocksCorrection().ShowDialog();
        }
    }
}
