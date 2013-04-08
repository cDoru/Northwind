using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace Northwind.ServiceBase
{
	/// <summary>
	/// Clase que representa un servicio web
	/// </summary>
	public class ServiceBase<TDto, TRepository> : Service
		where TDto : class
		where TRepository : class		
	{
	}
}
