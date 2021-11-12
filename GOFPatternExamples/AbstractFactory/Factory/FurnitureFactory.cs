using GOFPatternExamples.AbstractFactory.Product;

namespace GOFPatternExamples.AbstractFactory.Factory
{
    abstract class FurnitureFactory
    {
        public abstract Chair CreateChair();

        public abstract Sofa CreateSofa();

        public abstract Table CreateTable();
    }

    class VictorianFactory : FurnitureFactory
    {
        public override Chair CreateChair()
        {
            return new VictorianChair();
        }

        public override Sofa CreateSofa()
        {
            return new VictorianSofa();
        }

        public override Table CreateTable()
        {
            return new VictorianTable();
        }
    }

    class ModernFactory : FurnitureFactory
    {
        public override Chair CreateChair()
        {
            return new ModernChair();
        }

        public override Sofa CreateSofa()
        {
            return new ModernSofa();
        }

        public override Table CreateTable()
        {
            return new ModernTable();
        }
    }
}
