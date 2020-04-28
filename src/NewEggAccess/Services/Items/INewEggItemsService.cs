using NewEggAccess.Models.Items;
using NewEggAccess.Shared;
using NewEggAccess.Throttling;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewEggAccess.Services.Items
{
	public interface INewEggItemsService
	{
		Throttler Throttler { get; }
		Task< ItemInventory > GetSkuInventory( string sku, string warehouseLocationCode, CancellationToken token, Mark mark = null );
		Task< UpdateItemInventoryResponse > UpdateSkuQuantityAsync( string sku, string warehouseLocationCountryCode, int quantity, CancellationToken token, Mark mark = null );
		Task UpdateSkusQuantitiesAsync( Dictionary< string, int > skusQuantities, string warehouseLocationCode, CancellationToken token, Mark mark = null );
	}
}