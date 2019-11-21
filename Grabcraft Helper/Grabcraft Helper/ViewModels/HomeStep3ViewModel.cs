using Grabcraft_Helper;
using Grabcraft_Helper.Model;
using Grabcraft_Helper.ViewModels.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.ViewModels
{
    class HomeStep3ViewModel : ViewModelBase
    {
        #region Constructor
        public HomeStep3ViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this._mainWindowViewModel = mainWindowViewModel;
            OkButtonClicked = new RelayCommand<object>(OkButtonClickedHandler);
            Loaded = new RelayCommand<object>(LoadedHandler);   
        }
        #endregion

        #region Fields
        private MainWindowViewModel _mainWindowViewModel;
        #endregion

        #region Properties
        //Commands
        public RelayCommand<object> OkButtonClicked { get; set; }
        public RelayCommand<object> Loaded { get; set; }

        //Actual properties
        private string _buildingName;
        public string BuildingName
        {
            get => _buildingName;
            set
            {
                if (_buildingName == value)
                    return;

                _buildingName = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Command Handlers
        private void OkButtonClickedHandler(object obj)
        {
            _mainWindowViewModel.CurrentPage = _mainWindowViewModel.Pages["HomeStep1"];
        }

        private void LoadedHandler(object obj)
        {
            BuildingName = ActionManager.BuildingName;
        }

        #endregion
    }
}
