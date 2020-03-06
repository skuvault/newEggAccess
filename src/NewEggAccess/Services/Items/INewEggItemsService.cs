using NewEggAccess.Models.Items;
using System.Threading;
using System.Threading.Tasks;

namespace NewEggAccess.Services.Items
{
	public interface INewEggItemsService
	{
		Task< ItemInventory > GetSkuInventory( string sku, string warehouseLocationCode, CancellationToken token );
		Task< UpdateItemInventoryResponse > UpdateSkuQuantityAsync( string sku, string warehouseLocationCountryCode, int quantity, CancellationToken token );
	}
}