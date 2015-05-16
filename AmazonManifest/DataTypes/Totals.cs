using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonManifest.DataTypes
{
    public class Totals : INotifyPropertyChanged
    {
        private int _totalRows;
        private int _totalFound;
        private int _numberScans;

        public int TotalRows
        {
            get
            {
                return _totalRows;
            }

            set
            {
                if (_totalRows == value)
                {
                    return;
                }

                _totalRows = value;
                RaisePropertyChanged("TotalRows");
            }
        }

        public int TotalFound
        {
            get
            {
                return _totalFound;
            }

            set
            {
                if (_totalFound == value)
                {
                    return;
                }

                _totalFound = value;
                RaisePropertyChanged("TotalFound");
            }
        }

        public int NumberScans
        {
            get
            {
                return _numberScans;
            }

            set
            {
                if (_numberScans == value)
                {
                    return;
                }

                _numberScans = value;
                RaisePropertyChanged("NumberScans");
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
