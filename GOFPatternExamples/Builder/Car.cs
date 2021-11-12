namespace GOFPatternExamples.Builder
{
    class RenoCar
    {
        public string Brand { get; set; }

        public int Seats { get; set; }

        public Engine Engine { get; set; }

        public bool GPSInstalled { get; set; }
    }

    class OpelCar
    {
        public string Brand { get; set; }

        public int Seats { get; set; }

        public Engine Engine { get; set; }

        public bool GPSInstalled { get; set; }
    }

    class Engine
    {

    }

    class SportEngine : Engine
    {

    }

    class SimpleEngine : Engine
    {

    }
}
