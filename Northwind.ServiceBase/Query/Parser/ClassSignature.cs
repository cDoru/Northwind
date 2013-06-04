using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Northwind.ServiceBase.Query.Parser
{
	/// <summary>
	/// Clase que representa una firma única para un tipo
	/// </summary>
	/// <remarks>Adaptado de System.Linq.Dynamic</remarks>
	internal class ClassSignature : IEquatable<ClassSignature>
	{
		/// <summary>
		/// Propiedades que forman la firma
		/// </summary>
		public IEnumerable<MemberInfo> Properties { get; private set; }

		/// <summary>
		/// HashCode de esta clase
		/// </summary>
		public int _hashCode;

		#region Constructor

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		/// <param name="properties"></param>
		public ClassSignature(IEnumerable<MemberInfo> properties)
		{
			Properties = properties;
			_hashCode = 0;

			Properties.ToList().ForEach(p => _hashCode ^= p.Name.GetHashCode() ^ p.GetType().GetHashCode());
		}

		#endregion

		#region Miembros de IEquatable<ClassSignature>

		/// <summary>
		/// 
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals( ClassSignature other )
		{
			if ( Properties.Count() != other.Properties.Count() ) return false;

			if ( !Enumerable.SequenceEqual(Properties, other.Properties) ) return false;

			return true;
		}

		#endregion

		#region Overrides

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals( object obj )
		{
			return (obj is ClassSignature ? Equals((ClassSignature)obj) : false);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return _hashCode;
		}

		#endregion
	}
}
