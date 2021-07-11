namespace IISTestApplication.Models.MapperModels
{
    public class IncludeMemberSource
    {
        public string Name { get; set; }

        public IncludeMemberInnerSource InnerSource { get; set; }

        public IncludeMemberOtherInnerSource OtherInnerSource { get; set; }
    }

    public class IncludeMemberInnerSource
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class IncludeMemberOtherInnerSource
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }
    }

    public class IncludeMemberDestination
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }
    }
}
