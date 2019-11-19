using Grabcraft_Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grabcraft_Helper.DataTypes;
using System.Windows;
using System.Collections.Specialized;
using Grabcraft_Helper.ViewModels.Misc;
using Grabcraft_Helper.Model;

namespace Grabcraft_Helper.ViewModels
{
    class HomeStep2ViewModel : ViewModelBase
    {
        #region Constructor
        public HomeStep2ViewModel(MainWindowViewModel _mainWindowViewModel)
        {
            this.mainWindowViewModel = _mainWindowViewModel;
            SaveButtonClicked = new RelayCommand<object>(SaveButtonClickedHandler);
            SaveToMinecraftButtonClicked = new RelayCommand<object>(SaveToMinecraftButtonClickedHandler);
            Loaded = new RelayCommand<object>(LoadedHandler);
            SaveSelectingDialogButtonClicked = new RelayCommand<object>(SaveSelectingDialogButtonClickedHandler);
            MismatchesList = new MyObservableCollection<string>(nameof(MismatchesList), OnPropertyChanged);   
            PlayerSavesList = new MyObservableCollection<string>(nameof(PlayerSavesList), OnPropertyChanged);   
        }
        #endregion

        #region Fields
        private MainWindowViewModel mainWindowViewModel;
        private bool dialogButtonClicked = false;
        #endregion

        #region Properties
        // Commands
        public RelayCommand<object> SaveButtonClicked { get; set; }
        public RelayCommand<object> SaveToMinecraftButtonClicked { get; set; }
        public RelayCommand<object> Loaded { get; set; }
        public RelayCommand<object> SaveSelectingDialogButtonClicked { get; set; }
        
        // Actual properties
        private string _buildingName;
        public string BuildingName
        {
            get
            {
                return _buildingName;
            }
            set
            {
                if (_buildingName == value)
                    return;

                _buildingName = value;
                OnPropertyChanged();
            }
        }

        private MyObservableCollection<string> _mismatchesList;
        public MyObservableCollection<string> MismatchesList
        {
            get { return _mismatchesList; }
            set
            {
                _mismatchesList = value;
                OnPropertyChanged();
                
            }
        }

        private HowToHandleMismatch _howToHandleMismatch;
        public HowToHandleMismatch HowToHandleMismatch
        {
            get
            {
                return _howToHandleMismatch;
            }
            set
            {
                if (_howToHandleMismatch == value)
                    return;

                _howToHandleMismatch = value;
                OnPropertyChanged();
            }
        }

        private bool _areThereMismatches;
        public bool AreThereMismatches
        {
            get
            {
                return _areThereMismatches;
            }
            set
            {
                if (value == _areThereMismatches) return;

                _areThereMismatches = value;
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

        private bool _isDialogHostOpen;
        public bool IsDialogHostOpen
        {
            get
            { 
                return _isDialogHostOpen;
            }
            set
            {
                if (_isDialogHostOpen == value)
                    return;

                _isDialogHostOpen = value;
                OnPropertyChanged();
            }
        }

        private bool _isSaveToMinecraftAvailable;
        public bool IsSaveToMinecraftAvailable
        {
            get
            {
                return _isSaveToMinecraftAvailable;
            }
            set
            {
                if (_isSaveToMinecraftAvailable == value)
                    return;

                _isSaveToMinecraftAvailable = value;
                OnPropertyChanged();
            }
        }

        private MyObservableCollection<string> _playerSavesList;
        public MyObservableCollection<string> PlayerSavesList
        {
            get { return _playerSavesList; }
            set
            {
                _playerSavesList = value;
                OnPropertyChanged();

            }
        }

        private string _selectedSave;
        public string SelectedSave
        {
            get
            {
                return _selectedSave;
            }
            set
            {
                if (_selectedSave == value)
                    return;

                _selectedSave = value;
                OnPropertyChanged();
            }
        }

        private bool _areButtonsEnabled;

        public bool AreButtonsEnabled
        {
            get
            {
                return _areButtonsEnabled;
            }
            set
            {
                if (_areButtonsEnabled == value)
                    return;

                _areButtonsEnabled = value;
                OnPropertyChanged();
            }
        }



        #endregion Properties

        #region Command Handlers
        private void SaveToMinecraftButtonClickedHandler(object obj)
        {
            IsDialogHostOpen = true;
        } 

        private async void SaveButtonClickedHandler(object obj)
        {
            IsDialogHostOpen = true;
            AreButtonsEnabled = false;

            var response =  await ActionManager.AssembleMFunctionAndSave(HowToHandleMismatch);
            if(response.IsError)
            {
                IsThereError = true;
                ErrorMsg = response.ErrorMsg;
                return;
            }
            if (response.ErrorMsg == "user canceled")
            {
                AreButtonsEnabled = true;
                return;
            }
            
            mainWindowViewModel.CurrentPage = mainWindowViewModel.Pages["HomeStep3"];
        }

        private async void SaveSelectingDialogButtonClickedHandler(object obj)
        {
            AreButtonsEnabled = false;
            var response = await ActionManager.AssembleMFunctionAndSaveToMinecraft(HowToHandleMismatch, SelectedSave);
            if (response.IsError)
            {
                IsThereError = true;
                ErrorMsg = response.ErrorMsg;
                return;
            }
            
            mainWindowViewModel.CurrentPage = mainWindowViewModel.Pages["HomeStep3"];
        }

        private void LoadedHandler(object obj)
        {
            HowToHandleMismatch = HowToHandleMismatch.Ignore;
            IsDialogHostOpen = false;
            IsThereError = false;
            IsSaveToMinecraftAvailable = true;
            AreButtonsEnabled = true;
            MismatchesList.Clear();
            PlayerSavesList.Clear();

            SelectedSave = ActionManager.DefaultPlayerSave == "" ? null : ActionManager.DefaultPlayerSave;
            BuildingName = ActionManager.BuildingName;
            IsSaveToMinecraftAvailable = ActionManager.IsSaveToMinecraftAvailable;

            AreThereMismatches = ActionManager.AreThereMismatches;
            if (AreThereMismatches)
            {
                foreach (var item in ActionManager.MismatchesList)
                    MismatchesList.Add(item);
            }

            if (ActionManager.PlayerSavesList.Count >= 1)
            {
                foreach (var item in ActionManager.PlayerSavesList)
                    PlayerSavesList.Add(item);
            }
 
        }
        #endregion
    }
}
