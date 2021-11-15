using System;

namespace GOFPatternExamples.Proxy
{
    interface ISubject
    {
        void Request();
    }

    class Proxy : ISubject
    {
        private RealSubject _realSubject;

        public void Request()
        {
            Console.WriteLine($"{nameof(Proxy)} start work");

            if (_realSubject == null)
            {
                _realSubject = new RealSubject();
            }

            _realSubject.Request();

            Console.WriteLine($"{nameof(Proxy)} end work");
        }
    }

    class RealSubject : ISubject
    {
        public void Request()
        {
            Console.WriteLine(nameof(RealSubject));
        }
    }
}
