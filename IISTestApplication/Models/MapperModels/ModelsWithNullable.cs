namespace IISTestApplication.Models.MapperModels
{
    public class ModelWithNullable
    {
        public int? Number { get; set; }

        public int AdditionalField1 { get; set; }
    }

    public class ModelWithoutNullable
    {
        public int Number { get; set; }

        public int AdditionalField2 { get; set; }
    }
}
