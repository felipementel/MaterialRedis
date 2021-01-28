using System.Threading.Tasks;

namespace Aplicacao.Domain.UoW
{
    public interface IUnitOfWork
    {
        public void BeginTransaction();
        public bool Commit();
    }
}