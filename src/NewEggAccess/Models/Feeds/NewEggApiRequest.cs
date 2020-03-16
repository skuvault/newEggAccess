namespace NewEggAccess.Models.Feeds
{
	public class NewEggApiRequest< T > where T : class, new()
	{
		public string OperationType { get; set; }
		public T RequestBody { get; set; }
	}
}