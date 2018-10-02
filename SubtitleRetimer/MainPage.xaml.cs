using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace SubtitleRetimer{
   
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            LoadIcon(AppIcon, "ms-appx:///Assets/AppIconBlack.png", "ms-appx:///Assets/AppIconWhite.png");
            LoadAppbarIcon(ButtonAbout, "ms-appx:///Assets/IconInfoBlack.png", "ms-appx:///Assets/IconInfoWhite.png");
            PageArea.Navigate(typeof(Home));
        }       

        public static void LoadIcon(Image image, string darkIconUri, string lightIconUri)
        {            
            bool isLight = Application.Current.RequestedTheme == ApplicationTheme.Light;

            BitmapImage bitmap = new BitmapImage();            

            if (isLight)
            {
                bitmap.UriSource = new Uri(darkIconUri);
            }
            else
            {
                bitmap.UriSource = new Uri(lightIconUri);
            }

            image.Source = bitmap;
            
        }

        public static void LoadAppbarIcon(AppBarButton button, string darkIconUri, string lightIconUri)
        {
            bool isLight = Application.Current.RequestedTheme == ApplicationTheme.Light;

            BitmapIcon bitmap = new BitmapIcon();

            if (isLight)
            {
                bitmap.UriSource = new Uri(darkIconUri);
            }
            else
            {
                bitmap.UriSource = new Uri(lightIconUri);
            }

            button.Icon = bitmap;

        }

        private void ButtonAbout_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PageArea.Navigate(typeof(About));
        }

    }
}
