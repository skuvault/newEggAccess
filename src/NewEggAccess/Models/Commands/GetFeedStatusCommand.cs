using CuttingEdge.Conditions;
using NewEggAccess.Configuration;

namespace NewEggAccess.Models.Commands
{
	public class GetFeedStatusCommand : NewEggCommand
	{
		public GetFeedStatusCommand( NewEggConfig config, NewEggCredentials credentials, string payload ) : base( config, credentials, "/datafeedmgmt/feeds/status" )
		{
			Condition.Requires( payload, "payload" ).IsNotNullOrWhiteSpace();

			this.Payload = payload;
		}
	}
}