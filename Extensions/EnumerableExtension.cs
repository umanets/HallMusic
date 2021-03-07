using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace System.Collections.Generic
{
    public static class EnumerableExtension
    {
        public static IObservable<T> ToObservable<T>(this IEnumerable<T> list) 
        { 
            return Observable.Create<T>(obs =>
            {
                foreach(var item in list)
                {
                    obs.OnNext(item);
                }
                return Disposable.Create(()=>{});
            });
        }
    }
}
