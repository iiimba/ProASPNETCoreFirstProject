namespace GOFPatternExamples.Singleton
{
    class Singleton
    {
        protected Singleton()
        {

        }

        protected static Singleton _singleton;

        public static Singleton GetSingleton()
        {
            if (_singleton == null)
            {
                _singleton = new Singleton();
            }

            return _singleton;
        }
    }

    class SingletonForMultithreading
    {
        protected SingletonForMultithreading()
        {

        }

        protected static SingletonForMultithreading _singleton;

        protected static object _lock = new object();

        public static SingletonForMultithreading GetSingleton()
        {
            if (_singleton == null)
            {
                lock (_lock)
                {
                    if (_singleton == null)
                    {
                        _singleton = new SingletonForMultithreading();
                    }
                }
            }

            return _singleton;
        }
    }

    class SingletonForMultithreadingWithoutLock
    {
        static SingletonForMultithreadingWithoutLock()
        {
            _singleton = new SingletonForMultithreadingWithoutLock();
        }

        protected static SingletonForMultithreadingWithoutLock _singleton;

        public static SingletonForMultithreadingWithoutLock GetSingleton()
        {
            return _singleton;
        }
    }
}
