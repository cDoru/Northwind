using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Northwind.Host.Web
{
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// Inicia la aplicación
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Application_Start( object sender, EventArgs e )
		{
			AppHost.Start();
		}

		protected void Session_Start( object sender, EventArgs e )
		{

		}

		protected void Application_BeginRequest( object sender, EventArgs e )
		{

		}

		protected void Application_AuthenticateRequest( object sender, EventArgs e )
		{

		}

		protected void Application_Error( object sender, EventArgs e )
		{

		}

		protected void Session_End( object sender, EventArgs e )
		{

		}

		protected void Application_End( object sender, EventArgs e )
		{

		}
	}
}