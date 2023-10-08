

using jwtStore.core.Context.AccountContext.Entities;

namespace jwtStore.core.Context.AccountContext.UseCases.Create.Contracts
{
    public interface IService
    {
        Task SendEmailAsync(User email, CancellationToken cancellationToken);
    }
}
