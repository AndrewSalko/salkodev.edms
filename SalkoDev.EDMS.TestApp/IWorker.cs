using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalkoDev.EDMS.TestApp
{
	interface IWorker: IDisposable
	{
		void DoWork(BackgroundWorker worker);
	}
}
