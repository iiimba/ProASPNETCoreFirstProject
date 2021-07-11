namespace IISTestApplication.Models.MapperModels
{
    public class Car
    {
        public int Year { get; set; }

        public Engine Engine { get; set; }
    }

    public class Engine
    {
        public string Model { get; set; }
    }

    public class CarDTO
    {
        public int Year { get; set; }

        public EngineDTO Engine { get; set; }
    }

    public class EngineDTO
    {
        public string Model { get; set; }
    }
}
