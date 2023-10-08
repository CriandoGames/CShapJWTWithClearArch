using MediatR;

namespace jwtStore.core.Context.AccountContext.UseCases.Authenticate
{
    public record Request(string Email, string Password) : IRequest<Response>;
}
