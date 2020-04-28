using NewEggAccess.Models.Feeds;
using NewEggAccess.Shared;
using NewEggAccess.Throttling;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewEggAccess.Services.Feeds
{
	public interface INewEggFeedsService
	{
		Throttler Throttler { get; }
		Task< string > UpdateItemsInventoryInBulkAsync( IEnumerable< InventoryUpdateFeedItem > inventory, CancellationToken token, Mark mark = null );
		Task< FeedAcknowledgment > GetFeedStatusAsync( string feedId, CancellationToken token, Mark mark = null );
	}
}