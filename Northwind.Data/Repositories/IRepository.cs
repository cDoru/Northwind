using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Northwind.Data.Model;

namespace Northwind.Data.Repositories
{
	/// <summary>
	/// Interfaz que representa un Repositorio
	/// </summary>
	/// <typeparam name="TEntity">Tipo de clase para el repositorio</typeparam>
	public interface IRepository<TEntity> where TEntity : IEntity
	{
		/// <summary>
		/// Añade la entidad TEntity a la base de datos
		/// </summary>
		/// <param name="entity">Entidad a añadir</param>		
		void Add( TEntity entity );

		/// <summary>
		/// Añade todas las entidades de la lista a la base de datos
		/// </summary>
		/// <param name="entities">Lista de entidades</param>
		void AddAll( IEnumerable<TEntity> entities );

		/// <summary>
		/// Actualiza la entidad TEntity
		/// </summary>
		/// <param name="entity">Entidad a actualizar</param>
		/// <returns>true o false</returns>
		void Update( TEntity entity );

		/// <summary>
		/// Actualiza todas las entidades de la lista
		/// </summary>
		/// <param name="entities">Lista de entidades</param>
		void UpdateAll( IEnumerable<TEntity> entities );

		/// <summary>
		/// Elimina la entidad TEntity
		/// </summary>
		/// <param name="entity">Entidad a eliminar</param>
		/// <returns>true o false</returns>
		void Delete( TEntity entity );

		/// <summary>
		/// Elimina todos los elementos de la lista
		/// </summary>
		/// <param name="entities">Elementos a eliminar</param>
		void DeleteAll( IEnumerable<TEntity> entities );

		/// <summary>
		/// Obtiene un elemento por su clave primaria
		/// </summary>
		/// <param name="id">Valor de la clave</param>
		/// <returns>TEntity</returns>
		TEntity Get( long id );

		/// <summary>
		/// Devuelve todos los registros que cumplen la expresión <paramref name="filter"/>
		/// </summary>
		/// <param name="filter">Expresión de filtrado</param>
		/// <returns>Una lista de TEntity</returns>
		IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter);
		
	}
}
