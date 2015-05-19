using AmazonManifest.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonManifest
{
    public class Dialogs
    {
        public static void ShowError(string text)
        {
            var dialog = new MessageDialog("Error", text);
            dialog.ShowDialog();
        }

        public static void ShowMessage(string header, string text)
        {
            var dialog = new MessageDialog(header, text);
            dialog.ShowDialog();
        }



        public static void ShowLongOperationDialog(Action longOperation)
        {
            var dialog = new LongOperationDialog(longOperation);
            var dialogResult = dialog.ShowDialog();

            if (dialogResult != true)
            {
                throw dialog.Error;
            }
        }

    }
}
