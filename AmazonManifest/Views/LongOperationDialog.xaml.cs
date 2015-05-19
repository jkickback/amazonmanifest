using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace AmazonManifest.Views
{
    /// <summary>
    /// Interaction logic for LongOperationDialog.xaml
    /// </summary>
    public partial class LongOperationDialog : Window
    {
        public Exception Error { get; set; }

        private Action _longOperation;

        public LongOperationDialog(Action longOperation)
        {
            InitializeComponent();

            _longOperation = longOperation;

            var worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if( e.Error != null )
            {
                Error = e.Error;
                DialogResult = false;
            }
            else
            {
                DialogResult = true;
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _longOperation();
        }


    }
}
