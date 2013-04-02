# Estructura de la solución

  * Northwind.Data. *Clases del modelo y repositorios de acceso*
    * Model			    
    * Repositories		
  * Northwind.Host. *Servicio web*
  * Northwind.ServiceBase. *Clases base de servicio*
  * NorthWind.ServiceInterface. *Clases de implementación del servicio*
    * Services
  * NorthWind.ServiceModel
    * Contracts
    * Dto

# Guía de estilo

Los servicios de este proyecto seguirán las siguientes recomendaciones

## Nombrado de los servicios

Los servicios que hagan referencia a recursos estarán nombrados como nombres comunes en plural.

## Elementos totales de una colección

Se podrá recuperar el total de registros de una colección accediendo al recurso `count` de la misma. Este recurso devolverá únicamente el número total de elementos de la colección.

`/customers/count`

## Códigos de error

Los códigos de error serán equivalentes a los códigos de [status HTTP](http://es.wikipedia.org/wiki/Anexo:C%C3%B3digos_de_estado_HTTP). Por ejemplo:

  * 200 - OK
  * 201 - Creado
  * 304 - No modificado
  * 400 - Petición incorrecta
  * 401 - No autorizado
  * 403 - Prohibido
  * 404 - No encontrado
  * 500 - Error interno del servidor

## Paginación

Se utilizarán los parámetros `offset` y `limit` para informar de la cantidad de resultados que se devuelven, donde `offset` marca el primer registro de la lista y `limit` el número de registros que se recuperarán.

Por ejemplo, para recuperar los registros del 31 al 40:

`/customers?offset=31&limit=10`

Se establecerá un valor por defecto para la recuperación de cualquier lista. Jamás se devolverán todos los registros. 

Para el ejemplo anterior, `/customers` es equivalente a `/customers?offset=1&limit=10`.

## Ordenación

Se utilizará el parámetro `order` seguido de una lista separada por comas para indicar el orden en el que se devolverán los resultados. Si no se indica nada, la lista siempre indicará orden ascendente. Para indicar una ordenación descendente, se utilizará el parámetro `desc` a continuación del elemento de la lista necesario.

Por ejemplo, para recuperar los 10 primeros registros ordenados por nombre de manera ascendente sería:

`/customers?offset=1&limit=10&order=lastName`

Para recuperar la misma lista en ordenación descendente, sería:

`/customers?offset=1&limit=10&order=lastName%20desc`

## Respuestas parciales

Se habilita la posibilidad de recuperar únicamente aquella información realmente necesaria. Para ello se utilizará el parámetro `select` seguido de una lista separada por comas con la información requerida. Con esto se ahorra ancho de banda al no tener que servir toda la información de un recurso cuando únicamente nos interesa cierta información.

Por ejemplo, para recuperar únicamente el nombre y los apellidos de un cliente, sería: 

`/customers/1234?select=name,lastName`

# Operaciones

<table>
	<thead>
		<tr>
			<td><strong>Recurso</strong></td>
			<td><strong>GET</strong></td>
			<td><strong>POST</strong></td>
			<td><strong>PUT / PATCH</strong></td>
			<td><strong>DELETE<strong></td>
		</tr>
	</thead>
	<tbody>
		<tr>
			<td>/customers</td>
			<td>Lista de customers</td>
			<td>Creación de un customer</td>
			<td>Actualización customers en bloque</td>
			<td>Elimina todos los customers</td>
		</tr>
		<tr>
			<td>/customers/1234</td>
			<td>Recuperación de un customer</td>
			<td>Error</td>
			<td>Si existe, actualiza el customer</td>
			<td>Elimina el customer</td>
		</tr>
	</tbody>
</table>

# Ejemplos

## Creación de un cliente 

**Petición**

```json
POST /customers
{
	"customer" : {
		name : "John",
		lastName : "Doe"
	}
}
```

**Respuesta**

```json
200 OK
{
	"customer" : {
		id : 1234,
		name : "John",
		lastName : "Doe"
	}
}
```
