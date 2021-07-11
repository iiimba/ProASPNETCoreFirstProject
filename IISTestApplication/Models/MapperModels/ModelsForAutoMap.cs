using AutoMapper;
using AutoMapper.Configuration.Annotations;

namespace IISTestApplication.Models.MapperModels
{
    public class AutoMapModel
    {
        public int Value { get; set; }

        public int Total { get; set; }
    }

    [AutoMap(typeof(AutoMapModel))]
    public class AutoMapModelDTO
    {
        public int Value { get; set; }

        [Ignore]
        public int Total { get; set; }

        [SourceMember(nameof(Total))]
        public int TotalSecret { get; set; }
    }
}
