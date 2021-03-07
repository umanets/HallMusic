using HallAdPlayer.Public;
using System;

namespace HallAdPlayer
{
    public static class TimeForAdDispatcher
    {
        private static System.Timers.Timer _adTimer;

        public static void Start(int time) 
        { 
            _adTimer = new System.Timers.Timer(time);
            _adTimer.Elapsed += (a, b) => AdService.Dispatch(new AdPlay(PlayerProvider.PlayAd));
            _adTimer.Enabled = true;

            AdService.Actions().Subscribe(ProcessAction);
        }

        private static void ProcessAction(IAdServiceActions action)
        { 
            switch(action.Type()) 
            { 
                case "ADPLAYFINISHED":
                    _adTimer.Enabled = true;
                    break;
                case "ADPLAY":
                    _adTimer.Enabled = false;
                    break;
            }
        }
    }

}
