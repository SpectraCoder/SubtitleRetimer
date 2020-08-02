using SubtitlesParser.Classes.Parsers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.DirectX.Direct3D11;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace SubtitleRetimer
{
    class Importer
    {
        private static async Task<StorageFile> LoadFile()
        {            
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.FileTypeFilter.Add(".srt");
            openPicker.FileTypeFilter.Add(".txt");
            
            var file = await openPicker.PickSingleFileAsync();
           
            return file;
        }
        public static async Task LoadTextFile()
        {             
            StorageFile file = await LoadFile();

            if(file != null) 
            {
                if (file.FileType != ".srt" && file.FileType != ".txt")
                {
                    await Dialogs.ErrorDialog("Unable to open: The file wasn't a .srt or .txt file.");                    
                }
                else
                {
                    try
                    {
                        await SubtitleParser(file);
                        Parameters.FileName = Path.GetFileNameWithoutExtension(file.Name);
                        Parameters.ViewModel.Status.StatusMessage = $"{file.Name} is loaded";                        
                    }
                    catch (Exception)
                    {
                        await Dialogs.ErrorDialog("The file you selected isn't in the right format. Please check if the file is UTF-8.");
                    }
                }

            }

        }
        private async static Task SubtitleParser(StorageFile subtitleFile)
        {
            if (subtitleFile != null)
            {
                var subtitleText = await FileIO.ReadTextAsync(subtitleFile);
                byte[] bytearray = Encoding.UTF8.GetBytes(subtitleText);
                MemoryStream stream = new MemoryStream(bytearray);

                SrtParser srtparser = new SrtParser();
                Parameters.SubtitleList = srtparser.ParseStream(stream, Encoding.UTF8);
            }
        }               
    }
}
