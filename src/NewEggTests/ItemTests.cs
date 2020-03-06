using FluentAssertions;
using NewEggAccess.Services.Items;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewEggTests
{
	[ TestFixture ]
	public class ItemTests : BaseTest
	{
		private INewEggItemsService _itemsService;
		private const string testSku1 = "testSku1";
		private const string warehouseLocationCountryCode = "USA";

		[ SetUp ]
		public void Init()
		{
			this._itemsService = new NewEggItemsService( base.Config, base.Credentials );
		}

		[ Test ]
		public async Task GetItemInventoryThatExists()
		{
			var itemInventory = await this._itemsService.GetSkuInventory( testSku1, warehouseLocationCountryCode, CancellationToken.None );
			
			itemInventory.SellerPartNumber.ToLower().Should().Be( testSku1.ToLower() );
			itemInventory.InventoryAllocation.First().AvailableQuantity.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetItemInventoryThatDoesntExist()
		{
			var sku = Guid.NewGuid().ToString();
			var itemInventory = await this._itemsService.GetSkuInventory( sku, warehouseLocationCountryCode, CancellationToken.None );
			itemInventory.Should().BeNull();
		}

		[ Test ]
		public async Task UpdateItemInventoryThatExists()
		{
			var quantity = new Random().Next( 1, 100 );
			var itemInventory = await this._itemsService.UpdateSkuQuantityAsync( testSku1, warehouseLocationCountryCode, quantity, CancellationToken.None );

			itemInventory.Should().NotBeNull();
			itemInventory.SellerPartNumber.ToLower().Should().Be( testSku1.ToLower() );
			itemInventory.InventoryList.First().AvailableQuantity.Should().Be( quantity );
		}

		[ Test ]
		public async Task UpdateItemInventoryThatDoesntExist()
		{
			var quantity = new Random().Next( 1, 100 );
			var sku = Guid.NewGuid().ToString();
			var itemInventory = await this._itemsService.UpdateSkuQuantityAsync( sku, warehouseLocationCountryCode, quantity, CancellationToken.None );

			itemInventory.Should().BeNull();
		}
	}
}