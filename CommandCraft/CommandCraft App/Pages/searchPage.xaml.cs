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
using CommandCraft_App.DBHandling;

namespace CommandCraft_App
{
    /// <summary>
    /// Interaction logic for serachPage.xaml
    /// </summary>
    public partial class searchPage : Page
    {
        List<Building> buildings = new List<Building>();

        public searchPage()
        {
            InitializeComponent();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataAccess db = new DataAccess();

            buildings = db.GetBuildings();
            cbBuildings.ItemsSource = buildings;
        }
    }
}
