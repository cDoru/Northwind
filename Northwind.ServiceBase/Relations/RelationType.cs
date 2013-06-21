using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.ServiceBase.Relations
{
	/// <summary>
	/// Define los tipos de relaciones entre <see cref="Dto"/>
	/// </summary>
	public enum RelationType
	{
		/// <summary>
		/// Define una relación de 1-1
		/// </summary>
		BelongsTo,

		/// <summary>
		/// Define una relación de 1-N
		/// </summary>
		HasMany,		

	}
}
