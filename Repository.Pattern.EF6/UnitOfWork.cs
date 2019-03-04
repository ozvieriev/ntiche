using Microsoft.Practices.ServiceLocation;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Pattern.EF6
{
    public class UnitOfWork : IUnitOfWorkAsync
    {
        #region Private Fields

        private DbContext _dataContext;
        private bool _disposed;
        //private ObjectContext _objectContext;
        //private DbTransaction _transaction;
        private Dictionary<string, dynamic> _repositories;

        #endregion Private Fields

        #region Constuctor/Dispose

        public UnitOfWork(DbContext dataContext)
        {
            _dataContext = dataContext;
            _repositories = new Dictionary<string, dynamic>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // free other managed objects that implement
                // IDisposable only

                //try
                //{
                //    if (_objectContext != null && _objectContext.Connection.State == ConnectionState.Open)
                //    {
                //        _objectContext.Connection.Close();
                //    }
                //}
                //catch (ObjectDisposedException)
                //{
                //    // do nothing, the objectContext has already been disposed
                //}

                if (_dataContext != null)
                {
                    _dataContext.Dispose();
                    _dataContext = null;
                }
            }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }

        #endregion Constuctor/Dispose

        public async Task<int> SaveChangesAsync()
        {
            return await _dataContext.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            return _dataContext.SaveChangesAsync(cancellationToken);
        }

        public Repositories.IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class
        {
            if (ServiceLocator.IsLocationProviderSet)
            {
                return ServiceLocator.Current.GetInstance<IRepositoryAsync<TEntity>>();
            }

            if (_repositories == null)
            {
                _repositories = new Dictionary<string, dynamic>();
            }

            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IRepositoryAsync<TEntity>)_repositories[type];
            }

            var repositoryType = typeof(Repository<>);

            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dataContext, this));

            return _repositories[type];
        }

        public int SaveChanges()
        {
            return _dataContext.SaveChanges();
        }

        public Repositories.IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (ServiceLocator.IsLocationProviderSet)
            {
                return ServiceLocator.Current.GetInstance<IRepository<TEntity>>();
            }

            return RepositoryAsync<TEntity>();
        }

        public void BeginTransaction(System.Data.IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            throw new NotImplementedException();
        }

        public bool Commit()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public int ExecuteSqlCommand(string query, params object[] parameters)
        {
            return _dataContext.Database.ExecuteSqlCommand(query, parameters);
        }

        public async Task<List<T>> ExecuteStoredProcedureAsync<T>(string query, params object[] parameters)
        {
            using (var cmd = _dataContext.Database.Connection.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddRange(parameters);

                await _dataContext.Database.Connection.OpenAsync();

                var reader = await cmd.ExecuteReaderAsync();

                var list = ((IObjectContextAdapter)_dataContext).ObjectContext.Translate<T>(reader).ToList();

                _dataContext.Database.Connection.Close();

                return list;
            }
        }
        public async Task<Tuple<List<T1>, List<T2>>> ExecuteStoredProcedureAsync<T1, T2>(string query, params object[] parameters)
        {
            //https://msdn.microsoft.com/en-us/data/jj691402.aspx

            using (var cmd = _dataContext.Database.Connection.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddRange(parameters);

                await _dataContext.Database.Connection.OpenAsync();

                var reader = await cmd.ExecuteReaderAsync();

                var list1 = ((IObjectContextAdapter)_dataContext).ObjectContext.Translate<T1>(reader).ToList();
                reader.NextResult();
                var list2 = ((IObjectContextAdapter)_dataContext).ObjectContext.Translate<T2>(reader).ToList();

                _dataContext.Database.Connection.Close();

                return new Tuple<List<T1>, List<T2>>(list1, list2);
            }
        }
    }
}