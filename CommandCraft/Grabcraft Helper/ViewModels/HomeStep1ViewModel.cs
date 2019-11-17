﻿using Grabcraft_Helper;
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
        private async void GoButtonClickedHandler(object obj)
        {
            var response = await ActionManager.DownloadAndProcessBuilding(BuildingURL);
            if (response.IsError)
            {
                //TODO guierror
                return;
            }
            mainWindowViewModel.CurrentPage = mainWindowViewModel.Pages["HomeStep2"];
        }

        private async void LoadedHandler(object obj)
        {
            var response = await ActionManager.LoadDictionaries();
            if (response.IsError)
            {
                // TODO guierror
                return;
            }
        }
        #endregion
    }
}
