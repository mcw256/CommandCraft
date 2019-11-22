using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected void OnPropertyChanged([CallerMemberName] string memberName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }

      
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
