using Grabcraft_Helper.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.ViewModels
{
    class HomeStep2ViewModel : ViewModelBase
    {
        public RelayCommand<object> SaveBtnClicked { get; set; }

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

        //TODO observable collection of mismatches


        private HowToHandleMismatch _howToHandleMismatch;

        public HowToHandleMismatch MismatchAlternative
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
        } // TODO enum binding









        private void SaveButtonClicked(object obj)
        {
            //do some model code
            //change view

        }


        public enum HowToHandleMismatch//TODO this enum shouldnt be here. It should be in the place thats common for model and viewmodel
        {
            Ignore,
            Red_Wool,
            Sign_with_text
        }

    }
}
