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

namespace SubtitleRetimer
{
    public sealed partial class Home : Page
    {        
        public Home()
        {
            this.InitializeComponent();
        }        
         
        private async void ButtonLoadSrt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await Importer.LoadTextFile();                             
            }
            catch (Exception)
            {
                Parameters.ViewModel.Status.StatusMessage = "No file loaded.";                
            }  

        }        

        private async void ButtonExport_Click(object sender, RoutedEventArgs e)
        {
            if (Parameters.SubtitleList.Count != 0)
            {
                try
                {

                    if (ComboBoxMath.SelectedIndex == 0) //add
                    {
                        if (ComboBoxTime.SelectedIndex == 0) { Exporter.Add(Parameters.SubtitleList, int.Parse(TextBoxInput.Text)); } //add milliseconds
                        if (ComboBoxTime.SelectedIndex == 1) { Exporter.Add(Parameters.SubtitleList, int.Parse(TextBoxInput.Text) * 1000); } //add seconds
                        if (ComboBoxTime.SelectedIndex == 2) { Exporter.Add(Parameters.SubtitleList, int.Parse(TextBoxInput.Text) * 60000); } //add minutes
                        if (ComboBoxTime.SelectedIndex == 3) { Exporter.Add(Parameters.SubtitleList, int.Parse(TextBoxInput.Text) * 3600000); } //add hours
                    }

                    if (ComboBoxMath.SelectedIndex == 1) //subtract
                    {
                        if (ComboBoxTime.SelectedIndex == 0) { Exporter.Subtract(Parameters.SubtitleList, int.Parse(TextBoxInput.Text)); } //subtract milliseconds
                        if (ComboBoxTime.SelectedIndex == 1) { Exporter.Subtract(Parameters.SubtitleList, int.Parse(TextBoxInput.Text) * 1000); } //subtract seconds
                        if (ComboBoxTime.SelectedIndex == 2) { Exporter.Subtract(Parameters.SubtitleList, int.Parse(TextBoxInput.Text) * 60000); } //subtract minutes
                        if (ComboBoxTime.SelectedIndex == 3) { Exporter.Subtract(Parameters.SubtitleList, int.Parse(TextBoxInput.Text) * 3600000); } //subtract hours
                    }

                    await Exporter.Export(Parameters.FileName);  

                }
                catch (Exception)
                {

                    await Dialogs.ErrorDialog("Exporing aborted", "The value you entered isn't a numeric value or a whole number. Please try again.");
                }
            }

            else
            {
                await Dialogs.ErrorDialog("Exporting aborted", "Please load a subtitle file first.");
            }
        }       
        private void ButtonAbout_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(About));
        }
    }
}