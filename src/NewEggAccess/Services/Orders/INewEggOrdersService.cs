using NewEggAccess.Models.Orders;
using NewEggAccess.Shared;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewEggAccess.Services.Orders
{
	public interface INewEggOrdersService
	{
		Task< IEnumerable< NewEggOrder > > GetModifiedOrdersAsync( DateTime startDateUtc, DateTime endDateUtc, string countryCode, CancellationToken token, Mark mark = null );
	}
}