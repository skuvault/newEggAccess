using FluentAssertions;
using NewEggAccess.Services.Orders;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewEggTests
{
	[ TestFixture ]
	public class OrderTests : BaseTest
	{
		private INewEggOrdersService _orderService;

		[ SetUp ]
		public void Init()
		{
			this._orderService = new NewEggOrdersService( base.Config, base.Credentials );
		}

		[ Test ]
		public async Task GetModifiedOrders()
		{
			var orders = await this._orderService.GetModifiedOrdersAsync( DateTime.Now.AddDays( -1 ), DateTime.Now, WarehouseLocationCountryCode, CancellationToken.None );

			orders.Should().NotBeEmpty();
		}
	}
}
