﻿namespace E_Study.Repository.Infrastructures
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Change state of entity to added
        /// </summary>
        /// <param name="entity"></param>
        void Create(TEntity entity);
        Task CreateAsync(TEntity entity);  // Asynchronous version

        /// <summary>
        /// Change state of entity to deleted
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// Delete <paramref name="TEntity"></paramref> from database
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        void Delete(string id);

        /// <summary>
        /// Change state of entity to modified
        /// </summary>
        /// <param name="entity"></param>

        void Delete(params object[] keyValues);

        void Update(TEntity entity);

        /// <summary>
        /// Get all <paramref name="TEntity"></paramref> from database by Id
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Get <paramref name="TEntity"></paramref> from database
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        TEntity GetById(params object[] primaryKey);
        Task<TEntity> GetByIdAsync(params object[] primaryKey);  // Asynchronous version
    }
}
