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
            MismatchesList = new MyObservableCollection<string>(nameof(MismatchesList), OnPropertyChanged);

            HowToHandleMismatch = HowToHandleMismatch.Ignore;
            //TODO this also should be in loaded event handler otherwise mismatches detection fails, because this starts at the fkin begning
            AreThereMismatches = ActionManager.AreThereMismatches;
            if (AreThereMismatches)
            {
                foreach (var item in ActionManager.MismatchesList)
                    MismatchesList.Add(item);
            }
            //haha, jokes on me, it needs to be in loaded handler
            BuildingName = ActionManager.BuildingName;
        }
        #endregion

        #region Fields
        private MainWindowViewModel mainWindowViewModel;
        #endregion

        #region Properties
        // Commands
        public RelayCommand<object> SaveButtonClicked { get; set; }
        public RelayCommand<object> SaveToMinecraftButtonClicked { get; set; }
        
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
        #endregion Properties

        #region Command Handlers
        private void SaveToMinecraftButtonClickedHandler(object obj)
        {
            MessageBox.Show($"{this.GetType().Name} Right now I do nothin");
            mainWindowViewModel.CurrentPage = mainWindowViewModel.Pages["HomeStep3"];
            //TODO, write internals
        }

        private void SaveButtonClickedHandler(object obj)
        {
            var response = ActionManager.AssembleMFunctionAndSave(HowToHandleMismatch);
            if(response.IsError)
            {
                //show some notification
                return;
            }
            mainWindowViewModel.CurrentPage = mainWindowViewModel.Pages["HomeStep3"];
        }
        #endregion
    }
}
