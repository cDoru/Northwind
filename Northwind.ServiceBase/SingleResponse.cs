using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.ServiceBase
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TResult"></typeparam>
	public class SingleResponse<TResult> : IResponse<TResult>
		where TResult : IDto, new()
	{
		#region Miembros de IResponse<TResult>

		public TResult Result { get; set; }

		#endregion

		#region Constructores

		public SingleResponse()
		{
			
		}

		public SingleResponse( TResult result )
		{
			Result = result;
		}

		#endregion
	}
}
