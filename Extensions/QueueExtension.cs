using System.Linq;

namespace System.Collections.Generic
{
    public static class QueueExtension
    {
        public static T GetNextCycledItem<T>(this Queue<T> q) 
        { 
            var item = q.Dequeue();
            q.Enqueue(item);
            return item;
        }

        public static Queue<T> Rotate<T>(this Queue<T> q) 
        { 
            var item = q.Dequeue();
            q.Enqueue(item);
            return q;
        }

        public static Queue<T> Shuffle<T>(this Queue<T> q)
        { 
            return new Queue<T>(Shuffle(q.ToArray()));
        }

        private static IEnumerable<T> Shuffle<T>(IEnumerable<T> list) 
        { 
            var a = list.ToArray();
            var random = new Random((int) (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond));
            var n = a.Length;
            for (int i = 0; i < n; i++) {
                int r = i + random.Next(n-i);
                var temp = a[i];
                a[i] = a[r];
                a[r] = temp;
            }
            return a;
        }
    }
}
