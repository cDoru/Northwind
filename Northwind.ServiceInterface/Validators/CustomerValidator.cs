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
using ServiceStack.FluentValidation;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using Northwind.ServiceModel.Dto;

namespace Northwind.ServiceInterface.Validators
{
	/// <summary>
	/// Clase que representa un validador de <see cref="Customer"/>
	/// </summary>
	/// <seealso cref="https://github.com/ServiceStack/ServiceStack/wiki/Validation"/>
	public class CustomerValidator : AbstractValidator<Customer>
	{
		public CustomerValidator()
		{
			// Reglas de validación para peticiones GET
			RuleSet(ApplyTo.Get | ApplyTo.Post, () =>
			{
				RuleFor(r => r.Id).NotEmpty();
			});
		}
	}
}
