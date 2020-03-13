using NewEggAccess.Configuration;
using NewEggAccess.Services.Feeds;
using NewEggAccess.Services.Items;

namespace NewEggAccess
{
	public interface INewEggFactory
	{
		INewEggItemsService CreateItemsService( NewEggConfig config, string sellerId, string secretKey );
		INewEggFeedsService CreateFeedsService( NewEggConfig config, string sellerId, string secretKey );
	}
}