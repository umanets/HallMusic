namespace HallAdPlayer
{
    public static class PlayerProvider
    {
        private static readonly PlayersChanger playersChanger = new PlayersChanger();
        
        public static void Play()
        { 
            playersChanger.Play(); 
        }

        public static void Pause()
        { 
            playersChanger.Pause();
        }

        public static void PlayAd()
        { 
            playersChanger.FadeIn();
            playersChanger.Pause();
            
            playersChanger.SetAdActive();

            playersChanger.ResetVolume();
            playersChanger.Play();
        }

        public static void Resume()
        { 
            playersChanger.Resume();
        }

        public static void Stop()
        { 
            playersChanger.Stop();
        }

        public static void Dispose()
        {
        }
    }
}
