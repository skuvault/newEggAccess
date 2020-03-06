using System;
using System.Collections.Generic;
using System.Text;

namespace NewEggAccess.Models.Items
{
	public class GetItemInventoryRequest
	{
		public int Type { get; set; }
		public string Value { get; set; }
		public WarehouseList WarehouseList { get; set; }
	}

	public class WarehouseList
	{
		public string[] WarehouseLocation { get; set; }
	}
}