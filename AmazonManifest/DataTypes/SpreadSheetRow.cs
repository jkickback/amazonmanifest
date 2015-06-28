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

        #region Private Members
        private int _rowId;
        private string _asin;
        private string _upc;
        private string _ean;
        private string _lpn;
        private string _fcsku;
        private string _itemdesc;
        private bool _selected;
        private bool _found;
        private int _foundCount;
        #endregion

        #region Public Grid Members

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

        #endregion

        #region Public Spreadsheet-only Members
        public string LiquidatorVendorCode { get; set; }
        public string InventoryLocation { get; set; }
        public string FC { get; set; }
        public string IOG { get; set; }
        public string RemovalReason { get; set; }
        public string ShipmentClosed { get; set; }
        public string BOL { get; set; }
        public string Carrier { get; set; }
        public string ShipToCity { get; set; }
        public string RemovalOrderID { get; set; }
        public string ReturnID { get; set; }
        public string ReturnItemID { get; set; }
        public string ShipmentRequestID { get; set; }
        public string PkgID { get; set; }
        public string GL { get; set; }
        public string GLDesc { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryDesc { get; set; }
        public string SubcatCode { get; set; }
        public string SubcatDesc { get; set; }
        public string Units { get; set; }
        public string ItemPkgWeight { get; set; }
        public string ItemPkgWeightUOM { get; set; }
        public string CostSource { get; set; }
        public string CurrencyCode { get; set; }
        public string UnitCost { get; set; }
        public string AmazonPrice { get; set; }
        public string UnitRecovery { get; set; }
        public string TotalCost { get; set; }
        public string TotalRecovery { get; set; }
        public string RecoveryRate { get; set; }
        public string RecoveryRateType { get; set; }
        public string AdjTotalRecovery { get; set; }
        public string AdjRecoveryRate { get; set; }
        public string AdjReason { get; set; }
        public string FNSku { get; set; }

        #endregion

        #region Public Helper Members
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

        public bool Found
        {
            get
            {
                return _found;
            }
            set
            {
                _found = value;
                OnPropertyChanged("Found");
            }
        }

        public int FoundCount
        {
            get
            {
                return _foundCount;
            }
            set
            {
                _foundCount = value;
                OnPropertyChanged("FoundCount");
            }
        }

        #endregion

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
