using MediatR;

namespace jwtStore.core.Context.AccountContext.UseCases.Get
{
    public record Request (string Email) : IRequest<Response>;
    
}
