using SubtitlesParser.Classes.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace SubtitleRetimer
{
    class Importer
    {
        public static async Task LoadTextFile()
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
                return;
            }

            if (Parameters.SubtitleFile.FileType != ".srt" && Parameters.SubtitleFile.FileType != ".txt")
            {
                await Dialogs.ErrorDialog("Unable to open: The file wasn't a .srt or .txt file.");
                return;
            }

            else
            {
                try
                {                    
                    await SubtitleParser(Parameters.SubtitleFile);
                }
                catch (Exception)
                {

                    await Dialogs.ErrorDialog("The file you selected isn't in the right format. Please check if the file is UTF-8.");
                }

            }

        }

        private async static Task SubtitleParser(StorageFile subtitleFile)
        {
            if (Parameters.SubtitleFile != null)
            {
                try
                {
                    var subtitleText = await FileIO.ReadTextAsync(subtitleFile);
                    byte[] bytearray = Encoding.UTF8.GetBytes(subtitleText);
                    MemoryStream stream = new MemoryStream(bytearray);

                    SrtParser srtparser = new SrtParser();
                    Parameters.SubtitleList = srtparser.ParseStream(stream, Encoding.UTF8);
                    //Parameters.SubtitleListChanged = Parameters.SubtitleList = srtparser.ParseStream(stream, Encoding.UTF8);

                }
                catch (Exception)
                {
                    await Dialogs.ErrorDialog("Something went wrong.");
                }
                
            }

            else
            {
                return;
            }

        }
    }
}
