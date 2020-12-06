using Aplicacao.Domain.Model.Access;

namespace Aplicacao.Application.Interfaces.Access
{
    public interface ILoginApplication
    {
        Token VerifyAccess(User user);
    }
}