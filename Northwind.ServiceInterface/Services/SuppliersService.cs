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
using System.Net;
using System.Text;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.Text;
using Northwind.Data.Model;
using Northwind.Data.Repositories;
using Northwind.ServiceBase;
using Northwind.ServiceModel.Contracts;
using Northwind.ServiceModel.Dto;
using Northwind.ServiceModel.Operations;

namespace Northwind.ServiceInterface.Services
{
	/// <summary>
	/// Servicio de <see cref="Supplier"/>
	/// </summary>
	public class SuppliersService : ServiceBase<SupplierEntity, Supplier>
	{
		public SuppliersService( ISupplierEntityRepository repository )
			: base(repository)
		{ }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public object Get( GetSupplier request )
		{
			return base.ToOptimizedResultUsingCache(
				() =>
				{
					return GetSingle<SupplierResponse>(request);
				});
		}

		/// <summary>
		/// Recuperación de la lista de <see cref="Supplier"/>
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public object Get( GetSuppliers request )
		{			
			return base.ToOptimizedResultUsingCache(
				() =>
				{
					return GetCollection<SuppliersCollectionResponse>(request);
				});		
		}

		/// <summary>
		/// Actualización de un <see cref="Supplier"/>
		/// </summary>
		/// <param name="request"><see cref="Supplier"/> a actualizar</param>
		/// <returns></returns>
		public object Put( Supplier request )
		{
			return base.Update(request);
		}

		/// <summary>
		/// Creación de un <see cref="Supplier"/>
		/// </summary>
		/// <param name="request"><see cref="Supplier"/> a crear</param>
		/// <returns></returns>
		public object Post( Supplier request )
		{
			return base.Insert<SupplierResponse>(request);
		}

		/// <summary>
		/// Eliminación de un <see cref="Supplier"/>
		/// </summary>
		/// <param name="request"><see cref="Supplier"/> a eliminar</param>
		/// <returns></returns>
		public object Delete( Supplier request )
		{
			return base.Remove(request);
		}
	}

}
