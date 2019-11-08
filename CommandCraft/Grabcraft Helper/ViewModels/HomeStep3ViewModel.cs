using Grabcraft_Helper;
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
            _mainWindowViewModel = mainWindowViewModel;
            OkBtnClicked = new RelayCommand<object>(OkButtonClicked);
        }
        #endregion

        #region Properties
        private MainWindowViewModel _mainWindowViewModel;
        public RelayCommand<object> OkBtnClicked { get; set; }

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

        #region Commands
        public void OkButtonClicked(object obj)
        {
            _mainWindowViewModel.CurrentPage = _mainWindowViewModel.Pages["HomeStep1"];
        }
        #endregion
    }
}
