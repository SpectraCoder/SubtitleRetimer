using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;

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

        public static void StatusMessage(TextBlock textBlock, Color textColor, string message)
        {
            textBlock.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(textColor);
            textBlock.Text = message;
        }

        public static void StatusMessage(TextBlock textBlock, string message) => StatusMessage(textBlock, Colors.White, message);
    }
}
