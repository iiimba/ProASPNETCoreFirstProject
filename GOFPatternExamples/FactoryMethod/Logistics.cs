namespace GOFPatternExamples.FactoryMethod
{
    abstract class Logistics
    {
        public abstract Transport FactoryMethod();

        public void PlanDelivery()
        {
            var transport = FactoryMethod();
            transport.Deliver();
        }
    }

    class RoadLogistics : Logistics
    {
        public override Transport FactoryMethod()
        {
            return new Truck();
        }
    }

    class SeaLogistics : Logistics
    {
        public override Transport FactoryMethod()
        {
            return new Ship();
        }
    }
}
