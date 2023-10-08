
using jwtStore.core.Context.AccountContext.Entities;

namespace jwtStore.core.Context.AccountContext.UseCases.Authenticate.Contracts
{
    public interface IRepository
    {
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
