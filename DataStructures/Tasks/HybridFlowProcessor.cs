using System;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        private readonly LinkedList<T> _storage;

        public HybridFlowProcessor()
        {
            _storage = new LinkedList<T>();
        }

        public void Push(T item)
        {
            _storage.AddFirst(item);
        }

        public T Pop()
        {
            if (_storage.Count == 0)
            {
                throw new InvalidOperationException("No items to pop.");
            }

            var firstItem = _storage.First.Value;
            _storage.RemoveFirst();
            return firstItem;
        }

        public void Enqueue(T item)
        {
            _storage.AddLast(item);
        }

        public T Dequeue()
        {
            if (_storage.Count == 0)
            {
                throw new InvalidOperationException("No items to dequeue.");
            }

            var firstItem = _storage.First.Value;
            _storage.RemoveFirst();
            return firstItem;
        }
    }

}
