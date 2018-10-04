using SubtitlesParser.Classes.Parsers;
using System;
using System.Collections.Generic;
using System.Drawing;
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
    class Importer
    {
        public static async Task<bool> LoadTextFile()
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.FileTypeFilter.Add(".srt");
            openPicker.FileTypeFilter.Add(".txt");
            Parameters.SubtitleFile = await openPicker.PickSingleFileAsync();

            if (Parameters.SubtitleFile == null)
            {
                await Dialogs.ErrorDialog("No file selected");
                return false;
            }

            if (Parameters.SubtitleFile.FileType != ".srt" && Parameters.SubtitleFile.FileType != ".txt")
            {
                await Dialogs.ErrorDialog("Unable to open: The file wasn't a .srt or .txt file.");
                return false;
            }

            else
            {
                try
                {
                    bool succeeded = await SubtitleParser(Parameters.SubtitleFile);
                    if (succeeded)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }                                        
                }
                catch (Exception)
                {

                    await Dialogs.ErrorDialog("The file you selected isn't in the right format. Please check if the file is UTF-8.");
                    return false;
                }

            }

        }

        private async static Task<bool> SubtitleParser(StorageFile subtitleFile)
        {
            if (Parameters.SubtitleFile != null)
            {
                var subtitleText = await FileIO.ReadTextAsync(subtitleFile);
                byte[] bytearray = Encoding.UTF8.GetBytes(subtitleText);
                MemoryStream stream = new MemoryStream(bytearray);

                SrtParser srtparser = new SrtParser();
                Parameters.SubtitleList = srtparser.ParseStream(stream, Encoding.UTF8);
                return true;
            }

            else
            {
                return false;
            }

        }
    }
}
