﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace SubtitleRetimer
{
    class Exporter
    {        
        public static async Task<bool> Export()
        {
            int index = 0;
            List<string> lines = new List<string>();

            foreach (var item in Parameters.SubtitleList)
            {
                index++;
                TimeSpan startTime = TimeSpan.FromMilliseconds(item.StartTime);
                TimeSpan endTime = TimeSpan.FromMilliseconds(item.EndTime);


                if (item.Lines.Count > 1) //if there are two lines
                {
                    string lineOne = index.ToString();
                    string lineTwo = string.Format("{0:D2}:{1:D2}:{2:D2},{3:D3}", startTime.Hours, startTime.Minutes, startTime.Seconds, startTime.Milliseconds) + " --> " + string.Format("{0:D2}:{1:D2}:{2:D2},{3:D3}", endTime.Hours, endTime.Minutes, endTime.Seconds, endTime.Milliseconds);
                    string lineThree = item.Lines[0];
                    string lineFour = item.Lines[1];
                    string lineFive = string.Empty;

                    lines.Add(lineOne);
                    lines.Add(lineTwo);
                    lines.Add(lineThree);
                    lines.Add(lineFour);
                    lines.Add(lineFive);
                }

                if (item.Lines.Count == 1) //if there is only one line
                {
                    string lineOne = index.ToString();
                    string lineTwo = string.Format("{0:D2}:{1:D2}:{2:D2},{3:D3}", startTime.Hours, startTime.Minutes, startTime.Seconds, startTime.Milliseconds) + " --> " + string.Format("{0:D2}:{1:D2}:{2:D2},{3:D3}", endTime.Hours, endTime.Minutes, endTime.Seconds, endTime.Milliseconds);
                    string lineThree = item.Lines[0];
                    string lineFive = string.Empty;

                    lines.Add(lineOne);
                    lines.Add(lineTwo);
                    lines.Add(lineThree);
                    lines.Add(lineFive);
                }



            }

            bool succeeded = await SaveTextFile(lines, Parameters.SubtitleFile);

            if (succeeded)
            {                
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void Add(List<SubtitlesParser.Classes.SubtitleItem> subtitleList, int milliseconds)
        {
            foreach (var item in subtitleList)
            {
                item.StartTime += milliseconds;
                item.EndTime += milliseconds;
            }
        }

        public static void Subtract(List<SubtitlesParser.Classes.SubtitleItem> subtitleList, int milliseconds)
        {
            foreach (var item in subtitleList)
            {
                item.StartTime -= milliseconds;
                item.EndTime -= milliseconds;
            }
        }

        private static async Task<bool> SaveTextFile(List<string> subtitleLines, StorageFile storagefile)
        {
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
            savePicker.SuggestedFileName = (Path.GetFileNameWithoutExtension(storagefile.Name) + " retimed");
            savePicker.FileTypeChoices.Add("SRT file", new List<string>() { ".srt" });
            savePicker.FileTypeChoices.Add("Text file", new List<string>() { ".txt" });

            StorageFile savefile = await savePicker.PickSaveFileAsync();

            if (savefile != null)
            {
                
                await FileIO.WriteLinesAsync(savefile, subtitleLines);
                
                Windows.Storage.Provider.FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(savefile);
                
                if (status != Windows.Storage.Provider.FileUpdateStatus.Complete)
                {
                    Windows.UI.Popups.MessageDialog errorBox = new Windows.UI.Popups.MessageDialog("File" + savefile.Name + " couldn't be saved.");
                    await errorBox.ShowAsync();
                    return false;
                }

                return true;
            }

            else
            {
                Windows.UI.Popups.MessageDialog errorBox = new Windows.UI.Popups.MessageDialog("File saving was canceled.");
                await errorBox.ShowAsync();
                return false;
            }


        }
    }

    
}
