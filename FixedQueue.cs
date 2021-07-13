using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector
{
    //First In First Out generic collection.
    class FixedQueue<T>
    {
        public ConcurrentQueue<T> q = new ConcurrentQueue<T>();
        private object lockObject = new object();

        //Determines size of collection.
        public int Limit { get; set; }

        //Adds data to end of collection.
        public void Enqueue(T obj)
        {
            q.Enqueue(obj);
            lock (lockObject)
            {
                //When full will lose first element and continue to add to end of queue.
                T overflow;
                while (q.Count > Limit && q.TryDequeue(out overflow)) ;
            }
        }
    }
}
