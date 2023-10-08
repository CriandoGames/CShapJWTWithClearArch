
using MediatR;

namespace jwtStore.core.Context.AccountContext.UseCases.Create
{
    public record Request(string Name, string Email, string Password) : IRequest<Response>;
    
}
