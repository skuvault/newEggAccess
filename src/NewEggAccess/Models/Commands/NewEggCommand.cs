using CuttingEdge.Conditions;
using NewEggAccess.Configuration;

namespace NewEggAccess.Models.Commands
{
	public abstract class NewEggCommand
	{
		public NewEggConfig Config { get; private set; }
		public NewEggCredentials Credentials { get; private set; }
		public string RelativeUrl { get; private set; }

		public string Url
		{
			get 
			{ 
				return string.Format( "{0}{1}{2}?sellerId={3}", this.Config.ApiBaseUrl, this.GetPlatformUrl( this.Config.Platform ), this.RelativeUrl, this.Credentials.SellerId ); 
			}
		}
		public string Payload { get; protected set; }

		protected NewEggCommand( NewEggConfig config, NewEggCredentials credentials, string relativeUrl )
		{
			Condition.Requires( config, "config" ).IsNotNull();
			Condition.Requires( credentials, "credentials" ).IsNotNull();

			this.Config = config;
			this.Credentials = credentials;
			this.RelativeUrl = relativeUrl;
		}

		private string GetPlatformUrl( NewEggPlatform platform )
		{
			switch ( platform )
			{
				case NewEggPlatform.NewEggBusiness:
					{
						return "/b2b";
					}
				case NewEggPlatform.NewEggCA:
					{
						return "/can";
					}
				default:
					return string.Empty;
			}
		}
	}
}