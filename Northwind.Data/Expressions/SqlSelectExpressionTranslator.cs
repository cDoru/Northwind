using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Northwind.Data.Model;

namespace Northwind.Data.Expressions
{
	/// <summary>
	/// 
	/// </summary>
	/// <see cref="http://blogs.msdn.com/b/mattwar/archive/2007/07/31/linq-building-an-iqueryable-provider-part-ii.aspx"/>
	/// <see cref="http://blogs.msdn.com/b/mattwar/archive/2007/08/02/linq-building-an-iqueryable-provider-part-iv.aspx"/>
	internal class SqlSelectExpressionTranslator : ExpressionVisitor
	{
		List<String> _columns = new List<String>();

		public List<String> Columns
		{
			get { return _columns; }
			private set { _columns = value; }
		}

		#region Constructor

		/// <summary>
		/// 
		/// </summary>
		public SqlSelectExpressionTranslator()
			: base()
		{
		}

		#endregion		

		#region Métodos públicos

		/// <summary>
		/// 
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		public String Translate( Expression expression )
		{			
			Visit(expression);

			return String.Join(",", Columns);
		}

		#endregion

		#region Overrides		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		protected override MemberBinding VisitMemberBinding( MemberBinding node )
		{
			Columns.Add(node.Member.Name);

			return base.VisitMemberBinding(node);
		}

		#endregion
	}
}
