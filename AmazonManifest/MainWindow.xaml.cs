using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AmazonManifest.DataTypes;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace AmazonManifest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _xlsFileName;
        public DataSet SpreadsheetData; //Original Spreedsheet in DS form
        public ObservableCollection<SpreadSheetRow> SpreadsheetResults;  //View with certain columns that user sees
        public string ScannedText;

        public MainWindow()
        {
            InitializeComponent();
            SpreadsheetResults = new ObservableCollection<SpreadSheetRow>();
            SpreadsheetResults.CollectionChanged += SpreadsheetResults_CollectionChanged;

        }

        public void SpreadsheetResults_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SpreadsheetView.ItemsSource = SpreadsheetResults;
        }


        #region File Handlers
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".xlsx";
            dlg.Filter = "XLSX Files (*.xlsx)|*.xlsx";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                _xlsFileName = dlg.FileName;
            }

            _loadSpreadsheet();

        }

        private void _loadSpreadsheet()
        {

            using (FileStream stream = File.Open(_xlsFileName, FileMode.Open, FileAccess.Read))
            { 
                using (IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                {
                    excelReader.IsFirstRowAsColumnNames = true;
                    DataSet result = excelReader.AsDataSet();
                    SpreadsheetData = result;
                }
            }

            //SpreadsheetView.ItemsSource = SpreadsheetData.Tables[Properties.Settings.Default.SheetName].DefaultView;
            DataTable table = SpreadsheetData.Tables[Properties.Settings.Default.SheetName];
            int index = 1;
            foreach (DataRow row in table.Rows)
            {
                SpreadSheetRow newRow = new SpreadSheetRow(row, index);
                SpreadsheetResults.Add(newRow);
                index++;
            }


        }
        #endregion


        private void SearchDataset()
        {
            //Search through columns U (ASIN), V (UPC), W (EAN), AP (LPN), X (FCSKU) and  AP (ANSKU).


        }

    }
}
