namespace GOFPatternExamples.Builder
{
    abstract class CarBuilder
    {
        public abstract void SetBrand();

        public abstract void SetSeats(int number);

        public abstract void SetEngine(Engine engine);

        public abstract void SetGPS(bool gpsInstalled);

        /// <summary>
        /// This method can be added only when all Products have the same base class
        /// </summary>
        /// <returns></returns>
        // public abstract Product GetResult();
    }

    class RenoBuilder : CarBuilder
    {
        private RenoCar car = new RenoCar();

        public override void SetBrand()
        {
            car.Brand = "Reno";
        }

        public override void SetEngine(Engine engine)
        {
            car.Engine = engine;
        }

        public override void SetGPS(bool gpsInstalled)
        {
            car.GPSInstalled = gpsInstalled;
        }

        public override void SetSeats(int number)
        {
            car.Seats = number;
        }

        public RenoCar GetResult()
        {
            return car;
        }
    }

    class OpelBuilder : CarBuilder
    {
        private OpelCar car = new OpelCar();

        public override void SetBrand()
        {
            car.Brand = "Opel";
        }

        public override void SetEngine(Engine engine)
        {
            car.Engine = engine;
        }

        public override void SetGPS(bool gpsInstalled)
        {
            car.GPSInstalled = gpsInstalled;
        }

        public override void SetSeats(int number)
        {
            car.Seats = number;
        }

        public OpelCar GetResult()
        {
            return car;
        }
    }
}
