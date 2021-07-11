namespace IISTestApplication.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class OrderLine
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public decimal Quantity { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }
    }

    public class OrderLineDTO
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string Product { get; set; }

        public decimal Quantity { get; set; }
    }

    public class OrderLineResultDTO
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public decimal Quantity { get; set; }

        public decimal QuantityMultiplly2 { get; set; }

        public OrderLineResultDTO()
        {

        }

        public OrderLineResultDTO(decimal qty)
        {
            QuantityMultiplly2 = qty;
        }
    }

    public class OrderLineWithStringQuantityDTO
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string Quantity { get; set; }
    }

    public class OrderLineRichDTO
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public ProductRich Product { get; set; }

        public int OrderId { get; set; }

        public OrderRich Order { get; set; }

        public string Quantity { get; set; }
    }

    public class OrderRich
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class ProductRich
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }
    }
}
