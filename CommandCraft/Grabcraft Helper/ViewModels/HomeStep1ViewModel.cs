using Grabcraft_Helper;
using Grabcraft_Helper.ViewModels.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.ViewModels
{
    class HomeStep1ViewModel : ViewModelBase
    {
        #region Constructor
        public HomeStep1ViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            GoBtnClicked = new RelayCommand<object>(GoButtonClicked);

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

        }
        #endregion

    }
}
