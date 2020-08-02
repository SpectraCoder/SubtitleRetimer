using System;
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
using Windows.UI.Xaml.Shapes;

namespace SubtitleRetimer
{
    class Exporter
    {
        public static async Task Export(string fileName)
        {
            uint index = 0;
            List<string> lines = new List<string>();

            foreach (var item in Parameters.SubtitleList)
            {
                index++;
                TimeSpan startTime = TimeSpan.FromMilliseconds(item.StartTime);
                TimeSpan endTime = TimeSpan.FromMilliseconds(item.EndTime);
                
                string lineOne = index.ToString();
                string lineTwo = string.Format("{0:D2}:{1:D2}:{2:D2},{3:D3}", startTime.Hours, startTime.Minutes, startTime.Seconds, startTime.Milliseconds) + " --> " + string.Format("{0:D2}:{1:D2}:{2:D2},{3:D3}", endTime.Hours, endTime.Minutes, endTime.Seconds, endTime.Milliseconds);
                lines.Add(lineOne);
                lines.Add(lineTwo);

                foreach (var line in item.Lines)
                {
                    lines.Add(line);
                }
                    
                string lastLine = string.Empty;
                lines.Add(lastLine);               
            }

            await SaveTextFile(lines, fileName);
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

        private static async Task SaveTextFile(List<string> subtitleLines, string fileName)
        {
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
            savePicker.SuggestedFileName = fileName + " retimed";
            savePicker.FileTypeChoices.Add("SRT file", new List<string>() { ".srt" });
            savePicker.FileTypeChoices.Add("Text file", new List<string>() { ".txt" });

            StorageFile savefile = await savePicker.PickSaveFileAsync();

            if (savefile != null)
            {                
                await FileIO.WriteLinesAsync(savefile, subtitleLines);
                
                Windows.Storage.Provider.FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(savefile);
                
                if (status != Windows.Storage.Provider.FileUpdateStatus.Complete)
                {
                    await Dialogs.ErrorDialog($"File {savefile.Name} couldn't be saved.");                                   
                }
               
                Parameters.ViewModel.Status.StatusMessage = $"{fileName} was exported successfully";
                
            }   

        }
    }

    
}
