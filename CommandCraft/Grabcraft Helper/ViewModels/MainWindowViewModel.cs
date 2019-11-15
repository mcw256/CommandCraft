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

            CurrentPage = Pages["HomeStep1"];
            IsHomeBtnActive = true;

            HomeBtnClicked = new RelayCommand<object>(HomeButton);
            InfoBtnClicked = new RelayCommand<object>(InfoButton);
        }
        #endregion

        #region Properties
        private bool _isHomeBtnActive;
        public bool IsHomeBtnActive // when its active, xamls enable prop is meant to be set on false, thats why this code gets little bit confusing
        {
            get
            {
                return _isHomeBtnActive;
            }
            set
            {
                if (value == true) //set all the others
                {
                    IsInfoBtnActive = false;

                }

                if (_isHomeBtnActive == !value)
                    return;

                _isHomeBtnActive = !value; // this one exclamation mark i
                if (value == true)
                    IsInfoBtnActive = false;

                OnPropertyChanged();
            }
        }

        private bool _isInfoBtnActive;
        public bool IsInfoBtnActive
        {
            get
            {
                return _isInfoBtnActive;
            }
            set
            {
                if (value == true)//set all the others on false
                {
                    IsHomeBtnActive = false;

                }
                if (_isInfoBtnActive == !value)
                    return;

                _isInfoBtnActive = !value;
                
                OnPropertyChanged();
            }
        }

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

        public Dictionary<string, UserControl> Pages { get; }

        public RelayCommand<object> HomeBtnClicked { get; set; }

        public RelayCommand<object> InfoBtnClicked { get; set; }

        #endregion

        #region Commands
        private void HomeButton(object obj)
        {
            CurrentPage = Pages["HomeStep1"];
            IsHomeBtnActive = true;
        }

        private void InfoButton(object obj)
        {
            CurrentPage = Pages["Info"];
            IsInfoBtnActive = true;
        }

        #endregion



    }
}
