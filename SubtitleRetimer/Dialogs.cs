using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace SubtitleRetimer
{
    class Dialogs
    {
        public static async Task ErrorDialog(string errorTitle, string errorMessage)
        {
            ContentDialog errorDialog = new ContentDialog()
            {
                Title = errorTitle,
                Content = errorMessage,
                CloseButtonText = "OK"
            };
            await errorDialog.ShowAsync();
        }

        public static Task ErrorDialog(string errorMessage) => ErrorDialog("Error", errorMessage);        
    }
}
