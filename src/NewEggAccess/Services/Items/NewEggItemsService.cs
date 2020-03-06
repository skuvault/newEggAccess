using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using NewEggAccess.Configuration;
using NewEggAccess.Models;
using NewEggAccess.Models.Commands;
using NewEggAccess.Models.Items;
using NewEggAccess.Shared;
using Newtonsoft.Json;

namespace NewEggAccess.Services.Items
{
	public class NewEggItemsService : BaseService, INewEggItemsService
	{
		public NewEggItemsService( NewEggConfig config, NewEggCredentials credentials ) : base( credentials, config )
		{
		}

		/// <summary>
		///	Get sku's inventory on specified warehouseLocation
		/// </summary>
		/// <param name="sku"></param>
		/// <param name="warehouseLocationCode"></param>
		/// <param name="token"></param>
		/// <returns></returns>
		public async Task< ItemInventory > GetSkuInventory( string sku, string warehouseLocationCode, CancellationToken token )
		{
			var mark = Mark.CreateNew();

			var request = new GetItemInventoryRequest()
			{
				Type = 1,
				Value = sku,
				WarehouseList = new WarehouseList() {  WarehouseLocation = new string[] { warehouseLocationCode } }
			};

			Func< HttpStatusCode, ErrorResponse, bool > ignoreErrorHandler = ( status, error ) =>
			{
				// can't find item
				return error != null 
						&& error.Code == "CT026"
						&& status == HttpStatusCode.BadRequest;
			};

			var response = await base.PutAsync( new ItemInventoryCommand( base.Config, base.Credentials, request.ToJson() ), token, mark, ignoreErrorHandler );
			
			if ( response.Error == null )
			{
				return JsonConvert.DeserializeObject< ItemInventory >( response.Result );
			}
			
			return null;
		}

		/// <summary>
		///	Update sku's quantity in specified warehouse location
		/// </summary>
		/// <param name="sku"></param>
		/// <param name="warehouseLocation"></param>
		/// <param name="quantity"></param>
		/// <param name="token"></param>
		/// <returns></returns>
		public async Task< UpdateItemInventoryResponse > UpdateSkuQuantityAsync( string sku, string warehouseLocation, int quantity, CancellationToken token )
		{
			var mark = Mark.CreateNew();

			var request = new UpdateItemInventoryRequest()
			{
				Type = 1,
				Value = sku,
				InventoryList = new InventoryList() {  
					Inventory = new UpdateItemInventory[] { new UpdateItemInventory() { WarehouseLocation = warehouseLocation, AvailableQuantity = quantity } }
				}
			};

			Func< HttpStatusCode, ErrorResponse, bool > ignoreErrorHandler = ( status, error ) =>
			{
				// can't find item
				return error != null 
						&& error.Code == "CT026"
						&& status == HttpStatusCode.BadRequest;
			};

			var response = await base.PostAsync( new ItemInventoryCommand( base.Config, base.Credentials, request.ToJson() ), token, mark, ignoreErrorHandler );
			
			if ( response.Error == null )
			{
				return JsonConvert.DeserializeObject< UpdateItemInventoryResponse >( response.Result );
			}
			
			return null;
		}
	}
}