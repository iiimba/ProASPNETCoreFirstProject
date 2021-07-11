namespace IISTestApplication.Models.MapperModels
{
	public class ParentSource
	{
		public int Value1 { get; set; }
	}

	public class ChildSource : ParentSource
	{
		public int Value2 { get; set; }
	}

	public class ParentDestination
	{
		public int Value1 { get; set; }
	}

	public class ChildDestination : ParentDestination
	{
		public int Value2 { get; set; }
	}

	public class HierarchyOrder { }

	public class HierarchyOnlineOrder : HierarchyOrder
	{
		public string Referrer { get; set; }
	}

	public class HierarchyMailOrder : HierarchyOrder { }

	public class HierarchyOrderDTO
	{
		public string Referrer { get; set; }
	}

	public class HierarchyOnlineOrderDTO : HierarchyOrderDTO { }

	public class HierarchyMailOrderDto : HierarchyOrderDTO { }
}
