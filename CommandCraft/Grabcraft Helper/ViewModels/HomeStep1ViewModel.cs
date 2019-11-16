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
        public HomeStep1ViewModel(MainWindowViewModel _mainWindowViewModel)
        {
            this.mainWindowViewModel = _mainWindowViewModel;
            GoButtonClicked = new RelayCommand<object>(GoButtonClickedHandler);
        }
        #endregion

        #region Fields
        private MainWindowViewModel mainWindowViewModel;
        #endregion

        #region Properties
        // Commands
        public RelayCommand<object> GoButtonClicked { get; set; }
        
        //Actual properties
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

        #region Command Handlers
        private void GoButtonClickedHandler(object obj)
        {
            // TODO this should be in loaded handler, which i dont know how to call right now
            var response = ActionManager.LoadDictionaries();
            if (response.IsError)
                MessageBox.Show(response.ErrorMsg);
            // >

            response = ActionManager.DownloadAndProcessBuilding(BuildingURL);
            if (response.IsError)
            {
                MessageBox.Show(response.ErrorMsg);
                return;
            }
            mainWindowViewModel.CurrentPage = mainWindowViewModel.Pages["HomeStep2"];
        }
        #endregion
    }
}
