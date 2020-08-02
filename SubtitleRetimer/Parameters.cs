using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SubtitleRetimer
{
    public static class Parameters 
    {       
        public static string FileName = "";
        public static StatusViewModel ViewModel = new StatusViewModel();
        public static List<SubtitlesParser.Classes.SubtitleItem> SubtitleList { get; set; } = new List<SubtitlesParser.Classes.SubtitleItem>();        
    }
}
