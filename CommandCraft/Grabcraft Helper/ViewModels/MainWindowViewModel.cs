using Grabcraft_Helper.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Grabcraft_Helper.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public UserControl CurrentPage { get; set; }

        public Dictionary<string, UserControl> Pages { get; }

        public MainWindowViewModel()
        {
            Pages = new Dictionary<string, UserControl>
            {
                {"HomeStep1", new HomeStep1() },
                {"HomeStep2", new HomeStep2() },
                {"HomeStep3", new HomeStep3() },
                {"Info", new Info() },
            };

            CurrentPage = Pages["HomeStep1"];
        }



        private void OnPropertyChanged([CallerMemberName] string memberName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
