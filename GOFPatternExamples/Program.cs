using GOFPatternExamples.AbstractFactory;
using GOFPatternExamples.AbstractFactory.Factory;
using GOFPatternExamples.Builder;
using GOFPatternExamples.FactoryMethod;
using GOFPatternExamples.Prototype;
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
    }
}
