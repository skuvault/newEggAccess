using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace NewEggAccess.Shared
{
	public static class Misc
	{
		public static string ToJson( this object source )
		{
			try
			{
				if ( source == null )
					return "{}";
				else
				{
					var serialized = JsonConvert.SerializeObject( source, new IsoDateTimeConverter() );
					return serialized;
				}
			}
			catch( Exception )
			{
				return "{}";
			}
		}

		public static List< List< T > > SplitToChunks< T >( this IEnumerable< T > source, int chunkSize )
		{
			var i = 0;
			var chunks = new List< List< T > >();
			
			while( i < source.Count() )
			{
				var temp = source.Skip( i ).Take( chunkSize ).ToList();
				chunks.Add( temp );
				i += chunkSize;
			}
			return chunks;
		}
	}
}