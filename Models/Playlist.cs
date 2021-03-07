using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HallAdPlayer.Models
{
    public class FileNames 
    { 
        public string FileName {get;set;}
    }

    public class AdBlockConf
    { 
        public List<FileNames> FileNames {get;set;}
    }

    public class Playlist
    {
        public int AdTime { get; set; }
        public string MusicDir { get; set; }
        public List<AdBlockConf> AdBlocks {get;set;}
    }
}
