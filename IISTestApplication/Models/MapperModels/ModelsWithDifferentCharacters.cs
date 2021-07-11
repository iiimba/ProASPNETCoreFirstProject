namespace IISTestApplication.Models.MapperModels
{
    public class ModelWithWrongCharacters
    {
        public int Value { get; set; }

        public int Ävíator { get; set; }

        public int SubAirlinaFlight { get; set; }
    }

    public class ModelWithEn
    {
        public int Value { get; set; }

        public int Aviator { get; set; }

        public int SubAirlineFlight { get; set; }
    }
}
