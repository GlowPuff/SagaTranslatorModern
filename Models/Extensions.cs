namespace Imperial_Commander_Editor
{
	public static class Extensions
	{
		public static T FirstOr<T>( this IEnumerable<T> source, T alternate )
		{
			foreach ( T t in source )
				return t;
			return alternate;
		}
	}
}
