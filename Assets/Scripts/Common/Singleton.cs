namespace Common
{
	public class Singleton<T> where T : class, new()

	{
		public static T Instance => _instance ?? (_instance = new T());
		private static T _instance;
	}
}