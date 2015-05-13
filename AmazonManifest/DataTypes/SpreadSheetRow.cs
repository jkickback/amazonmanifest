using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonManifest.DataTypes
{
    public class SpreadSheetRow : INotifyPropertyChanged
    {
        private int _rowId;
        private string _asin;
        private string _upc;
        private string _ean;
        private string _lpn;
        private string _fcsku;
        //private string _ansku;
        private string _itemdesc;

        private bool _selected;

        public int RowId 
        { 
            get
            {
                return _rowId;
            }
            set
            {
                _rowId = value;
                OnPropertyChanged("RowId");
            }
        }

        public string Asin 
        {
            get
            {
                return _asin;
            }
            set
            {
                _asin = value;
                OnPropertyChanged("Asin");
            }
        }


        public string UPC 
        {
            get
            {
                return _upc;
            }
            set
            {
                _upc = value;
                OnPropertyChanged("UPC");
            }    
        }

        public string EAN 
        {
            get
            {
                return _ean;
            }
            set
            {
                _ean = value;
                OnPropertyChanged("EAN");
            }   
        }

        public string LPN 
        {
            get
            {
                return _lpn;
            }
            set
            {
                _lpn = value;
                OnPropertyChanged("LPN");
            }   
        }

        public string FCSKU 
        {
            get
            {
                return _fcsku;
            }
            set
            {
                _fcsku = value;
                OnPropertyChanged("FCSKU");
            }  
        }

        //public string ANSKU 
        //{
        //    get
        //    {
        //        return _ansku;
        //    }
        //    set
        //    {
        //        _ansku = value;
        //        OnPropertyChanged("ANSKU");
        //    } 
        //}
        
        public string ItemDesc 
        {
            get
            {
                return _itemdesc;
            }
            set
            {
                _itemdesc = value;
                OnPropertyChanged("ItemDesc");
            } 
        }

        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                OnPropertyChanged("Selected");
            }
        }


        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion


    }


}
