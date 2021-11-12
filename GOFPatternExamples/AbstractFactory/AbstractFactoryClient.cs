using GOFPatternExamples.AbstractFactory.Factory;
using GOFPatternExamples.AbstractFactory.Product;

namespace GOFPatternExamples.AbstractFactory
{
    class AbstractFactoryClient
    {
        private Chair _chair;

        private Sofa _sofa;

        private Table _table;

        public AbstractFactoryClient(FurnitureFactory factory)
        {
            _chair = factory.CreateChair();
            _sofa = factory.CreateSofa();
            _table = factory.CreateTable();
        }

        /// <summary>
        /// This method can be omitted in our example
        /// </summary>
        /// <param name="sofa"></param>
        public void Run()
        {
            _chair.Interact(_sofa);
        }
    }
}
