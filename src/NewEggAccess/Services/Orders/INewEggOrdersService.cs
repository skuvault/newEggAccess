using NewEggAccess.Models.Orders;
using NewEggAccess.Shared;
using NewEggAccess.Throttling;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewEggAccess.Services.Orders
{
	public interface INewEggOrdersService
	{
		Throttler Throttler { get; }
		Task< IEnumerable< NewEggOrder > > GetModifiedOrdersAsync( DateTime startDateUtc, DateTime endDateUtc, string countryCode, CancellationToken token, Mark mark = null );
	}
}