using GOFPatternExamples.AbstractFactory;
using GOFPatternExamples.AbstractFactory.Factory;
using GOFPatternExamples.Adapter;
using GOFPatternExamples.Builder;
using GOFPatternExamples.FactoryMethod;
using GOFPatternExamples.Prototype;
using GOFPatternExamples.Singleton;
using System;

namespace GOFPatternExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            AbstractFactoryExample();

            BuilderExample();

            FactoryMethodExample();

            PrototypeExample();

            SingletonExample();

            AdaptorExample();

            Console.ReadKey();
        }

        /// <summary>
        /// You can also use the product family instead of using the Run method
        /// </summary>
        static void AbstractFactoryExample()
        {
            var client = new AbstractFactoryClient(new VictorianFactory());
            client.Run();

            client = new AbstractFactoryClient(new ModernFactory());
            client.Run();
        }

        static void BuilderExample()
        {
            var director = new Director();

            var renoBuilder = new RenoBuilder();
            director.MakeSportCar(renoBuilder);
            var renoCar = renoBuilder.GetResult();

            var opelBuilder = new OpelBuilder();
            director.MakeSportCar(opelBuilder);
            var opelCar = opelBuilder.GetResult();
        }

        static void FactoryMethodExample()
        {
            var roadLogistics = new RoadLogistics();
            roadLogistics.PlanDelivery();
            var truck = roadLogistics.FactoryMethod();

            var seaLogistics = new SeaLogistics();
            seaLogistics.PlanDelivery();
            var ship = seaLogistics.FactoryMethod();
        }

        static void PrototypeExample()
        {
            var developer = new Developer("Vlad", 1000, "C#");
            var developerClone = developer.Clone() as Developer;

            Console.WriteLine(developerClone);
        }

        static void SingletonExample()
        {
            var singleton1 = Singleton.Singleton.GetSingleton();
            var singleton2 = Singleton.Singleton.GetSingleton();

            Console.WriteLine(singleton1.GetHashCode());
            Console.WriteLine(singleton2.GetHashCode());

            var singletonForMultithreading1 = SingletonForMultithreading.GetSingleton();
            var singletonForMultithreading2 = SingletonForMultithreading.GetSingleton();

            Console.WriteLine(singletonForMultithreading1.GetHashCode());
            Console.WriteLine(singletonForMultithreading2.GetHashCode());

            var singletonForMultithreadingWithoutLock1 = SingletonForMultithreadingWithoutLock.GetSingleton();
            var singletonForMultithreadingWithoutLock2 = SingletonForMultithreadingWithoutLock.GetSingleton();

            Console.WriteLine(singletonForMultithreadingWithoutLock1.GetHashCode());
            Console.WriteLine(singletonForMultithreadingWithoutLock2.GetHashCode());
        }

        static void AdaptorExample()
        {
            ITarget target1 = new ClassAdapter();
            target1.Request();

            Target target2 = new ObjectAdapter();
            target2.Request();
        }
    }
}
