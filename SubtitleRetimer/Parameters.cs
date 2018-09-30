using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SubtitleRetimer
{
    class Parameters
    {
        public static StorageFile SubtitleFile { get; set; }

        public static List<SubtitlesParser.Classes.SubtitleItem> SubtitleList { get; set; } = new List<SubtitlesParser.Classes.SubtitleItem>();
    }
}
