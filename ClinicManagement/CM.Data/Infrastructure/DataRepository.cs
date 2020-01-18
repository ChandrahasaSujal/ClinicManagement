using CM.Data.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CM.Data.Infrastructure
{
    public class DataRepository<T> where T : class
    {
        #region "Private Member(s)"

        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        private bool _isDisposed;

        #endregion "Private Member(s)"

        #region "Constructor"

        /// <summary>
        /// Public Constructor
        /// </summary>
        /// <param name="context"></param>
        public DataRepository(DbContext context)
        {
            try
            {
                this._dbContext = context;
                this._dbSet = _dbContext.Set<T>();
                this._isDisposed = false;
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
            }
        }

        #endregion

        #region "Public properties"

        /// <summary>
        /// Property fetches the total Count from the dbset.
        /// </summary>
        public int TotalCount
        {
            get { return _dbSet.Count(); }
        }

        #endregion "Public properties"

        #region "Private Method(s)"

        /// <summary>
        /// Append is deleted property into predicate so that,
        ///  only items with IsDeleted=false will be retured from query.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private Expression<Func<T, bool>> AppendIsDeletedIntoPredicate(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (typeof(T).GetProperty("IsDeleted") != null)
                {
                    if (predicate != null)
                    {
                        Expression deleteProperty = Expression.Property(predicate.Parameters[0], "IsDeleted");
                        var value = Expression.Constant(false);
                        Expression deleteExpression = Expression.Equal(deleteProperty, value);
                        var andExp = Expression.AndAlso(deleteExpression, predicate.Body);
                        var lambda = Expression.Lambda<Func<T, bool>>(andExp, predicate.Parameters[0]);
                        return lambda;
                    }
                    else
                    {
                        ParameterExpression argParam = Expression.Parameter(typeof(T), "x");
                        Expression deleteProperty = Expression.Property(argParam, "IsDeleted");
                        var value = Expression.Constant(false);
                        Expression deleteExpression = Expression.Equal(deleteProperty, value);
                        var lambda = Expression.Lambda<Func<T, bool>>(deleteExpression, argParam);
                        return lambda;
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
            }
            return predicate;
        }

        #endregion

        #region "Public Method(s)"

        /// <summary>
        /// Gets Count
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="isIncludeLogicalDeleted"></param>
        /// <returns></returns>
        public int Count(Expression<Func<T, bool>> predicate, bool isIncludeLogicalDeleted = false)
        {
            try
            {
                return isIncludeLogicalDeleted ? _dbSet.Count(predicate) : _dbSet.Count(AppendIsDeletedIntoPredicate(predicate));
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
                return 0;
            }
        }

        /// <summary>
        /// Gets Count
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            try
            {
                return _dbSet.Count();
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
                return 0;
            }
        }

        /// <summary>
        /// Method add the entity into the context.
        /// </summary>
        /// <param name="entity"></param>
        public T Add(T entity)
        {
            try
            {
                PropertyInfo createdOn = entity.GetType().GetProperty("CreatedDate");
                PropertyInfo modifiedOn = entity.GetType().GetProperty("ModifiedDate");
                modifiedOn.SetValue(entity, DateTime.Now, null);
                createdOn.SetValue(entity, DateTime.Now, null);
                return _dbSet.Add(entity);
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// Create proxy instance.
        /// </summary>
        /// <returns></returns>
        public T Create()
        {
            try
            {
                return _dbSet.Create<T>();
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// Method attaches the entity from the context
        /// </summary>
        /// <param name="entity"></param>
        public void Attach(T entity)
        {
            try
            {
                _dbSet.Attach(entity);
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
            }
        }

        /// <summary>
        /// Method call when explicitly updating the enteries.
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            try
            {
                this.AttachEntity(entity);
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
            }
        }

        /// <summary>
        /// Method deletes the entity from the datacontext by Id
        /// </summary>
        /// <param name="id"></param>
        public void Delete(object id)
        {
            try
            {
                var entityToDelete = _dbSet.Find(id);
                if (entityToDelete != null)
                {
                    Delete(entityToDelete);
                }
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
            }
        }

        /// <summary>
        /// Method call when explicitly delete the enteries.
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
            }
        }

        /// <summary>
        /// Method deletes the entity based on the supplied function.
        /// </summary>
        /// <param name="predicate"></param>
        public void Delete(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var entitiesToDelete = Fetch(predicate, true);
                foreach (var entity in entitiesToDelete)
                {
                    AttachEntity(entity);
                    _dbSet.Remove(entity);
                }
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
            }
        }

        /// <summary>
        /// Method mark IsDeleted to true in the the datacontext by Id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <param name="modifiedByUserId">Modified By User Id(Not required to pass if dont wants to set)</param>
        public void LogicalDelete(object id, int? modifiedByUserId = null)
        {
            try
            {
                var entityToDelete = Find(id);
                if (entityToDelete != null)
                {
                    PropertyInfo deleted = entityToDelete.GetType().GetProperty("IsDeleted");
                    if (deleted == null)
                    {
                        throw new Exception(String.Format("No isDeleted column found for the {0} entity.", typeof(T)));
                    }
                    deleted.SetValue(entityToDelete, true, null);
                    PropertyInfo modifiedDate = entityToDelete.GetType().GetProperty("ModifiedDate");
                    if (modifiedDate != null)
                        modifiedDate.SetValue(entityToDelete, DateTime.Now, null);
                }
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
            }
        }

        /// <summary>
        ///  Method mark IsDeleted to true in the the datacontext by specified filter expression.
        /// </summary>
        /// <param name="predicate">Predicate to filter</param>
        /// <param name="modifiedByUserId">Modified By User Id(Not required to pass if dont wants to set)</param>
        public void LogicalDelete(Expression<Func<T, bool>> predicate, int? modifiedByUserId = null)
        {
            try
            {
                var entitiesToDelete = Fetch(predicate);
                {
                    foreach (var entity in entitiesToDelete)
                    {
                        LogicalDelete(entity, modifiedByUserId);
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
            }
        }

        /// <summary>
        /// Method fetches the IQueryable.
        /// </summary>
        /// <param name="isIncludeLogicalDeleted">is include logical deleted ?</param>
        /// <returns></returns>
        public IQueryable<T> Fetch(bool isIncludeLogicalDeleted = false)
        {
            try
            {
                if (isIncludeLogicalDeleted)
                {
                    return _dbSet.AsQueryable();
                }
                var predicate = AppendIsDeletedIntoPredicate(null);
                if (predicate != null)
                {
                    return _dbSet.Where(predicate).AsQueryable();
                }
                return _dbSet.AsQueryable();
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// Method fetches the IQueryable based on expression.
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <param name="isIncludeLogicalDeleted">is include logical deleted ?</param>
        /// <returns></returns>
        public IQueryable<T> Fetch(Expression<Func<T, bool>> predicate, bool isIncludeLogicalDeleted = false)
        {
            try
            {
                return isIncludeLogicalDeleted ? _dbSet.Where(predicate).AsQueryable() : _dbSet.Where(AppendIsDeletedIntoPredicate(predicate)).AsQueryable();
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// Method fetches the IQueryable based on filter,size and index.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="total"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <param name="isIncludeLogicalDeleted">is include logical deleted ?</param>
        /// <returns></returns>
        public IQueryable<T> Fetch(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 50, bool isIncludeLogicalDeleted = false)
        {
            total = 0;
            try
            {
                var skipCount = index * size;
                var resetSet = isIncludeLogicalDeleted ? _dbSet.Where(predicate).AsQueryable() : _dbSet.Where(AppendIsDeletedIntoPredicate(predicate)).AsQueryable();
                resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
                total = resetSet.Count();
                return resetSet.AsQueryable();
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// Method fetches the entity based on the keys supplied.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public T Find(params object[] keys)
        {
            try
            {
                return _dbSet.Find(keys);
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// Method fetches the first or default item from the datacontext.
        /// </summary>
        /// <param name="isIncludeLogicalDeleted">is include logical deleted ?</param>
        /// <returns></returns>
        public T FirstOrDefault(bool isIncludeLogicalDeleted = false)
        {
            try
            {
                return Fetch(isIncludeLogicalDeleted).FirstOrDefault();
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
                return null;
            }
        }


        public T LastOrDefault(bool isIncludeLogicalDeleted=false)
        {
            try
            {
                return Fetch(isIncludeLogicalDeleted).LastOrDefault();
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// Method fetches the first or default item from the datacontext based on the the supplied function.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="isIncludeLogicalDeleted">is include logical deleted ?</param>
        /// <returns></returns>
        public T FirstOrDefault(Expression<Func<T, bool>> predicate, bool isIncludeLogicalDeleted = false)
        {
            try
            {
                return Fetch(predicate, isIncludeLogicalDeleted).FirstOrDefault();
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// Method fetches the true/false item from the datacontext.
        /// </summary>
        /// <param name="isIncludeLogicalDeleted">is include logical deleted ?</param>
        /// <returns></returns>
        public bool Any(bool isIncludeLogicalDeleted = false)
        {
            try
            {
                return Fetch(isIncludeLogicalDeleted).Any();
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
            }
            return false;
        }

        /// <summary>
        /// Method fetches the true/false from the datacontext based on the the supplied function.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="isIncludeLogicalDeleted">is include logical deleted ?</param>
        /// <returns></returns>
        public bool Any(Expression<Func<T, bool>> predicate, bool isIncludeLogicalDeleted = false)
        {
            try
            {
                return Fetch(predicate, isIncludeLogicalDeleted).Any();
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
            }
            return false;
        }

        /// <summary>
        /// Method Checks whether dbset has anything entity in it or not.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool Contains(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _dbSet.Any(predicate);
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
                return false;
            }
        }

        /// <summary>
        /// Method call on dispose calls.
        /// </summary>
        public void Dispose()
        {
            try
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
            }
        }

        /// <summary>
        /// Method Disposes the Context.
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            try
            {
                if (!_isDisposed)
                {
                    if (disposing)
                    {
                        _isDisposed = true;
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
            }
        }

        /// <summary>
        /// Attach entity
        /// </summary>
        /// <param name="entity"></param>
        private void AttachEntity(T entity)
        {
            try
            {
                DbEntityEntry<T> entry = _dbContext.Entry<T>(entity);
                var idPropertyInfo = entity.GetType().GetProperties().FirstOrDefault(x => x.Name.ToLower().Contains("id"));

                if (idPropertyInfo != null)
                {
                    int id;
                    Guid guid;
                    if (int.TryParse(Convert.ToString(idPropertyInfo.GetValue(entity, null)), out id))
                    {
                            var set = _dbContext.Set<T>();
                            T attachedEntity = set.Find(id); // You need to have access to key

                            if (attachedEntity != null)
                            {
                                var attachedEntry = _dbContext.Entry(attachedEntity);
                                attachedEntry.CurrentValues.SetValues(entity);
                            }
                            else
                            {
                                entry.State = EntityState.Modified; // This should attach entity
                            }
                    }
                    else if (Guid.TryParse(Convert.ToString(idPropertyInfo.GetValue(entity, null)), out guid))
                    {
                        var set = _dbContext.Set<T>();
                        T attachedEntity = set.Find(guid); // You need to have access to key

                        if (attachedEntity != null)
                        {
                            var attachedEntry = _dbContext.Entry(attachedEntity);
                            attachedEntry.CurrentValues.SetValues(entity);
                        }
                        else
                        {
                            entry.State = EntityState.Modified; // This should attach entity
                        }
                    }
                }
                PropertyInfo modifiedOn = entity.GetType().GetProperty("ModifiedDate");
                if (modifiedOn != null)
                    modifiedOn.SetValue(entity, DateTime.Now, null);
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
            }
        }

        /// <summary>
        /// Public Destructor.
        /// </summary>
        ~DataRepository()
        {
            try
            {
                Dispose(false);
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
            }
        }

        #endregion "Public Method(s)"
    }
}
