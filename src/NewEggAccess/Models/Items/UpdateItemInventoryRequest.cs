namespace NewEggAccess.Models.Items
{
	public class UpdateItemInventoryRequest
	{
		public int Type { get; set; }
		public string Value { get; set; }
		public InventoryList InventoryList { get; set; }
	}

	public class InventoryList
	{
		public UpdateItemInventory[] Inventory { get; set; }
	}

	public class UpdateItemInventory
	{
		public string WarehouseLocation { get; set; }
		public int AvailableQuantity { get; set; }
	}

	public class UpdateItemInventoryResponse
	{
		public string SellerId { get; set; }
		public string ItemNumber { get; set; }
		public string SellerPartNumber { get; set; }
		public UpdateItemInventory[] InventoryList { get; set; }
	}
}