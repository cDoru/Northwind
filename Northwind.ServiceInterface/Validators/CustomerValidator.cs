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
