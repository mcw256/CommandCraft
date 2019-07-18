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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Win32;
using System.IO;

namespace CommandCraft_App.Pages
{
    /// <summary>
    /// Interaction logic for downloadPage.xaml
    /// </summary>
    public partial class downloadPage : Page
    {
        private downloadPageDataViewModel pageData;


        #region Default Constructor
        public downloadPage()
        {
            InitializeComponent();
        }
        #endregion

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            pageData = new downloadPageDataViewModel("", "", true, Visibility.Collapsed);

            DataContext = pageData;
          
        }

        #region Buttons Click Handlers

        private void BtnGet_Click(object sender, RoutedEventArgs e)
        {
            pageData.IsGetButtonEnabled = false;
            var buildingUrl = txtBoxUrl.Text;
            ProcessManager.Process(buildingUrl);

            pageData.BuildingName = ProcessManager.BuildingName;
            pageData.IsBuildingInfoVisible = Visibility.Visible;
            pageData.IsGetButtonEnabled = true;
        }

        private void BtnSaveAsTxt_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt|McFunction (*.mcfunction)|*.mcfunction";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllLines(saveFileDialog.FileName, ProcessManager.MinecraftFunction);

        }

        private void BtnAddToDatabase_Click(object sender, RoutedEventArgs e)
        {
            DataAccess db = new DataAccess(); //don't know why I don't make this class static


            string buildingData = String.Join("\r\n", ProcessManager.MinecraftFunction);
            db.SaveBuilding(pageData.BuildingName, buildingData);

        }

        #endregion      
    }

    class downloadPageDataViewModel : INotifyPropertyChanged
    {
        private string _givenUrl;
        public string GivenUrl
        {
            get { return _givenUrl; }

            set
            {
                if (_givenUrl == value)
                    return; 

                _givenUrl = value;
                OnPropertyChanged();
            }
        }

        private string _buildingName;
        public string BuildingName
        {
            get { return _buildingName; }

            set
            {
                if (_buildingName == value)
                    return;

                _buildingName = value;
                OnPropertyChanged();
            }
        }

        private bool _isGetButtonEnabled;
        public bool IsGetButtonEnabled
        {
            get { return _isGetButtonEnabled; }

            set
            {
                if (_isGetButtonEnabled == value)
                    return;

                _isGetButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        private Visibility _isBuildingInfoVisible;
        public Visibility IsBuildingInfoVisible
        {
            get { return _isBuildingInfoVisible; }

            set
            {
                if (_isBuildingInfoVisible == value)
                    return;

                _isBuildingInfoVisible = value;
                OnPropertyChanged();
            }
        }

        public downloadPageDataViewModel(string givenUrl, string buildingName, bool isGetButtonEnabled, Visibility isBuildingInfoVisible)
        {
            GivenUrl = givenUrl;
            BuildingName = buildingName;
            IsGetButtonEnabled = isGetButtonEnabled;
            IsBuildingInfoVisible = isBuildingInfoVisible;
        }

        private void OnPropertyChanged([CallerMemberName] string memberName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
