namespace GOFPatternExamples.Builder
{
    /// <summary>
    /// Director also can have his own reference to Builder instance
    /// </summary>
    class Director
    {
        public void MakeSportCar(CarBuilder builder)
        {
            builder.SetBrand();
            builder.SetEngine(new SportEngine());
            builder.SetSeats(4);
            builder.SetGPS(true);
        }

        public void MakeSimpleCar(CarBuilder builder)
        {
            builder.SetBrand();
            builder.SetEngine(new SimpleEngine());
            builder.SetSeats(4);
        }
    }
}
