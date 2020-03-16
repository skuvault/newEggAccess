using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NewEggAccess.Configuration;
using NewEggAccess.Exceptions;
using NewEggAccess.Models.Commands;
using NewEggAccess.Models.Orders;
using NewEggAccess.Shared;
using Newtonsoft.Json;

namespace NewEggAccess.Services.Orders
{
	public class NewEggOrdersService : BaseService, INewEggOrdersService
	{
		public NewEggOrdersService( NewEggConfig config, NewEggCredentials credentials ) : base( credentials, config )
		{
		}

		public async Task< IEnumerable< NewEggOrder > > GetModifiedOrdersAsync( DateTime startDateUtc, DateTime endDateUtc, string countryCode, CancellationToken token, Mark mark = null )
		{
			var orders = new List< NewEggOrder >();

			if ( mark == null )
			{
				mark = Mark.CreateNew();
			}

			int pageIndex = 1;

			while ( true )
			{
				var request = new GetOrderInfoRequest(
					new GetOrderInfoRequestBody(
						pageIndex,
						base.Config.OrdersPageSize,
						new GetOrderInfoRequestCriteria()
						{
							Type = 0,
							OrderDateFrom = Misc.ConvertFromUtcToPstStr( startDateUtc ),
							OrderDateTo = Misc.ConvertFromUtcToPstStr( endDateUtc ),
							CountryCode = countryCode
						} ) );

				var ordersPageServerResponse = await base.PutAsync( new GetModifiedOrdersCommand( base.Config, base.Credentials, request.ToJson() ), token, mark, ( code, response ) => false ).ConfigureAwait( false );

				if ( ordersPageServerResponse.Error == null )
				{
					var ordersPage = JsonConvert.DeserializeObject< GetOrderInfoResponse >( ordersPageServerResponse.Result );

					if ( ordersPage.IsSuccess )
					{
						if ( ordersPage.ResponseBody.OrderInfoList != null )
						{
							orders.AddRange( ordersPage.ResponseBody.OrderInfoList.Select( o => o.ToSVOrder() ) );
							++pageIndex;
						}

						if ( ordersPage.ResponseBody.PageInfo.TotalPageCount <= pageIndex )
						{
							break;
						}
					}
					else
					{
						break;
					}
				}
				else
				{
					throw new NewEggException( ordersPageServerResponse.Error.Message );
				}
			}

			return orders.ToArray();
		}
	}
}