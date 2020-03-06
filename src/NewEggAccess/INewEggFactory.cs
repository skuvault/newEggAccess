using NewEggAccess.Configuration;
using NewEggAccess.Services.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewEggAccess
{
	public interface INewEggFactory
	{
		INewEggItemsService CreateItemsService( NewEggConfig config, string sellerId, string secretKey );
	}
}