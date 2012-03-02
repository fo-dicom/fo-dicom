using System;
using System.Linq;
using System.Threading;

namespace Dicom {
	internal static class Extensions {
		public static void InvokeAsync(this Delegate delegate_, params object[] args) {
			ThreadPool.QueueUserWorkItem(delegate(object state) {
				delegate_.DynamicInvoke(args);
			});
		}
	}
}
