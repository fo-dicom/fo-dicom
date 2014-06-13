using System;
using System.Threading.Tasks;

namespace Dicom.Threading {
	public static class ActionExtensions {
		public static void InvokeAsync(this Action action) {
			action.BeginInvoke(OnEndInvoke, action);
		}
		private static void OnEndInvoke(IAsyncResult result) {
			try {
				Action action = result.AsyncState as Action;
				action.EndInvoke(result);
			} catch {
			}
		}
	}

	public class ActionCallback {
		private Action _action;

		public ActionCallback(Action action) {
			_action = action;
		}

		public void Invoke() {
			_action.Invoke();
		}

		public IAsyncResult BeginInvoke(AsyncCallback callback, object @object) {
			return _action.BeginInvoke(callback, @object);
		}

		public void EndInvoke(IAsyncResult result) {
			_action.EndInvoke(result);
		}

		public static implicit operator Action(ActionCallback callback) {
			return callback._action;
		}

		public static implicit operator Delegate(ActionCallback callback) {
			return callback._action;
		}
	}

	public class ActionCallback<T1> : ActionCallback {
		public ActionCallback(Action<T1> action, T1 arg0) : base(() => { action(arg0); }) {
		}
	}

	public class ActionCallback<T1, T2> : ActionCallback {
		public ActionCallback(Action<T1, T2> action, T1 arg0, T2 arg1) : base(() => { action(arg0, arg1); }) {
		}
	}

	public class ActionCallback<T1, T2, T3> : ActionCallback {
		public ActionCallback(Action<T1, T2, T3> action, T1 arg0, T2 arg1, T3 arg2) : base(() => { action(arg0, arg1, arg2); }) {
		}
	}

	public class ActionCallback<T1, T2, T3, T4> : ActionCallback {
		public ActionCallback(Action<T1, T2, T3, T4> action, T1 arg0, T2 arg1, T3 arg2, T4 arg3) : base(() => { action(arg0, arg1, arg2, arg3); }) {
		}
	}
}
