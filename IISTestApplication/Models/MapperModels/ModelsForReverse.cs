namespace IISTestApplication.Models.MapperModels
{
    public class ReverseOrder
    {
        public decimal Total { get; set; }

        public ReverseCustomer Customer { get; set; }
    }

    public class ReverseCustomer
    {
        public string Name { get; set; }
    }

    public class ReverseOrderDTO
    {
        public decimal Total { get; set; }

        public string CustomerName { get; set; }
    }
}
