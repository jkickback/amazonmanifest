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
using AmazonManifest.ViewModel;
using System.Reflection;

namespace AmazonManifest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            Title = string.Format("Amazon Manifest Tool v{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
            DataContext = new MainViewModel(SpreadSheetGrid, BarcodeInput);
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!BarcodeInput.IsFocused)
            {
                BarcodeInput.Focus();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!BarcodeInput.IsFocused)
            {
                BarcodeInput.Focus();
            }
            
        }

        
    }
}
