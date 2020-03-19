using CuttingEdge.Conditions;
using NewEggAccess.Configuration;

namespace NewEggAccess.Models.Commands
{
	public class GetModifiedOrdersCommand : NewEggCommand
	{
		private const int ApiVersion = 307;

		public GetModifiedOrdersCommand( NewEggConfig config, NewEggCredentials credentials, string payload ) : base( config, credentials, GetModifiedOrdersServiceUrl )
		{
			Condition.Requires( payload, "payload" ).IsNotNullOrWhiteSpace();

			this.Payload = payload;
			this.AddUrlParameter( "version", ApiVersion.ToString() );
		}
	}
}