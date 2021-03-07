using System;
using System.Reactive.Subjects;

namespace HallAdPlayer.Public
{
    public static class AdService
    {
        private static Subject<IAdServiceActions> actionSource = new Subject<IAdServiceActions>();

        public static IObservable<IAdServiceActions> Actions() 
        {
            return actionSource;
        }

        public static void Dispatch(IAdServiceActions action) { 
            actionSource.OnNext(action);
        }
    }
}
