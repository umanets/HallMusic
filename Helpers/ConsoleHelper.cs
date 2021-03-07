using HallAdPlayer.Public;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HallAdPlayer
{
    /// <summary>
    /// This class contain helper method for user notification using console output
    /// </summary>
    public static class ConsoleHelper
    {
        /// <summary>
        /// This method shows information about command user can type manully to get immediate feedback from app
        /// </summary>
        public static void ShowInfo() 
        { 
            Console.WriteLine("// ============================================================ //");
            Console.WriteLine("// ================== HALL MUSIC PLAYER ======================= //");
            Console.WriteLine("// ============================================================ //");
            Console.WriteLine("");

            Console.WriteLine("Avaiable commands:");
            Console.WriteLine("    stop   - stops current composition and start new one.");
            Console.WriteLine("    pause  - pause current composation.");
            Console.WriteLine("    resume - resume current composation.");
            Console.WriteLine("    adplay - start next block od adv.");
            Console.WriteLine("    exit   - stops player and exit:");

            Console.WriteLine("");
            Console.WriteLine("Type command and press enter");
        }
    }

    public class CommandsFromConsole : IEnumerable<IAdServiceActions>
    {
        private static IAdServiceActions GetCommandByString(string command) 
        { 
            switch(command)
            {
                case "play": return new Play(PlayerProvider.Play);
                case "pause": return new Resume(PlayerProvider.Pause);
                case "resume": return new Resume(PlayerProvider.Resume);
                case "adplay": return new AdPlay(PlayerProvider.PlayAd);
                case "stop": return new Stop(PlayerProvider.Stop);

                default: return null;
            }
        }

        public IEnumerator<IAdServiceActions> GetEnumerator()
        {
            for (string command; (command = Console.ReadLine()) != "exit";)
            { 
                yield return GetCommandByString(command);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
