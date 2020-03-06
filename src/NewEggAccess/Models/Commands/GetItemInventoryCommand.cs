using CuttingEdge.Conditions;
using NewEggAccess.Configuration;

namespace NewEggAccess.Models.Commands
{
	public class ItemInventoryCommand : NewEggCommand
	{
		public ItemInventoryCommand( NewEggConfig config, NewEggCredentials credentials, string payload ) : base( config, credentials, "/contentmgmt/item/international/inventory" )
		{
			Condition.Requires( payload, "payload" ).IsNotNullOrWhiteSpace();

			this.Payload = payload;
		}
	}
}