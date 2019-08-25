using Grabcraft_Helper.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.ViewModels
{
    class HomeStep1ViewModel : ViewModelBase
    {
        public HomeStep1ViewModel()
        {
            GoBtnClicked = new RelayCommand<object>(GoButtonClicked);
        }


        public RelayCommand<object> GoBtnClicked { get; set; }

        private string _buildingURL;

        public string BuildingURL
        {
            get
            {
                return _buildingURL;
            }
            set
            {
                if (_buildingURL == value)
                    return;

                _buildingURL = value;
                OnPropertyChanged();
            }
        }


        private void GoButtonClicked(object obj)
        {
            //run some model code
            //change page

        }



    }
}
