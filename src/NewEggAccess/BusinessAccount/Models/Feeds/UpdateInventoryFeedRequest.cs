﻿using CuttingEdge.Conditions;
using NewEggAccess.Models.Items;
using System.Collections.Generic;

namespace NewEggAccess.BusinessAccount.Models.Feeds
{
	public class UpdateInventoryFeedRequestBody
	{
		public InventoryUpdateFeed Inventory { get; set; }
	}

	public class InventoryUpdateFeed
	{
		public IEnumerable< InventoryUpdateFeedItem > Item { get; private set; }

		public InventoryUpdateFeed( IEnumerable< InventoryUpdateFeedItem > items )
		{
			Condition.Requires( items, "items " ).IsNotEmpty();

			this.Item = items;
		}
	}

	public class InventoryUpdateFeedItem
	{
		public string SellerPartNumber { get; private set; }		
		public int Inventory { get; private set; }

		public InventoryUpdateFeedItem( string sellerPartNumber, int inventory )
		{
			Condition.Requires( sellerPartNumber, "sku/sellerPartNumber" ).IsNotNullOrWhiteSpace().IsNotLongerThan( ItemInventoryRequest.MaxSellerPartNumberLength );			
			Condition.Requires( inventory, "inventory" ).IsGreaterOrEqual( 0 );

			this.SellerPartNumber = sellerPartNumber;			
			this.Inventory = inventory;
		}
	}
}