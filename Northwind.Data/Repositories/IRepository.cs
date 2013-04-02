using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Northwind.Data.Repositories
{
	/// <summary>
	/// Interfaz que representa un Repositorio
	/// </summary>
	/// <typeparam name="TEntity">Tipo de clase para el repositorio</typeparam>
	public interface IRepository<TEntity> where TEntity : class
	{
		/// <summary>
		/// Añade la entidad TEntity a la base de datos
		/// </summary>
		/// <param name="entity">Entidad a añadir</param>
		/// <returns>La nueva entidad creada</returns>
		TEntity Add( TEntity entity );

		/// <summary>
		/// Actualiza la entidad TEntity
		/// </summary>
		/// <param name="entity">Entidad a actualizar</param>
		/// <returns>true o false</returns>
		bool Update( TEntity entity );

		/// <summary>
		/// Elimina la entidad TEntity
		/// </summary>
		/// <param name="entity">Entidad a eliminar</param>
		/// <returns>true o false</returns>
		bool Delete( TEntity entity );

		/// <summary>
		/// Devuelve todos los registros
		/// </summary>
		/// <returns>Una lista de TEntity</returns>
		IList<TEntity> GetAll();

		/// <summary>
		/// Devuelve una sóla entidad TEntity de la base de datos
		/// </summary>
		/// <param name="where">Expresión Linq a utilizar</param>
		/// <returns>La entidad TEntity</returns>
		TEntity Single( Expression<Func<TEntity, bool>> where );

		/// <summary>
		/// Devuelve la primera entidad TEntity de la base de datos
		/// </summary>
		/// <param name="where">Expresión Linq a utilizar</param>
		/// <returns>La entidad TEntity</returns>
		TEntity First( Expression<Func<TEntity, bool>> where );
	}
}
