using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Northwind.Data.Model;

namespace Northwind.Test.Data
{
	public static class NorthwindFactory
	{
		public static readonly List<Type> ModelTypes = new List<Type> {
                        typeof(EmployeeEntity),
                        typeof(CategoryEntity),
                        typeof(CustomerEntity),
                        typeof(ShipperEntity),
                        typeof(SupplierEntity),
                        typeof(OrderEntity),
                        typeof(ProductEntity),
                        typeof(OrderDetailEntity),
                        typeof(CategoryEntity),
                        typeof(RegionEntity),
                        typeof(TerritoryEntity),
                        typeof(EmployeeTerritoryEntity),
                };

		public static CategoryEntity Category( int id, string categoryName, string description)
		{
			return new CategoryEntity
			{
				Id = id,
				CategoryName = categoryName,
				Description = description,				
				LastUpdated = DateTime.Now
			};
		}

		public static CustomerEntity Customer(
				string customerId, string companyName, string contactName, string contactTitle,
				string address, string city, string region, string postalCode, string country,
				string phoneNo, string faxNo)
		{
			return new CustomerEntity
			{
				Id = customerId,
				CompanyName = companyName,
				ContactName = contactName,
				ContactTitle = contactTitle,
				Address = address,
				City = city,
				Region = region,
				PostalCode = postalCode,
				Country = country,
				Phone = phoneNo,
				Fax = faxNo,
				LastUpdated = DateTime.Now
			};
		}

		public static EmployeeEntity Employee(
				int employeeId, string lastName, string firstName, string title,
				string titleOfCourtesy, string birthDate, string hireDate,
				string address, string city, string region, string postalCode, string country,
				string homePhone, string extension,
				string photo, string notes, int reportsTo, string photoPath )
		{
			return new EmployeeEntity
			{
				Id = employeeId,
				LastName = lastName,
				FirstName = firstName,
				Title = title,
				TitleOfCourtesy = titleOfCourtesy,
				BirthDate = birthDate,
				HireDate = hireDate,
				Address = address,
				City = city,
				Region = region,
				PostalCode = postalCode,
				Country = country,
				HomePhone = homePhone,
				Extension = extension,
				Photo = photo,
				Notes = notes,
				ReportsTo = reportsTo,
				PhotoPath = photoPath,
				LastUpdated = DateTime.Now
			};
		}

		public static ShipperEntity Shipper( int id, string companyName, string phoneNo )
		{
			return new ShipperEntity
			{
				Id = id,
				CompanyName = companyName,
				Phone = phoneNo,
				LastUpdated = DateTime.Now
			};
		}

		public static SupplierEntity Supplier(
				int supplierId, string companyName, string contactName, string contactTitle,
				string address, string city, string region, string postalCode, string country,
				string phoneNo, string faxNo,
				string homePage )
		{
			return new SupplierEntity
			{
				Id = supplierId,
				CompanyName = companyName,
				ContactName = contactName,
				ContactTitle = contactTitle,
				Address = address,
				City = city,
				Region = region,
				PostalCode = postalCode,
				Country = country,
				Phone = phoneNo,
				Fax = faxNo,
				HomePage = homePage,
				LastUpdated = DateTime.Now
			};
		}

		public static OrderEntity Order(
				int orderId, string customerId, int employeeId, string orderDate, string requiredDate,
				string shippedDate, int shipVia, decimal freight, string shipName,
				string address, string city, string region, string postalCode, string country )
		{
			return new OrderEntity
			{
				Id = orderId,
				CustomerId = customerId,
				EmployeeId = employeeId,
				OrderDate = orderDate,
				RequiredDate = requiredDate,
				ShippedDate = shippedDate,
				ShipVia = shipVia,
				Freight = freight,
				ShipName = shipName,
				ShipAddress = address,
				ShipCity = city,
				ShipRegion = region,
				ShipPostalCode = postalCode,
				ShipCountry = country,
				LastUpdated = DateTime.Now
			};
		}

		public static ProductEntity Product(
				int productId, string productName, int supplierId, int categoryId,
				string qtyPerUnit, decimal unitPrice, short unitsInStock,
				short unitsOnOrder, short reorderLevel, int discontinued )
		{
			return new ProductEntity
			{
				Id = productId,
				ProductName = productName,
				SupplierId = supplierId,
				CategoryId = categoryId,
				QuantityPerUnit = qtyPerUnit,
				UnitPrice = unitPrice,
				UnitsInStock = unitsInStock,
				UnitsOnOrder = unitsOnOrder,
				ReorderLevel = reorderLevel,
				Discontinued = discontinued,
				LastUpdated = DateTime.Now
			};
		}

		public static OrderDetailEntity OrderDetail(
				int orderId, int productId, decimal unitPrice, short quantity, double discount )
		{
			return new OrderDetailEntity
			{
				OrderId = orderId,
				ProductId = productId,
				UnitPrice = unitPrice,
				Quantity = quantity,
				Discount = discount,
				LastUpdated = DateTime.Now
			};
		}

		public static RegionEntity Region(
				int regionId, string regionDescription )
		{
			return new RegionEntity
			{
				Id = regionId,
				RegionDescription = regionDescription,
				LastUpdated = DateTime.Now
			};
		}

		public static TerritoryEntity Territory(
				string territoryId, string territoryDescription, int regionId )
		{
			return new TerritoryEntity
			{
				Id = territoryId,
				TerritoryDescription = territoryDescription,
				RegionId = regionId,
				LastUpdated = DateTime.Now
			};
		}

		public static EmployeeTerritoryEntity EmployeeTerritory(
				int employeeId, string territoryId )
		{
			return new EmployeeTerritoryEntity
			{
				EmployeeId = employeeId,
				TerritoryId = territoryId,
				LastUpdated = DateTime.Now
			};
		}

	}
}
