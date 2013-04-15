using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Northwind.ServiceBase;

namespace Northwind.ServiceModel.Dto
{
	/// <summary>
	/// Clase que representa 
	/// </summary>
	public class Customers : IDto
	{
		#region Miembros de IDto

		/// <summary>
		/// Identificador
		/// </summary>
		public int Id { get; set; }

		#endregion
	}
}
