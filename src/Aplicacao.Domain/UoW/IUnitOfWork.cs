using System.Threading.Tasks;

namespace Aplicacao.Domain.UoW
{
    public interface IUnitOfWork
    {
        public Task BeginTransaction();
        public Task<bool> Commit();
    }
}
