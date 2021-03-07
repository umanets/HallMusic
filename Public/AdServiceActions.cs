using System;

namespace HallAdPlayer.Public
{
    public interface IAdServiceActions
    {
        string Type();
        void Execute();
    }

    public class Play : IAdServiceActions
    {
        private readonly Action action;

        public Play(Action action)
        {
            this.action = action;
        }

        public void Execute()
        { 
            action();
        }

        public string Type()
        {
            return "PLAY";
        }
    }

    public class Stop : IAdServiceActions
    {
        private readonly Action action;

        public Stop(Action action)
        {
            this.action = action;
        }

        public void Execute()
        { 
            action();
        }

        public string Type()
        {
            return "STOP";
        }
    }

    public class Pause : IAdServiceActions
    {
        private readonly Action action;

        public Pause(Action action)
        {
            this.action = action;
        }

        public void Execute()
        { 
            action();
        }

        public string Type()
        {
            return "PAUSE";
        }
    }

    public class Resume : IAdServiceActions
    {
        private readonly Action action;

        public Resume(Action action)
        {
            this.action = action;
        }

        public void Execute()
        { 
            action();
        }

        public string Type()
        {
            return "RESUME";
        }
    }

    public class AdPlay : IAdServiceActions
    {
        private readonly Action action;

        public AdPlay(Action action)
        {
            this.action = action;
        }

        public void Execute()
        { 
            action();
        }

        public string Type()
        {
            return "ADPLAY";
        }
    }

    public class AdPlayFinished : IAdServiceActions
    {
        private readonly Action action;

        public AdPlayFinished(Action action)
        {
            this.action = action;
        }

        public void Execute()
        { 
            action();
        }

        public string Type()
        {
            return "ADPLAYFINISHED";
        }
    }
}
