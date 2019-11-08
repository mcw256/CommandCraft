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

namespace Grabcraft_Helper.ViewModels
{
    class HomeStep2ViewModel : ViewModelBase
    {
        #region Constructor
        public HomeStep2ViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            SaveBtnClicked = new RelayCommand<object>(SaveButtonClicked);
            Mismatches2 = new MyObservableCollection<string>(nameof(Mismatches2), OnPropertyChanged);
            HowToHandleMismatch = HowToHandleMismatch.Ignore;
        }
        #endregion

        #region Properties
        private MainWindowViewModel _mainWindowViewModel;

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

        private MyObservableCollection<string> _mismatches2;
        public MyObservableCollection<string> Mismatches2
        {
            get { return _mismatches2; }
            set
            {
                _mismatches2 = value;
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

        public RelayCommand<object> SaveBtnClicked { get; set; }

        #endregion Properties

        #region Commands
        private void SaveButtonClicked(object obj)
        {
            _mainWindowViewModel.CurrentPage = _mainWindowViewModel.Pages["HomeStep3"];
        }

        #endregion Commands



    }
}
