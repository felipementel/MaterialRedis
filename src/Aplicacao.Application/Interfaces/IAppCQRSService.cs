using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Application.Interfaces
{
    public interface IAppCQRSService<T, Tid> where T : class
    {
        Task<T> Add(T itemDTO);
        Task<T> Update(T itemDTO);
        Task<bool> Remover(Tid Tid);
    }
}