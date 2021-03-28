using System;
using System.Collections.Generic;
using System.Text;

namespace SalkoDev.BaseClasses
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class DisposableBase: IDisposable
	{
		~DisposableBase()
		{
			_Dispose(false);
		}

		public void Dispose()
		{
			_Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void _Dispose(bool disposing)
		{
		}
	}
}
