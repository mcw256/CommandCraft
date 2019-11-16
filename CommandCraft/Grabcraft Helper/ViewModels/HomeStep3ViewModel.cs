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
        public HomeStep3ViewModel(MainWindowViewModel _mainWindowViewModel)
        {
            this.mainWindowViewModel = _mainWindowViewModel;
            OkButtonClicked = new RelayCommand<object>(OkButtonClickedHandler);

            BuildingName = ActionManager.BuildingName;
        }
        #endregion

        #region Fields
        private MainWindowViewModel mainWindowViewModel;
        #endregion

        #region Properties
        //Commands
        public RelayCommand<object> OkButtonClicked { get; set; }

        //Actual properties
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
        #endregion

        #region Command Handlers
        public void OkButtonClickedHandler(object obj)
        {
            mainWindowViewModel.CurrentPage = mainWindowViewModel.Pages["HomeStep1"];
        }
        #endregion
    }
}
