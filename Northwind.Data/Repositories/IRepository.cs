﻿#region Licencia
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
		TEntity Add( TEntity entity );

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
		TEntity Get( object id );

		/// <summary>
		/// Obtiene todos los registros
		/// </summary>
		/// <returns>Una lista de <typeparamref name="TEntity"/></returns>
		IEnumerable<TEntity> GetAll();

		/// <summary>
		/// Obtiene registros según los límites indicados
		/// </summary>
		/// <param name="start">Índice del primer registro que se recuperará</param>
		/// <param name="limit">Número de registros a recuperar</param>
		/// <returns>Una lista de <typeparamref name="TEntity"/></returns>
		IEnumerable<TEntity> GetAll(int start, int limit);

		/// <summary>
		/// Obtiene los registros según la selección indicada
		/// </summary>
		/// <param name="select">Expresión de selección</param>
		/// <returns>Una lista de <typeparamref name="TEntity"/></returns>
		IEnumerable<TEntity> GetAll( Expression<Func<TEntity, object>> selector );
		
		/// <summary>
		/// Devuelve todos los registros que cumplen la expresión <paramref name="filter"/>
		/// </summary>
		/// <param name="filter">Expresión de filtrado</param>
		/// <returns>Una lista de TEntity</returns>
		IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter);

		/// <summary>
		/// Devuelve el número total de registros
		/// </summary>
		/// <returns>El número de registros</returns>
		long Count();
		
	}
}
