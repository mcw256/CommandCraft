using Grabcraft_Helper.ViewModels;
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

namespace Grabcraft_Helper.Views
{
    /// <summary>
    /// Interaction logic for HomeStep3.xaml
    /// </summary>
    public partial class HomeStep3 : UserControl
    {
        public HomeStep3()
        {
            InitializeComponent();
            DataContext = new HomeStep3ViewModel();
        }
    }
}
