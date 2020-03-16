using CuttingEdge.Conditions;
using System.Collections.Generic;

namespace NewEggAccess.Models.Feeds
{
	public class UpdateInventoryFeedRequestBody
	{
		public IEnumerable< InventoryUpdateFeedItem > Inventory { get; set; }
	}

	public class InventoryUpdateFeedItem
	{
		public string SellerPartNumber { get; private set; }
		public string WarehouseLocation { get; private set; }
		public int Inventory { get; private set; }

		public InventoryUpdateFeedItem( string sellerPartNumber, string warehouseLocation, int inventory )
		{
			Condition.Requires( sellerPartNumber, "sellerPartNumber" ).IsNotNullOrWhiteSpace();
			Condition.Requires( warehouseLocation, "warehouseLocation" ).IsNotNullOrWhiteSpace().HasLength( 3 );
			Condition.Requires( inventory, "inventory" ).IsGreaterOrEqual( 0 );

			this.SellerPartNumber = sellerPartNumber;
			this.WarehouseLocation = warehouseLocation;
			this.Inventory = inventory;
		}
	}
}