﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace SubtitleRetimer{
   
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();            
        }

        private async void ButtonLoadSrt_Click(object sender, RoutedEventArgs e)
        {
            await Importer.LoadTextFile();            
        }

        private async void ButtonExport_Click(object sender, RoutedEventArgs e)
        {
            if(Parameters.SubtitleList != null)
            {
                //List<SubtitlesParser.Classes.SubtitleItem> SubtitleListChanged = new List<SubtitlesParser.Classes.SubtitleItem>();
                //List<SubtitlesParser.Classes.SubtitleItem> SubtitleListChanged = Parameters.SubtitleList.Select(item => new List<SubtitlesParser.Classes.SubtitleItem>(item.Lines[0])).ToList();

                //SubtitleListChanged = Parameters.Clone(Parameters.SubtitleList);
                //Parameters.SubtitleList.ConvertAll(new Converter<SubtitlesParser.Classes.SubtitleItem, SubtitlesParser.Classes.SubtitleItem>(ConvertList) );                

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

            }

            await Exporter.Export();
        }
       
    }
}