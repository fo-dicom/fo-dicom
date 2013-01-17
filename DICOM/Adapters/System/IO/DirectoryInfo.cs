// ReSharper disable CheckNamespace
namespace System.IO
// ReSharper restore CheckNamespace
{
	internal class DirectoryInfo
	{
		#region FIELDS

		private readonly string _path;

		#endregion

		#region CONSTRUCTORS

		internal DirectoryInfo(string path)
		{
			_path = path;
		}

		#endregion

		#region METHODS

		internal bool Exists
		{
			get { return Directory.Exists(_path); }
		}

		internal void Create()
		{
			Directory.CreateDirectory(_path);
		}
 
		#endregion
	}
}