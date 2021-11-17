using System.Collections.Generic;

namespace GOFPatternExamples.Iterator
{
    abstract class Aggregate
    {
        public abstract Iterator CreateIterator();
    }

    class ConcreteAggregate : Aggregate
    {
        private List<object> _list = new List<object>();

        public override Iterator CreateIterator()
        {
            return new ConcreteIterator(this);
        }

        public object this[int index]
        {
            get { return _list[index]; }
        }

        public int Count { get { return _list.Count; } }

        public void Add(object value)
        {
            _list.Add(value);
        }
    }
}
