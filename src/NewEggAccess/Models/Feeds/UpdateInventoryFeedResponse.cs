using System;

namespace NewEggAccess.Models.Feeds
{
	public class UpdateInventoryFeedResponse
	{
		public string RequestId { get; set; }
		public string RequestType { get; set; }
		public DateTime RequestDate { get; set; }
		public string RequestStatus { get; set; }
	}
}