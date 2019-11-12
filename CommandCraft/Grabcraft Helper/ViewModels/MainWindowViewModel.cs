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
            IsInfoBtnDimmed = true;

            HomeBtnClicked = new RelayCommand<object>(HomeButton);
            InfoBtnClicked = new RelayCommand<object>(InfoButton);
        }
        #endregion

        #region Properties
        private bool _isHomeBtnDimmed;
        public bool IsHomeBtnDimmed
        {
            get
            {
                return _isHomeBtnDimmed;
            }
            set
            {
                if (_isHomeBtnDimmed == value)
                    return;


                _isHomeBtnDimmed = value;
                if (value == false)
                    IsInfoBtnDimmed = true;

                OnPropertyChanged();
            }
        }

        private bool _isInfoBtnDimmed;
        public bool IsInfoBtnDimmed
        {
            get
            {
                return _isInfoBtnDimmed;
            }
            set
            {
                if (_isInfoBtnDimmed == value)
                    return;

                _isInfoBtnDimmed = value;
                if (value == false)
                    IsHomeBtnDimmed = true;

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
            IsHomeBtnDimmed = false;
        }

        private void InfoButton(object obj)
        {
            CurrentPage = Pages["Info"];
            IsInfoBtnDimmed = false;
        }

        #endregion



    }
}
