using System;

namespace NewEggAccess.Models.Feeds
{
	public class FeedAcknowledgment
	{
		public DateTime RequestDate { get; set; }
		public string RequestId { get; set; }
		public string RequestStatus { get; set; }
		public string RequestType { get; set; }
	}
}