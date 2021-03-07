using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HallAdPlayer.Models
{
    internal interface IMusicQueue
    { 
        MusicFile GetNext();
    }

    public class AdBlockQueue: IMusicQueue
    {
        private readonly Queue<AdBlock> blocks;

        public AdBlockQueue(Playlist config)
        {
            config.AdBlocks
                .ForEach(x => x.FileNames.ForEach(y => this.Validate(y.FileName)));

            this.blocks = new Queue<AdBlock>(
                config.AdBlocks
                    .Select(x => x.FileNames.Select(y => new MusicFile(y.FileName)))
                    .Select(x => new Queue<MusicFile>(x))
                    .Select(x => new AdBlock(x))
            );
        }

        public MusicFile GetNext() 
        { 
            var currentBlock = this.blocks.Peek();
            
            return currentBlock.IsPlayedOneCycle 
                ? this.blocks.Rotate().Peek().GetNext() 
                : currentBlock.GetNext();
        }

        public bool IsPlayedOneCycle()
        { 
            var currentBlock = this.blocks.Peek();
            
            return currentBlock.IsPlayedOneCycle;
        }

        private void Validate(string path) {
            if (!File.Exists(path)) throw new InvalidOperationException($"File not exists {path}");
        }
    }

    public class MusicQueue: IMusicQueue
    {
        private readonly Queue<MusicFile> musics;
        
        public MusicQueue(Playlist config)
        {
            musics = new Queue<MusicFile>();
            string [] fileEntries = Directory.GetFiles(config.MusicDir);
            foreach(string fileName in fileEntries)
            {
                if (fileName.EndsWith(".mp3"))
                    musics.Enqueue(new MusicFile(fileName));
            }
            musics = musics.Shuffle();
            
            if (musics.Count == 0) throw new InvalidOperationException("The music folder can't be empty");
        }

        public MusicFile GetNext() 
        { 
            return this.musics.GetNextCycledItem();
        }
    }


    public class TracksProvider: IMusicQueue
    {
        private readonly MusicQueue mq;
        private readonly AdBlockQueue aq;
        private IMusicQueue current;
        
        public TracksProvider(MusicQueue mq, AdBlockQueue aq)
        {
            this.aq = aq;
            this.mq = mq;
            this.current = mq;
        }

        public MusicFile GetNext()
        { 
            return this.current.GetNext();
        }

        public void SetMusicDeck()
        { 
            this.current = mq;
        }

        public void SetAdDeck()
        { 
            this.current = aq;
        }

        public bool AdBlockFinished() 
        { 
            return this.current == aq && aq.IsPlayedOneCycle();
        }
    }
}
