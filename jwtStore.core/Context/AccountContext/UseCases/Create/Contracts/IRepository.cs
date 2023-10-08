
using jwtStore.core.AccountContext.ValueObjects;
using jwtStore.core.Context.AccountContext.Entities;

namespace jwtStore.core.Context.AccountContext.UseCases.Create.Contracts
{
    public interface IRepository
    {
        Task CreateAsync(User user, CancellationToken cancellationToken);
        Task<bool> UserExistsAsync(Email email, CancellationToken cancellationToken);
    }
}
