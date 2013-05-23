using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.ServiceBase
{
	/// <summary>
	/// Clase que representa un Dto
	/// </summary>
	public class CommonDto : IDto
	{
		#region Miembros de IDto

		public Uri Link { get; set; }

		#endregion
	}
}
