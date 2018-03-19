// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace System
{
    /// <summary>
    /// .NET 3.5 implementation of <code>Lazy{T}</code> class (not thread-safe).
    /// </summary>
	internal class Lazy<T>
	{
		private readonly Func<T> _initializer;
		private bool _isValueCreated;
		private T _value;

		public Lazy(Func<T> initializer)
		{
			if (initializer == null)
				throw new ArgumentNullException(nameof(initializer));
			_initializer = initializer;
			_isValueCreated = false;
		}

		public bool IsValueCreated
		{
			get { return _isValueCreated; }
		}

		public T Value
		{
			get
			{
				if (!_isValueCreated)
				{
					_value = _initializer();
					_isValueCreated = true;
				}
				return _value;
			}
		}
	}
}