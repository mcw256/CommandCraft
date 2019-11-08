using Grabcraft_Helper;
using Grabcraft_Helper.ViewModels.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.ViewModels
{
    class HomeStep3ViewModel : ViewModelBase
    {
        public HomeStep3ViewModel()
        {
            OkBtnClicked = new RelayCommand<object>(OkButtonClicked);
        }


        public RelayCommand<object> OkBtnClicked { get; set; }


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


        public void OkButtonClicked(object obj)
        {
            //change page
        }

    }
}
