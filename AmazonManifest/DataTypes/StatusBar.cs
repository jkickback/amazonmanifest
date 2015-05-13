using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonManifest.DataTypes
{
    public class StatusBar : INotifyPropertyChanged
    {
        private string _statusText;

        public string StatusText
        {
            get
            {
                return _statusText;
            }

            set
            {
                if (_statusText == value)
                {
                    return;
                }

                _statusText = value;
                RaisePropertyChanged("StatusText");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
