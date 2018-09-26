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

        //public static List<SubtitlesParser.Classes.SubtitleItem> DeepCopy()
        //{
        //    List<SubtitlesParser.Classes.SubtitleItem> deepCopy = new List<SubtitlesParser.Classes.SubtitleItem>();
        //    return deepCopy;
        //}
        //public static List<SubtitlesParser.Classes.SubtitleItem> SubtitleListChanged { get; set; }  = new List<SubtitlesParser.Classes.SubtitleItem>();

        //public static IList<T> CloneList<T>(IList<T> listToClone) where T : ICloneable
        //{
        //    return listToClone.Select(item => (T)item.Clone()).ToList();
        //}

        
        public static T Clone<T>(T obj)
        {
            using(var stream = new System.IO.MemoryStream())
            {
                var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formatter.Serialize(stream, obj);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }            
        }

        //public enum TimeOptions {Milliseconds, Seconds, Minutes, Hours};

        //public enum MathOptions {Add, Subtract};
    }

}
