﻿using CuttingEdge.Conditions;
using NewEggAccess.Configuration;
using NewEggAccess.Services.Items;

namespace NewEggAccess
{
	public class NewEggFactory : INewEggFactory
	{
		private string _developerApiKey;

		public NewEggFactory( string developerApiKey )
		{
			Condition.Requires( developerApiKey, "developerApiKey" ).IsNotNullOrWhiteSpace();

			this._developerApiKey = developerApiKey;
		}

		public INewEggItemsService CreateItemsService( NewEggConfig config, string sellerId, string secretKey )
		{
			var credentials = new NewEggCredentials( sellerId, this._developerApiKey, secretKey );

			return new NewEggItemsService( config, credentials );
		}
	}
}