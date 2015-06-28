using AmazonManifest.DataTypes;
using Excel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Input;
using System.Linq;
using System.ComponentModel;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using OfficeOpenXml;
using System.Windows.Media;
using System.Globalization;
using System.Threading;
using System.Windows.Threading;


namespace AmazonManifest.ViewModel
{

    public class MainViewModel : ViewModelBase 
    {
        private List<SpreadSheetRow> _rowList;
        private StatusBar _statusBar;
        private Totals _totalsBar;
        

        public string XlsFileName;

        public string BarCodeInput;

        public StatusBar StatusBar
        {
            get { return _statusBar; }
            set { _statusBar = value; }
        }
        
        public Totals TotalsBar
        {
            get { return _totalsBar; }
            set { _totalsBar = value; }
        }


        public List<SpreadSheetRow> Rows
        {
            get
            {
                return _rowList;
            }

            set
            {
                if (_rowList == value)
                {
                    return;
                }

                _rowList = value;
                RaisePropertyChanged("Rows");
            }
        }

        public ListView SpreadSheetGrid {get; set; }

        public TextBox BarcodeTextBox { get; set; }


        public List<string> Scans { get; set; }

        public List<string> SpreadsheetColumns { get; set; }

        private SpreadSheetRow SearchResult { get; set; }

        #region Commands
        public ICommand LoadSpreadsheet { get; private set; }

        public RelayCommand<string> SearchSheetCommand { get; private set; }

        public RelayCommand SaveDataCommand { get; private set; }

        public RelayCommand ChooseSpreadsheetCommand { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(ListView listview, TextBox barcodeBox)
        {
            StatusBar = new StatusBar();
            _statusBar.StatusText = "Start Scanning!";

            TotalsBar = new Totals();
            _totalsBar.TotalRows = 9999;
            _totalsBar.TotalFound = 9999;
            _totalsBar.NumberScans = 9999;

            Scans = new List<string>();
            Rows = new List<SpreadSheetRow>();
            SearchResult = new SpreadSheetRow();

            this.SpreadSheetGrid = listview;
            this.BarcodeTextBox = barcodeBox;

            ChooseSpreadsheet();
            this.SearchSheetCommand = new RelayCommand<string>(this.SearchSheet);
            this.SaveDataCommand = new RelayCommand(this.SaveData);
            this.ChooseSpreadsheetCommand = new RelayCommand(this.ChooseSpreadsheet);
           
        }

        public bool CanChooseSpreadsheetCommand()
        {
            return true;
        }
        public void ChooseSpreadsheet()
        {
            _statusBar.StatusText = "Please Choose a Spreadsheet.";
            //clear out cache, in case they're opening a new spreadsheet.
            _rowList = new List<SpreadSheetRow>();
            _totalsBar.NumberScans = 0;
            _totalsBar.TotalFound = 0;
            _totalsBar.TotalRows = 0;
            Scans = new List<string>();


            //Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".xlsx";
            dlg.Filter = "XLSX Files (*.xlsx)|*.xlsx";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();
            Console.WriteLine("");
            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                XlsFileName = dlg.FileName;
                Dialogs.ShowLongOperationDialog(new Action(() =>
                {
                    LoadSpreadsheetExecute();
                }), "Loading...");
                SpreadSheetGrid.ItemsSource = _rowList;
                _totalsBar.TotalRows = _rowList.Count;
                _statusBar.StatusText = "Waiting for you to Scan...";

                
            }
        }

        #region LOAD SPREADSHEET


        private void LoadSpreadsheetExecute()
        {
            DataSet SpreadsheetData; //Original Spreedsheet in DS form

            try
            {
                using (FileStream stream = File.Open(XlsFileName, FileMode.Open, FileAccess.Read))
                {
                    using (IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                    {
                        excelReader.IsFirstRowAsColumnNames = true;
                        DataSet result = excelReader.AsDataSet();
                        SpreadsheetData = result;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Unable to open spreadsheet!");
                return;
            }

            //SpreadsheetView.ItemsSource = SpreadsheetData.Tables[Properties.Settings.Default.SheetName].DefaultView;
            DataTable table = SpreadsheetData.Tables[0];
            
            //store the columns for later.
            SpreadsheetColumns = table.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList();  

            _rowList = new List<SpreadSheetRow>();
            int index = 1;
            foreach (DataRow row in table.Rows)
            {
                SpreadSheetRow newRow = new SpreadSheetRow();
                newRow.RowId = index;
                newRow.Asin = row["Asin"].ToString();
                newRow.UPC = row["UPC"].ToString();
                newRow.EAN = row["EAN"].ToString();
                newRow.LPN = row["LPN"].ToString();
                newRow.FCSKU = row["FCSKU"].ToString();
                newRow.ItemDesc = row["itemDesc"].ToString();

                #region "rows that we dont need to display/query"
                newRow.LiquidatorVendorCode = row["LiquidatorVendorCode"].ToString();
                newRow.InventoryLocation = row["InventoryLocation"].ToString();
                newRow.FC = row["FC"].ToString();
                newRow.IOG = row["IOG"].ToString();
                newRow.RemovalReason = row["RemovalReason"].ToString();
                newRow.ShipmentClosed = row["ShipmentClosed"].ToString();
                newRow.BOL = row["BOL"].ToString();
                newRow.Carrier = row["Carrier"].ToString();
                newRow.ShipToCity = row["ShipToCity"].ToString();
                newRow.RemovalOrderID = row["RemovalOrderID"].ToString();
                newRow.ReturnID = row["ReturnID"].ToString();
                newRow.ReturnItemID = row["ReturnItemID"].ToString();
                newRow.ShipmentRequestID = row["ShipmentRequestID"].ToString();
                newRow.PkgID = row["PkgID"].ToString();
                newRow.GL = row["GL"].ToString();
                newRow.GLDesc = row["GLDesc"].ToString();
                newRow.CategoryCode = row["CategoryCode"].ToString();
                newRow.CategoryDesc = row["CategoryDesc"].ToString();
                newRow.SubcatCode = row["SubcatCode"].ToString();
                newRow.SubcatDesc = row["SubcatDesc"].ToString();
                newRow.Units = row["Units"].ToString();
                newRow.ItemPkgWeight = row["ItemPkgWeight"].ToString();
                newRow.ItemPkgWeightUOM = row["ItemPkgWeightUOM"].ToString();
                newRow.CostSource = row["CostSource"].ToString();
                newRow.CurrencyCode = row["CurrencyCode"].ToString();
                newRow.UnitCost = row["UnitCost"].ToString();
                newRow.AmazonPrice = row["AmazonPrice"].ToString();
                newRow.UnitRecovery = row["UnitRecovery"].ToString();
                newRow.TotalCost = row["TotalCost"].ToString();
                newRow.TotalRecovery = row["TotalRecovery"].ToString();
                newRow.RecoveryRate = row["RecoveryRate"].ToString();
                newRow.RecoveryRateType = row["RecoveryRateType"].ToString();
                newRow.AdjTotalRecovery = row["AdjTotalRecovery"].ToString();
                newRow.AdjRecoveryRate = row["AdjRecoveryRate"].ToString();
                newRow.AdjReason = row["AdjReason"].ToString();
                newRow.FNSku = row["FNSku"].ToString();
                #endregion
                newRow.Found = false;
                newRow.Selected = false;
                newRow.FoundCount = 0;
                _rowList.Add(newRow);
                
                index++;
            }
            
        }
        #endregion

        #region SEARCH SHEET


        public bool CanSearchSheetCommand()
        {
            return true;
        }

        private void ResetRows()
        {
            _statusBar.StatusText = "Searching...";
            foreach (var row in _rowList)
            {
                row.Selected = false;
            }
        }

        private void AddScan(string scan)
        {
            if (!Scans.Contains(scan))
            {
                Scans.Add(scan);
                _totalsBar.NumberScans = Scans.Count;
            }
        }

        private void WriteXML()
        {
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(List<SpreadSheetRow>));

            string filename = XlsFileName.Replace(".xlsx", ".xml");
            System.IO.StreamWriter file = new System.IO.StreamWriter(filename);
            writer.Serialize(file, _rowList);
            file.Close();
        }

        private void HandleSearchResult(string barcodeText)
        {
            if (SearchResult != null)
            {
              
                SpreadSheetGrid.SelectedItem = SearchResult;
                Dispatcher.CurrentDispatcher.BeginInvoke(
                    DispatcherPriority.Send,
                    new Action(() => SpreadSheetGrid.ScrollIntoView(SearchResult))
                );
                
                _totalsBar.TotalFound = _rowList.Where(x => x.Found == true).Count();
                _statusBar.StatusText = barcodeText + " Found!";

            }
            else
            {
                StatusBar.StatusText = barcodeText + " Not Found!";
                //AddScan(barcodeText);
            }

            BarcodeTextBox.Focus();
            BarcodeTextBox.Select(0, BarcodeTextBox.Text.Length);
           
        }

        public void SearchSheet(string barcodeText)
        {
            
            Dialogs.ShowLongOperationDialog(new Action(() =>
            {
                
                ResetRows();

                //Search through columns U (ASIN), V (UPC), W (EAN), AP (LPN), X (FCSKU) and  AP (ANSKU).
                SearchResult = _rowList.Where(s => s.Asin == barcodeText
                    || s.UPC == barcodeText 
                    || s.EAN == barcodeText 
                    || s.LPN == barcodeText 
                    || s.FCSKU == barcodeText).FirstOrDefault();

                
                
                if (SearchResult != null)
                {
                    SearchResult.Selected = true;
                    SearchResult.Found = true;
                    SearchResult.FoundCount += 1;
                }
                else
                {
                    AddScan(barcodeText);
                }

                //WriteXML(); //disabled for now becuase I'm lazy.
                //Thread.Sleep(1000);
            }), "Searching...");

            HandleSearchResult(barcodeText);
            
        }
        #endregion

        #region Output Data
        public bool CanSaveDataCommand()
        {
            return true;
        }
        
        public void SaveData()
        {
            _statusBar.StatusText = "Saving Output to " + XlsFileName;

            Dialogs.ShowLongOperationDialog(new Action(() =>
            {
                ExcelPackage pck = new ExcelPackage(new FileInfo(XlsFileName));
                ExcelWorksheet workSheet = pck.Workbook.Worksheets[1];

                var start = workSheet.Dimension.Start;
                var end = workSheet.Dimension.End;

                //Write our count header.
                workSheet.Cells["BA1"].Value = "Num Times Found";

                foreach (SpreadSheetRow row in Rows)
                {
                    //Found the barcode on the spreadsheet, highlight it green
                    if (row.Found == true)
                    {

                        workSheet.Row(row.RowId + 1).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        workSheet.Row(row.RowId + 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(176,255,122));
                    }
                    else
                    {
                        workSheet.Row(row.RowId + 1).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        workSheet.Row(row.RowId + 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(255,122,122));
                    }

                    workSheet.Cells["BA" + (row.RowId + 1)].Value = row.FoundCount;
                }


                if (pck.Workbook.Worksheets["Scanned But Not Found"] == null)
                {
                    //Create new worksheet to save everything they scanned, but didn't find on the spreadsheet
                    pck.Workbook.Worksheets.Add("Scanned But Not Found");
                }

                ExcelWorksheet scannedWorksheet = pck.Workbook.Worksheets["Scanned But Not Found"];
                int i = 0;
                foreach (string code in Scans)
                { 
                    string coords = string.Format("A{0}", (i + 1));
                    scannedWorksheet.Cells[coords].Value = code;
                    i++;
                }


                pck.Save();
            }), "Saving...");
            BarcodeTextBox.Focus();
            BarcodeTextBox.Select(0, BarcodeTextBox.Text.Length);
            _statusBar.StatusText = "Successfully Saved " + XlsFileName;

        }
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}