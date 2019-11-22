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
using CommandCraft.ViewModels;

namespace CommandCraft.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            DataContext = new MainWindowViewModel();
        }

        private void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
                if (e.ChangedButton == MouseButton.Left)
                    DragMove();
        }

        private void WinNavCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WinNavMaxBtn_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                this.BorderThickness = new System.Windows.Thickness(0);
            }
            else
            {
                WindowState = WindowState.Maximized;
                this.BorderThickness = new System.Windows.Thickness(7);
            }
        }

        private void WinNavMinBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
