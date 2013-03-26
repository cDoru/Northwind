ESTRUCTURA DE LA SOLUCIÓN
--------------------------

Northwind.Data			// Datos (modelo, dtos y repositorios)
	Model			
	Dto			
	Repositories		
Northwind.Host				// Servicio web
Northwind.ServiceBase		// Clases base de servicio (ServiceBase, etc.)
NorthWind.ServiceInterface	// Implementación del servicio
	Services
		CustomerService
		EmployeeService
		[...]
NorthWind.ServiceModel		// Clases base de servicio
	Contracts
		IRequest		
		IResponse
		Request
		Response
		CollectionResponse
		[...]
		 