namespace IISTestApplication.Models.MapperModels
{
    public class ModelWithoutPostPrefixe
    {
        public string Name { get; set; }

        public int Price { get; set; }
    }

    public class ModelWithPrefix
    {
        public string pref1Name { get; set; }

        public int pref2Price { get; set; }
    }

    public class ModelWithPostfix
    {
        public string Namepostf1 { get; set; }

        public int Pricepostf2 { get; set; }
    }
}
