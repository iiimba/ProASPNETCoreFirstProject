namespace GOFPatternExamples.Iterator
{
    abstract class Iterator
    {
        public abstract object First();

        public abstract object Next();

        public abstract bool IsDone();

        public abstract object Current();
    }

    class ConcreteIterator : Iterator
    {
        private ConcreteAggregate _aggregate;
        private int _current;

        public ConcreteIterator(ConcreteAggregate aggregate)
        {
            _aggregate = aggregate;
        }

        public override object Current()
        {
            return _aggregate[_current];
        }

        public override object First()
        {
            return _aggregate[0];
        }

        public override bool IsDone()
        {
            return _current == _aggregate.Count;
        }

        public override object Next()
        {
            object next = null;
            if (++_current < _aggregate.Count)
            {
                next = _aggregate[_current];
            }

            return next;
        }
    }
}
