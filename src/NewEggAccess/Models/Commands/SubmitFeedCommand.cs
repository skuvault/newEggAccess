using CuttingEdge.Conditions;
using NewEggAccess.Configuration;

namespace NewEggAccess.Models.Commands
{
	public class SubmitFeedCommand : NewEggCommand
	{
		public SubmitFeedCommand( NewEggConfig config, NewEggCredentials credentials, SubmitFeedRequestTypeEnum requestType, string payload ) : base( config, credentials, SubmitFeedServiceUrl )
		{
			Condition.Requires( payload, "payload" ).IsNotNullOrWhiteSpace();

			this.Payload = payload;
			base.AddUrlParameter( "requesttype", requestType.ToString().ToUpper() );
		}
	}

	public enum SubmitFeedRequestTypeEnum
	{
		Inventory_Data,
		Item_Data,
		Price_Data,
		Order_Ship_Notice_Data,
		MultiChannel_Order_Data,
		Volume_Discount_Data,
		Item_Warranty_Data
	}
}