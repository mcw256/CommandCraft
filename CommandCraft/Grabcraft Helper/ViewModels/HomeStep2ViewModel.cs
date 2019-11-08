using Grabcraft_Helper.Handlers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grabcraft_Helper.CommonDataTypes;
using System.Windows;
using System.Collections.Specialized;

namespace Grabcraft_Helper.ViewModels
{
    class HomeStep2ViewModel : ViewModelBase
    {
        #region Constructor
        public HomeStep2ViewModel()
        {
            SaveBtnClicked = new RelayCommand<object>(SaveButton);
            Mismatches2 = new MyObservableCollection<string>(nameof(Mismatches2), OnPropertyChanged);
            HowToHandleMismatch = HowToHandleMismatch.Ignore;
        }
        #endregion

        #region Properties

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

        private void SaveButton(object obj)
        {
           //save stuff here
        }

        #endregion Commands



    }
}
