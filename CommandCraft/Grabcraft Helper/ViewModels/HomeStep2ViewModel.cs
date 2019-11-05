using Grabcraft_Helper.Handlers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grabcraft_Helper.CommonDataTypes;
using System.Windows;

namespace Grabcraft_Helper.ViewModels
{
    class HomeStep2ViewModel : ViewModelBase
    {
        #region Constructor
        public HomeStep2ViewModel()
        {
            SaveBtnClicked = new RelayCommand<object>(SaveButton);

            Mismatches = new ObservableCollection<string>();
            Mismatches.Add("12dkfsdfg9234jef");
            

            Mismatches2 = Mismatches;

            BuildingName = "ergifgj23904tjfbms90b0er9gm,09ergj09ergj\ndnf203f903m2f9023mf";
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

        public ObservableCollection<string> Mismatches;

        private ObservableCollection<string> _mismatches2;

        public ObservableCollection<string> Mismatches2
        {
            get { return _mismatches2; }
            set
            {
                _mismatches2 = value;
                OnPropertyChanged();
            }
        }


        private HowToHandleMismatch _mismatchAlternative;
        public HowToHandleMismatch MismatchAlternative
        {
            get
            {
                return _mismatchAlternative;
            }
            set
            {
                if (_mismatchAlternative == value)
                    return;

                _mismatchAlternative = value;
                OnPropertyChanged();
            }
        } // TODO enum binding

        public RelayCommand<object> SaveBtnClicked { get; set; }

        #endregion Properties

        #region Commands

        private void SaveButton(object obj)
        {
            Mismatches2.Add("12341dfgndf89gj43");
            
            //do some model code
            //change view

        }

        #endregion Commands

    }
}
