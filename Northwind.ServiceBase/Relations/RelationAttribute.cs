using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Northwind.Common;

namespace Northwind.ServiceBase.Relations
{
	/// <summary>
	/// Atributo que define una relación entre clases
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class RelationAttribute : Attribute
	{
		/// <summary>
		/// Tipo de relación
		/// </summary>
		public RelationType Type { get; private set; }

		/// <summary>
		/// Entidad destino de la relación
		/// </summary>
		public Type TargetEntity { get; private set; }

		/// <summary>
		/// Constructor de clase
		/// </summary>
		/// <param name="relationType"><see cref="RelationType"/></param>
		/// <param name="targetEntity">Entidad destino de la relación</param>
		public RelationAttribute( RelationType relationType, Type targetEntity )
		{
			Verify.ArgumentNotNull(relationType, "relationType");
			Verify.ArgumentNotNull(targetEntity, "relationType");

			Type = relationType;
			TargetEntity = targetEntity;
		}
	}
}
