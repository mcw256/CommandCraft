using CommandCraft_App.Pages;
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

namespace CommandCraft_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            pageFrame.Content = new homePage();


        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            pageFrame.Content = new homePage();
        }

       
        private void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            pageFrame.Content = new downloadPage();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            pageFrame.Content = new searchPage();
        }
    }
}
