using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Domain.Interfaces.Services
{
    public interface IService<TEntity, Tid> : IDisposable where TEntity : class
    {
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<bool> Delete(Tid tid);
        Task<TEntity> Get(Tid tid);
        Task<IEnumerable<TEntity>> GetAll();
    }
}