using HallAdPlayer.Public;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace HallAdPlayer
{
    class Program
    {
        static void Main()
        {
            try { ConsoleHelper.ShowInfo(); Run(); } catch (Exception ex) { Console.WriteLine(ex.Message); throw ex; }
        }

        /// <summary>
        /// entry point for start playing music and ad
        /// </summary>
        private static void Run() 
        {
            // subscribe to any player event and execute linked action
            AdService.Actions().Subscribe(a => a.Execute());
            
            // start timer responsible to dispatch ad play event
            TimeForAdDispatcher.Start(Configuration.Playlist.AdTime * 1000 * 60);

            // start play music immediatelly
            AdService.Dispatch(new Play(PlayerProvider.Play));

            // subscribe to commands typed in console and dispatch as event
            using(new CommandsFromConsole()
                .Where(x => x != null)
                .ToObservable()
                .Subscribe(AdService.Dispatch)
            ){};
        }
    }
}