namespace IISTestApplication.Models.MapperModels
{
    public class Source
    {
        public int Value { get; set; }
    }

    public class SourceDTO
    {
        public SourceDTO()
        {

        }

        public SourceDTO(int newName)
        {
            Value = newName;
        }

        public int Value { get; }
    }
}
