using CuttingEdge.Conditions;
using NewEggAccess.Configuration;
using System.Collections.Generic;
using System.Linq;

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
				var urlParameters = string.Join( "&", this._urlParameters.Select( pair => string.Format( "{0}={1}", pair.Key, pair.Value ) ) );
				return string.Format( "{0}{1}{2}?{4}", this.Config.ApiBaseUrl, this.GetPlatformUrl( this.Config.Platform ), this.RelativeUrl, this.Credentials.SellerId, urlParameters ); 
			}
		}
		public string Payload { get; protected set; }

		private Dictionary< string, string > _urlParameters { get; set; }

		protected NewEggCommand( NewEggConfig config, NewEggCredentials credentials, string relativeUrl )
		{
			Condition.Requires( config, "config" ).IsNotNull();
			Condition.Requires( credentials, "credentials" ).IsNotNull();

			this.Config = config;
			this.Credentials = credentials;
			this.RelativeUrl = relativeUrl;
			this._urlParameters = new Dictionary< string, string >
			{
				{ "sellerId", this.Credentials.SellerId }
			};
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

		protected void AddUrlParameter( string name, string value )
		{
			Condition.Requires( name, "name" ).IsNotNullOrWhiteSpace();
			Condition.Requires( value, "value" ).IsNotNullOrWhiteSpace();

			this._urlParameters.Add( name, value );
		}
	}
}