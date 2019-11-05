using Grabcraft_Helper.Handlers;
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
    class MainWindowViewModel : ViewModelBase
    {
        #region Constructor
        public MainWindowViewModel()
        {
            Pages = new Dictionary<string, UserControl>
            {
                {"HomeStep1", new HomeStep1() },
                {"HomeStep2", new HomeStep2() },
                {"HomeStep3", new HomeStep3() },
                {"Info", new Info() },
            };

            CurrentPage = Pages["HomeStep2"];

            IsHomeBtnEnabled = false;
            IsInfoBtnEnabled = true;

            HomeBtnClicked = new RelayCommand<object>(HomeButton);
            InfoBtnClicked = new RelayCommand<object>(InfoButton);

        }
        #endregion

        #region Properties
        private bool _isHomeBtnEnabled;
        public bool IsHomeBtnEnabled
        {
            get
            {
                return _isHomeBtnEnabled;
            }
            set
            {
                if (_isHomeBtnEnabled == value)
                    return;


                _isHomeBtnEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _isInfoBtnEnabled;
        public bool IsInfoBtnEnabled
        {
            get
            {
                return _isInfoBtnEnabled;
            }
            set
            {
                if (_isInfoBtnEnabled == value)
                    return;

                _isInfoBtnEnabled = value;
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
            IsHomeBtnEnabled = false;
            IsInfoBtnEnabled = true;

        }

        private void InfoButton(object obj)
        {
            CurrentPage = Pages["Info"];
            IsHomeBtnEnabled = true;
            IsInfoBtnEnabled = false;
            MessageBox.Show("Whatever");
        }

        #endregion



    }
}
