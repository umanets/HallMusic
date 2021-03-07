using System;
using System.Collections.Generic;
using System.Text;

namespace HallAdPlayer.Models
{
    public class MusicFile
    {
        public MusicFile(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; } = "";
    }
}
