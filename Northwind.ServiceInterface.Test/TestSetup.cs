using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Northwind.Services.Test
{
	/// <summary>
	/// Clase base para funciones de test
	/// </summary>
	//[SetUpFixture]
	public class TestSetup
	{
		private TestAppHost _appHost = null;

		[SetUp]
		public void Setup()
		{
			_appHost = new TestAppHost();
			_appHost.Init();
			_appHost.Start(TestConfig.AbsoluteBaseUri.ToString());
			Console.WriteLine("Setup");
		}

		[TearDown]
		public void TearDown()
		{
			if ( _appHost != null )
			{
				_appHost.Stop();
				_appHost.Dispose();
			}
		}
	}
}
