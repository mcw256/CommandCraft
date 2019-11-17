using Grabcraft_Helper;
using Grabcraft_Helper.ViewModels.Misc;
using Grabcraft_Helper.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Grabcraft_Helper.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Constructor
        public MainWindowViewModel()
        {
            Pages = new Dictionary<string, UserControl>
            {
                {"HomeStep1", new HomeStep1(this) },
                {"HomeStep2", new HomeStep2(this) },
                {"HomeStep3", new HomeStep3(this) },
                {"Info", new Info() },
            };
            HomeButtonClicked = new RelayCommand<object>(HomeButtonClickedHandler);
            InfoButtonClicked = new RelayCommand<object>(InfoButtonClickedHandler);
            Loaded = new RelayCommand<object>(LoadedHandler);
        }
        #endregion

        #region Properties
        //Commands
        public RelayCommand<object> HomeButtonClicked { get; set; }
        public RelayCommand<object> InfoButtonClicked { get; set; }
        public RelayCommand<object> Loaded { get; set; }

        //Actual properties
        public Dictionary<string, UserControl> Pages { get; }

        private UserControl _currentPage;
        public UserControl CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                if (_currentPage == value)
                    return;

                _currentPage = value;
                OnPropertyChanged();
            }
        }

        private bool _isHomeButtonActive;
        public bool IsHomeButtonActive // when its active, xamls enable prop is meant to be set on false, thats why this code gets little bit confusing
        {
            get
            {
                return _isHomeButtonActive;
            }
            set
            {
                if (value == true) //set all the others
                {
                    IsInfoButtonActive = false;

                }

                if (_isHomeButtonActive == !value)
                    return;

                _isHomeButtonActive = !value; // this one exclamation mark i
                if (value == true)
                    IsInfoButtonActive = false;

                OnPropertyChanged();
            }
        }

        private bool _isInfoButtonActive;
        public bool IsInfoButtonActive
        {
            get
            {
                return _isInfoButtonActive;
            }
            set
            {
                if (value == true)//set all the others on false
                {
                    IsHomeButtonActive = false;

                }
                if (_isInfoButtonActive == !value)
                    return;

                _isInfoButtonActive = !value;
                
                OnPropertyChanged();
            }
        } 
        #endregion

        #region Command Handlers
        private void HomeButtonClickedHandler(object obj)
        {
            CurrentPage = Pages["HomeStep1"];
            IsHomeButtonActive = true;
        }

        private void InfoButtonClickedHandler(object obj)
        {
            CurrentPage = Pages["Info"];
            IsInfoButtonActive = true;
        }

        private void LoadedHandler(object obj)
        {
            CurrentPage = Pages["HomeStep1"];
            IsHomeButtonActive = true;
        }

        #endregion
    }
}
