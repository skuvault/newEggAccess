﻿using FluentAssertions;
using NewEggAccess.Services;
using NewEggAccess.Services.Orders;
using NSubstitute;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace NewEggTests
{
	[ TestFixture ]
	public class OrderTests : BaseTest
	{
		private NewEggOrdersService _orderService;
		private DateTime _startDate = DateTime.Now.AddMonths( -2 );
		private DateTime _endDate = DateTime.Now;

		[ SetUp ]
		public void Init()
		{
			this._orderService = new NewEggOrdersService( base.Config, base.Credentials );
		}

		[ Test ]
		public async Task GetModifiedOrders()
		{
			var orders = await this._orderService.GetModifiedOrdersAsync( _startDate, _endDate, WarehouseLocationCountryCode, CancellationToken.None );

			orders.Should().NotBeEmpty();
		}

		[ Test ]
		public async Task GetModifiedOrdersBySmallPage()
		{
			base.Config.OrdersPageSize = 1;
			var orders = await this._orderService.GetModifiedOrdersAsync( _startDate, _endDate, WarehouseLocationCountryCode, CancellationToken.None );

			orders.Should().NotBeEmpty();
		}

		[ Test ]
		public async Task GetModifiedOrdersMockResponse()
		{
			this._orderService.HttpClient = this.GetMock( "GetOrderInformation.json" );

			var orders = await this._orderService.GetModifiedOrdersAsync( _startDate, _endDate, WarehouseLocationCountryCode, CancellationToken.None );

			orders.Should().NotBeEmpty();
		}

		private IHttpClient GetMock( string fileWithResponsePath )
		{
			var httpClientMock = Substitute.For< IHttpClient >();
			var httpResponseMock = Substitute.For< IHttpResponseMessage >();
			httpResponseMock.ReadContentAsStringAsync().Returns( this.GetFileResponseContent( fileWithResponsePath ) );
			httpResponseMock.IsSuccessStatusCode.Returns( true );
			httpResponseMock.StatusCode.Returns( System.Net.HttpStatusCode.OK );
			
			httpClientMock.PutAsync( null, null, CancellationToken.None ).ReturnsForAnyArgs( httpResponseMock );

			return httpClientMock;
		}

		private string GetFileResponseContent( string fileName )
		{
			string basePath = new Uri( Path.GetDirectoryName( Assembly.GetExecutingAssembly().CodeBase ) ).LocalPath;
			var path = basePath + @"\..\..\Responses\" + fileName;

			return File.ReadAllText( path );
		}
	}
}
