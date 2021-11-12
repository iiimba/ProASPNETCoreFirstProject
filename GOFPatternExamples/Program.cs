using GOFPatternExamples.AbstractFactory;
using GOFPatternExamples.AbstractFactory.Factory;
using GOFPatternExamples.Builder;

namespace GOFPatternExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            AbstractFactoryExample();

            BuilderExample();
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
    }
}
