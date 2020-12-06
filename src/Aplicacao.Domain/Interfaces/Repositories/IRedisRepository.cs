using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Domain.Interfaces.Repositories
{
    public interface IRedisRepository<T, Tid>
    {
        //Repositório é APENAS um CRUD
        Task Set(T cliente);

        Task Setm(IEnumerable<T> clientes);

        Task<IEnumerable<T>> Getm();

        Task<T> Get(Tid key);

        Task Remove(Tid key);
    }
}