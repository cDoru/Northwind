using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.ServiceBase
{
	/// <summary>
	/// 
	/// </summary>
	public interface ISelectable
	{
		List<string> Select { get; set; }
	}
}
