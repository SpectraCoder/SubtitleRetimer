using SubtitlesParser.Classes;
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
        public static List<SubtitleItem> SubtitleList = new List<SubtitleItem>();        
    }
}
