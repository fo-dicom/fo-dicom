// ReSharper disable CheckNamespace
namespace System.IO
// ReSharper restore CheckNamespace
{
	internal class FileInfo
	{
		#region FIELDS

		private readonly string _name;
		
		#endregion

		#region CONSTRUCTORS

		internal FileInfo(string name)
		{
			_name = name;
		}
		
		#endregion

		#region PROPERTIES

		internal DirectoryInfo Directory { get { return new DirectoryInfo(Path.GetDirectoryName(_name)); } }
		
		#endregion

		#region METHODS

		internal FileStream OpenWrite()
		{
			return new FileStream(_name, FileMode.Create);
		}
		
		#endregion
	}
}