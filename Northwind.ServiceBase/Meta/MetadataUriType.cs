using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.ServiceBase.Meta
{
	/// <summary>
	/// Define los tipos de <see cref="Uri"/> presentes en los petadatos
	/// </summary>
	public enum MetadataUriType
	{
		/// <summary>
		/// <see cref="Uri"/> que representa a la misma entidad
		/// </summary>
		Self,

		/// <summary>
		/// <see cref="Uri"/> que representa a la siguiente página de datos
		/// </summary>
		Next,

		/// <summary>
		/// <see cref="Uri"/> que representa a la anterior página de datos
		/// </summary>
		Previous,

		/// <summary>
		/// <see cref="Uri"/> que representa a la primera página de datos
		/// </summary>
		First,

		/// <summary>
		/// <see cref="Uri"/> que representa a la última página de datos
		/// </summary>
		Last
	}
}
