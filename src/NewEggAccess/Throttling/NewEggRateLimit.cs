using System;

namespace NewEggAccess.Throttling
{
	public class NewEggRateLimit
	{
		public int Limit { get; set; }
		public int Remaining { get; set; }
		public DateTime ResetTime { get; set; }
	}
}