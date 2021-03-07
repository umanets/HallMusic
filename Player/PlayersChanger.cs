using HallAdPlayer.Models;
using HallAdPlayer.Public;
using Newtonsoft.Json;
using System.IO;

namespace HallAdPlayer
{
    public class PlayersChanger
    {
        private readonly Player _adPlayer = new Player(1f);
        private readonly Player _musicPlayer = new Player(0.8f);
        private Player _currentPlayer;
        private readonly TracksProvider tracks;
        
        public PlayersChanger()
        {
            var appConfig = JsonConvert.DeserializeObject<Playlist>(File.ReadAllText(@"appsettings.json"));
            tracks = new TracksProvider(new MusicQueue(appConfig), new AdBlockQueue(appConfig));
            _currentPlayer = _musicPlayer;

            _musicPlayer.Stopped += MusicPlayer_Stopped;
            _adPlayer.Stopped += AdPlayer_Stopped;
        }

        private void AdPlayer_Stopped(object sender, System.EventArgs e)
        {
            if (tracks.AdBlockFinished())
            {
                this.SetMusicActive();
                _currentPlayer.Resume();
                AdService.Dispatch(new AdPlayFinished(() => { })); // add event regular
                _currentPlayer.FadeOut();
            } 
            else 
            { 
                if (_currentPlayer == _adPlayer)
                    _currentPlayer.Play(tracks.GetNext());
            }
        }

        private void MusicPlayer_Stopped(object sender, System.EventArgs e)
        {
            if (_currentPlayer == _musicPlayer)
                _currentPlayer.Play(tracks.GetNext());
        }

        public void Play()
        { 
            _currentPlayer.Play(tracks.GetNext());
        }

        public void FadeOut()
        { 
            _currentPlayer.FadeOut();
        }

        public void FadeIn()
        { 
            _currentPlayer.FadeIn();
        }

        public void Pause()
        { 
            _currentPlayer.Pause();
        }

        public void Resume()
        { 
            _currentPlayer.Resume();
        }

        public void Stop()
        { 
            _currentPlayer.Stop();
        }

        public void ResetVolume()
        { 
            _currentPlayer.ResetVolume();
        }

        public void SetMusicActive() 
        { 
            _currentPlayer = _musicPlayer; tracks.SetMusicDeck(); 
        }

        public void SetAdActive() { _currentPlayer = _adPlayer; tracks.SetAdDeck(); }
    }
}
