using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewEggAccess.Services
{
	public interface IHttpClient
	{
		void SetRequestHeader( string name, string value );
		void SetAcceptHeader( MediaTypeWithQualityHeaderValue header );
		Task< IHttpResponseMessage > GetAsync( string url );
		Task< IHttpResponseMessage > PostAsync( string url, HttpContent content, CancellationToken token );
		Task< IHttpResponseMessage > PutAsync( string url,  HttpContent content, CancellationToken token );
	}

	public interface IHttpResponseMessage
	{
		Task< string > ReadContentAsStringAsync();
		bool IsSuccessStatusCode { get; set; }
		HttpStatusCode StatusCode { get; set; }
	}

	public class DefaultHttpClient : IHttpClient
	{
		public HttpClient HttpClient { get; private set; }

		public DefaultHttpClient()
		{
			this.HttpClient = new HttpClient();
		}

		public void SetRequestHeader( string name, string value )
		{
			this.HttpClient.DefaultRequestHeaders.Add( name, value );
		}

		public void SetAcceptHeader( MediaTypeWithQualityHeaderValue header )
		{
			this.HttpClient.DefaultRequestHeaders.Accept.Add( header );
		}

		public async Task< IHttpResponseMessage > GetAsync( string url )
		{
			return new DefaultHttpResponseMessage( await this.HttpClient.GetAsync( url ) );
		}

		public async Task< IHttpResponseMessage > PostAsync( string url, HttpContent content, CancellationToken token )
		{
			return new DefaultHttpResponseMessage( await this.HttpClient.PostAsync( url, content, token ) );
		}

		public async Task< IHttpResponseMessage > PutAsync( string url, HttpContent content, CancellationToken token )
		{
			return new DefaultHttpResponseMessage( await this.HttpClient.PutAsync( url, content, token ) );
		}
	}

	public class DefaultHttpResponseMessage : IHttpResponseMessage
	{
		private HttpResponseMessage _responseMessage;

		public DefaultHttpResponseMessage( HttpResponseMessage responseMessage )
		{
			this._responseMessage = responseMessage;
		}

		public HttpStatusCode StatusCode
		{
			get
			{
				return this._responseMessage.StatusCode;
			}
			set { }
		}

		bool IHttpResponseMessage.IsSuccessStatusCode
		{
			get
			{
				return this._responseMessage.IsSuccessStatusCode;
			}
			set { }
		}

		public Task< string > ReadContentAsStringAsync()
		{
			return this._responseMessage.Content.ReadAsStringAsync();
		}
	}
}