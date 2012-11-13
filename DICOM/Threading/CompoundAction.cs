using System;
using System.Collections.Generic;

namespace Dicom.Threading {
	public class CompoundAction {
		private Action _action;
		private List<Action> _actions;

		public CompoundAction() {
			_actions = new List<Action>();
			_action = () => {
				foreach (Action action in _actions)
					if (action != null) action();
			};
		}

		public CompoundAction(params Action[] actions) : this() {
			_actions.AddRange(actions);
		}

		public void Add(Action action) {
			_actions.Add(action);
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

		public static implicit operator Action(CompoundAction compound) {
			return compound._action;
		}
	}
}
