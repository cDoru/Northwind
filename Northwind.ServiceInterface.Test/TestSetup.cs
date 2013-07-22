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
