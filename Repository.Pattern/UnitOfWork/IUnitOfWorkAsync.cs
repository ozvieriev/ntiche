using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Pattern.UnitOfWork
{
    public interface IUnitOfWorkAsync : IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class;
        int ExecuteSqlCommand(string query, params object[] parameters);
        Task<List<T>> ExecuteStoredProcedureAsync<T>(string query, params object[] parameters);
        Task<Tuple<List<T1>, List<T2>>> ExecuteStoredProcedureAsync<T1, T2>(string query, params object[] parameters);
    }
}
