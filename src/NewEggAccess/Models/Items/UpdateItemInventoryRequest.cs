using CuttingEdge.Conditions;

namespace NewEggAccess.Models.Items
{
	public class UpdateItemInventoryRequest
	{
		public int Type { get; private set; }
		public string Value { get; private set; }
		public InventoryList InventoryList { get; private set; }

		public UpdateItemInventoryRequest( int type, string value, string warehouseLocation, int quantity )
		{
			Condition.Requires( value, "sku/sellerPartNumber" ).IsNotNullOrWhiteSpace().IsNotLongerThan( ItemInventoryRequest.MaxSellerPartNumberLength );
			Condition.Requires( warehouseLocation, "warehouseLocation" ).IsNotNullOrWhiteSpace();

			this.Value = value;
			this.Type = type;
			this.InventoryList = new InventoryList()
			{
				Inventory = new UpdateItemInventory [] { new UpdateItemInventory() { WarehouseLocation = warehouseLocation, AvailableQuantity = quantity } }
			};
		}
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