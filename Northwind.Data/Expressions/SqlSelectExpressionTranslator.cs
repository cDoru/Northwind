#region Licencia
/*
   Copyright 2013 Juan Diego Martinez

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.

*/        
#endregion
          
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
