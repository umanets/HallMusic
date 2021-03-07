using HallAdPlayer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HallAdPlayer
{
    public static class Configuration
    {
        private static Playlist playListConfiguration;
        public static Playlist Playlist 
        { 
            get 
            { 
                if (playListConfiguration == null)
                { 
                    var file = File.ReadAllText(@"appsettings.json");
                    playListConfiguration = JsonConvert.DeserializeObject<Playlist>(file);
                }
                
                return playListConfiguration;
            }  
        }
    }
}
