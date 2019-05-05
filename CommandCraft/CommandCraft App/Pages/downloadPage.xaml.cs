using CommandCraft_App.Business_Logic.Activities;
using CommandCraft_App.Business_Logic;
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

namespace CommandCraft_App.Pages
{
    /// <summary>
    /// Interaction logic for downloadPage.xaml
    /// </summary>
    public partial class downloadPage : Page
    {
        private string buildingName;
        private string givenUrl;
        
        
        #region Default Constructor
        public downloadPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Buttons Click Handlers

        private void BtnGet_Click(object sender, RoutedEventArgs e)
        {
            btnGet.IsEnabled = false;
            givenUrl = txtBoxUrl.Text;
            //TODO given url validation -  regex checking
            ProcessManager.SetHtmlMainUrl(givenUrl);
            ProcessManager.Process();
            buildingName = ProcessManager.GetBuildingName();

            txtBlockBuildingName.Text = buildingName;
        }

        private void BtnSaveAsTxt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddToDatabase_Click(object sender, RoutedEventArgs e)
        {
            DataAccess db = new DataAccess();

            db.SaveBuilding(buildingName);

        }

        #endregion





    }
}
