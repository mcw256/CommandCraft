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
        public HomeStep2ViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            SaveBtnClicked = new RelayCommand<object>(SaveButtonClicked);
            SaveToMinecraftBtnClicked = new RelayCommand<object>(SaveToMinecraftButtonClicked);

            MismatchesList = new MyObservableCollection<string>(nameof(MismatchesList), OnPropertyChanged);
            HowToHandleMismatch = HowToHandleMismatch.Ignore;

            AreThereMismatches = ActionManager.AreThereMismatches;
            if(AreThereMismatches)
            {
                foreach (var item in ActionManager.MismatchesList)
                    MismatchesList.Add(item);
            }

            

        }
        #endregion

        #region Properties
        private MainWindowViewModel _mainWindowViewModel;
        public RelayCommand<object> SaveBtnClicked { get; set; }
        public RelayCommand<object> SaveToMinecraftBtnClicked { get; set; }
        
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


        #endregion Properties

        #region Commands
        private void SaveToMinecraftButtonClicked(object obj)
        {
            MessageBox.Show($"{this.GetType().Name} Right now I do nothin");
            _mainWindowViewModel.CurrentPage = _mainWindowViewModel.Pages["HomeStep3"];



        }

        private void SaveButtonClicked(object obj)
        {
            ActionManager.AssembleMFunctionAndSave(HowToHandleMismatch);
            _mainWindowViewModel.CurrentPage = _mainWindowViewModel.Pages["HomeStep3"];


        }

        #endregion Commands



    }
}
