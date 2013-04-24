using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.OrmLite;
using Northwind.Data.Model;

namespace Northwind.Data.Repositories
{
	/// <summary>
	/// Clase que representa un repositorio
	/// </summary>
	public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity, new()
	{
		#region Propiedades

		/// <summary>
		/// Conexión a la base de datos
		/// </summary>
		protected IDbConnectionFactory dbFactory { get; set; }

		#endregion

		#region Constructor
		/// <summary>
		/// Constructor de la clase
		/// </summary>
		/// <param name="factory">Conexión</param>
		public Repository( IDbConnectionFactory factory )
		{
			dbFactory = factory;
		}
		#endregion

		#region Miembros de IRepository<TEntity>

		/// <summary>
		/// Añade la entidad TEntity a la base de datos
		/// </summary>
		/// <param name="entity">Entidad a añadir</param>	
		public void Add( TEntity entity )
		{
			using ( var db = dbFactory.OpenDbConnection() )
			{
				db.Insert(entity);

				// TODO: Hay que asegurarse que OrmLite actualiza la propiedad Id
			}
		}

		/// <summary>
		/// Añade todas las entidades de la lista a la base de datos
		/// </summary>
		/// <param name="entities">Lista de entidades</param>
		public void AddAll( IEnumerable<TEntity> entities )
		{
			using ( var db = dbFactory.OpenDbConnection() )
			{				
				using ( var tr = db.OpenTransaction() )
				{
					// TODO: Si OrmLite actualiza la clave primaria
					//db.InsertAll(entities);

					foreach ( var e in entities )
					{
						Add(e);
					}

					tr.Commit();
				}
			}
		}

		/// <summary>
		/// Actualiza la entidad TEntity
		/// </summary>
		/// <param name="entity">Entidad a actualizar</param>
		/// <returns>true o false</returns>
		public void Update( TEntity entity )
		{
			using ( var db = dbFactory.OpenDbConnection() )
			{
				db.Update(entity);
			}
		}

		/// <summary>
		/// Actualiza todas las entidades de la lista
		/// </summary>
		/// <param name="entities">Lista de entidades</param>
		public void UpdateAll( IEnumerable<TEntity> entities )
		{
			using ( var db = dbFactory.OpenDbConnection() )
			{
				db.UpdateAll(entities);
			}
		}

		/// <summary>
		/// Elimina la entidad TEntity
		/// </summary>
		/// <param name="entity">Entidad a eliminar</param>
		/// <returns>true o false</returns>
		public void Delete( TEntity entity )
		{
			using ( var db = dbFactory.OpenDbConnection() )
			{
				db.Delete<TEntity>(entity);
			}
		}

		/// <summary>
		/// Elimina todos los elementos de la lista
		/// </summary>
		/// <param name="entities">Elementos a eliminar</param>
		public void DeleteAll( IEnumerable<TEntity> entities )
		{
			using ( var db = dbFactory.OpenDbConnection() )
			{
				db.DeleteAll(entities);
			}
		}

		/// <summary>
		/// Obtiene un elemento por su clave primaria
		/// </summary>
		/// <param name="id">Valor de la clave</param>
		/// <returns>TEntity</returns>
		public TEntity Get( object id )
		{
			using ( var db = dbFactory.OpenDbConnection() )
			{
				return db.GetById<TEntity>(id);
			}
		}

		/// <summary>
		/// Devuelve todos los registros que cumplen la expresión <paramref name="filter"/>
		/// </summary>
		/// <param name="filter">Expresión de filtrado</param>
		/// <returns>Una lista de TEntity</returns>
		public IEnumerable<TEntity> GetFiltered( System.Linq.Expressions.Expression<Func<TEntity, bool>> filter )
		{
			using ( var db = dbFactory.OpenDbConnection() )
			{
				return db.Select(filter);
			}
		}

		#endregion
	}
}
