using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using NewEggAccess.Configuration;
using NewEggAccess.Exceptions;
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
		public async Task< ItemInventory > GetSkuInventory( string sku, string warehouseLocationCode, CancellationToken token, Mark mark = null )
		{
			if ( mark == null )
			{
				mark = Mark.CreateNew();
			}

			if ( string.IsNullOrWhiteSpace( warehouseLocationCode ) )
			{
				throw new NewEggException( "Warehouse location code is not specified!" );
			}

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
		public async Task< UpdateItemInventoryResponse > UpdateSkuQuantityAsync( string sku, string warehouseLocation, int quantity, CancellationToken token, Mark mark = null )
		{
			if ( mark == null )
			{
				mark = Mark.CreateNew();
			}

			if ( string.IsNullOrWhiteSpace( warehouseLocation ) )
			{
				throw new NewEggException( "Warehouse location code is not specified!" );
			}

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

		/// <summary>
		///	Updates skus quantities
		/// </summary>
		/// <param name="skusQuantities"></param>
		/// <param name="token"></param>
		/// <param name="mark"></param>
		/// <returns></returns>
		public async Task UpdateSkusQuantitiesAsync( Dictionary< string, int > skusQuantities, string warehouseLocationCode, CancellationToken token, Mark mark = null )
		{
			foreach( var skuQuantity in skusQuantities )
			{
				await this.UpdateSkuQuantityAsync( skuQuantity.Key, warehouseLocationCode, skuQuantity.Value, token, mark ).ConfigureAwait( false );
			}
		}
	}
}