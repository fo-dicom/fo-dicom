// ReSharper disable CheckNamespace
namespace System.Collections.Generic
// ReSharper restore CheckNamespace
{
	internal static class ListExtensions
	{
		 internal static List<T> AsReadOnly<T>(this List<T> list)
		 {
			 return list;
		 }
	}
}