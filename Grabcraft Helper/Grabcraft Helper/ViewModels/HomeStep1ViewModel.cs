using Grabcraft_Helper;
using Grabcraft_Helper.DataTypes;
using Grabcraft_Helper.Model;
using Grabcraft_Helper.ViewModels.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            Loaded = new RelayCommand<object>(LoadedHandler);
        }
        #endregion

        #region Fields
        private MainWindowViewModel mainWindowViewModel;
        #endregion

        #region Properties
        // Commands
        public RelayCommand<object> GoButtonClicked { get; set; }
        public RelayCommand<object> Loaded { get; set; }


        //Actual properties
        private bool _isGoButtonEnabled;
        public bool IsGoButtonEnabled
        {
            get
            {
                return _isGoButtonEnabled;
            }
            set
            {
                if (_isGoButtonEnabled == value)
                    return;

                _isGoButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _isGoButtonInProgress;
        public bool IsGoButtonInProgress
        {
            get
            {
                return _isGoButtonInProgress;
            }
            set
            {
                if (_isGoButtonInProgress == value)
                    return;

                _isGoButtonInProgress = value;
                OnPropertyChanged();
            }
        }


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

        private bool _isThereError;
        public bool IsThereError
        {
            get
            {
                return _isThereError;
            }
            set
            {
                if (_isThereError == value)
                    return;

                _isThereError = value;
                OnPropertyChanged();
            }
        }

        private string _errorMsg;
        public string ErrorMsg
        {
            get
            {
                if (!IsThereError || _errorMsg == null) return "";
                return _errorMsg;
            }
            set
            {
                if (_errorMsg == value)
                    return;

                _errorMsg = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Command Handlers
        private async void GoButtonClickedHandler(object obj)
        {
            IsGoButtonEnabled = false;
            IsGoButtonInProgress = true;
            var response = await ActionManager.DownloadAndProcessBuilding(BuildingURL);
            if (response.IsError)
            {
                IsThereError = true;
                ErrorMsg = response.ErrorMsg;
                return;
            }


            mainWindowViewModel.CurrentPage = mainWindowViewModel.Pages["HomeStep2"];
        }

        private async void LoadedHandler(object obj)
        {
            IsGoButtonEnabled = true;
            IsGoButtonInProgress = false;
            IsThereError = false;
            BuildingURL = "";
            ActionManager.ResetData();

            var response = await ActionManager.LoadDictionaries();
            if (response.IsError)
            {
                IsThereError = true;
                ErrorMsg = response.ErrorMsg;
                IsGoButtonEnabled = false;
                return;
            }
        }
        #endregion
    }
}
