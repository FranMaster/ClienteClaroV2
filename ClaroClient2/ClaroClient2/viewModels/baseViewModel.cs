using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ClaroClient2.viewModels
{
    public class baseViewModel : INotifyPropertyChanged
    {
        public void onPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
