using System.Collections.Generic;
using System.Linq;

namespace IISTestApplication.Models.MapperModels
{
	public class FlatterOrder
	{
		private readonly IList<FlatterOrderLineItem> _orderLineItems = new List<FlatterOrderLineItem>();

		public FlatterCustomer Customer { get; set; }

		public FlatterOrderLineItem[] GetOrderLineItems()
		{
			return _orderLineItems.ToArray();
		}

		public void AddOrderLineItem(FlatterProduct product, int quantity)
		{
			_orderLineItems.Add(new FlatterOrderLineItem(product, quantity));
		}

		public decimal GetTotal()
		{
			return _orderLineItems.Sum(li => li.GetTotal());
		}
	}

	public class FlatterProduct
	{
		public decimal Price { get; set; }

		public string Name { get; set; }
	}

	public class FlatterOrderLineItem
	{
		public FlatterOrderLineItem(FlatterProduct product, int quantity)
		{
			Product = product;
			Quantity = quantity;
		}

		public FlatterProduct Product { get; private set; }

		public int Quantity { get; private set; }

		public decimal GetTotal()
		{
			return Quantity * Product.Price;
		}
	}

	public class FlatterCustomer
	{
		public string Name { get; set; }
	}

	public class FlatterOrderDTO
	{
		public string CustomerName { get; set; }

		public decimal Total { get; set; }
	}
}
