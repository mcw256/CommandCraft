using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Microsoft.Win32;

namespace CommandCraft_App
{
    /// <summary>
    /// Interaction logic for serachPage.xaml
    /// </summary>
    public partial class searchPage : Page
    {
        #region Private fields
        private string buildingData;
        List<Building> buildingsRaw;
        private searchPageDataViewModel pageData;
        #endregion

        #region Default constructor
        public searchPage()
        {
            InitializeComponent();
        }
        #endregion

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> buildingsCol; // buildings colection

            pageData = new searchPageDataViewModel(new List<string>(), "", Visibility.Hidden);
            DataAccess db = new DataAccess();
            buildingsRaw = db.GetBuildings();

            buildingsCol = new List<string>();
            foreach (var item in buildingsRaw)
            {
                buildingsCol.Add(item.BuildingName);
            }

            pageData.BuildingsCol = new ObservableCollection<string>(buildingsCol);


            DataContext = pageData;
        }

        #region Button Click handlers
        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            string bn;
            try
            {
                 bn = cbBuildings.SelectedItem.ToString();
            }
            catch (Exception)
            {
                return;           
            } 
            
            foreach (var item in buildingsRaw)
            {
                if(item.BuildingName == bn)
                {
                    buildingData = item.BuildingData;
                    break;
                }
            }
            pageData.BuildingName = bn;
            pageData.IsBuildingInfoVisible = Visibility.Visible;
        }
       
        private void BtnSaveAsTxt_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt|McFunction (*.mcfunction)|*.mcfunction";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, buildingData);
        }

        #endregion
    }

    class searchPageDataViewModel : INotifyPropertyChanged
    {
        
        private ObservableCollection<string> _buildingsCol; //I'm not quite sure if its gonna work
        public ObservableCollection<string> BuildingsCol
        {
            get { return _buildingsCol; }

            set
            {
                if (_buildingsCol == value)
                    return;

                _buildingsCol = value;
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

        public searchPageDataViewModel(List<string> buildingsCol, string buildingName, Visibility isBuildingInfoVisible )
        {
            BuildingsCol = new ObservableCollection<string>(buildingsCol);
            BuildingName = buildingName;
            IsBuildingInfoVisible = isBuildingInfoVisible;
        }

        private void OnPropertyChanged([CallerMemberName] string memberName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }


}
