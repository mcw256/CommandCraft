using Grabcraft_Helper;
using Grabcraft_Helper.DataTypes;
using Grabcraft_Helper.Model;
using Grabcraft_Helper.ViewModels.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Grabcraft_Helper.ViewModels
{
    class HomeStep1ViewModel : ViewModelBase
    {
        #region Constructor
        public HomeStep1ViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            GoBtnClicked = new RelayCommand<object>(GoButtonClicked);

            var response = ActionManager.LoadDictionaries();
            if(response.IsError) MessageBox.Show(response.ErrorMsg);
        }
        #endregion

        #region Properties
        private MainWindowViewModel _mainWindowViewModel;
        public RelayCommand<object> GoBtnClicked { get; set; }

        private string _buildingURL;

        public string BuildingURL
        {
            get
            {
                return _buildingURL;
            }
            set
            {
                if (_buildingURL == value)
                    return;

                _buildingURL = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        private void GoButtonClicked(object obj)
        {
            _mainWindowViewModel.CurrentPage = _mainWindowViewModel.Pages["HomeStep2"];

            var response = ActionManager.DownloadAndProcessBuilding(BuildingURL);
            if(response.IsError) MessageBox.Show(response.ErrorMsg);
        }
        #endregion

    }
}
