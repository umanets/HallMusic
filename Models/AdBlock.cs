using System;
using System.Collections.Generic;
using System.Text;

namespace HallAdPlayer.Models
{
    public class AdBlock
    {
        private readonly Queue<MusicFile> _pq;
        private int playIndex;

        public AdBlock(Queue<MusicFile> pq)
        {
            _pq = pq;
            playIndex = 0;
        }

        public bool IsPlayedOneCycle
        { 
            get 
            { 
                return playIndex > 0 && playIndex % _pq.Count == 0;
            }
        }

        public MusicFile GetNext()
        { 
            playIndex = IsPlayedOneCycle ? 1 : playIndex + 1;
            return _pq.GetNextCycledItem();
        }
    }
}
