using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Domain.Interfaces.Repositories
{
    public interface ISQLRepository<T, Tid>
    {
        //Repositório é APENAS um CRUD
        Task<T> Create(T entity);
        Task<IEnumerable<T>> ReadAll();
        Task<T> ReadById(Tid id);
        Task<T> Update(T entity);
        Task Delete(Tid id);
    }
}
