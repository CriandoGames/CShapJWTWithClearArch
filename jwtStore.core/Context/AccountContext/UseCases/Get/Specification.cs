

using Flunt.Notifications;
using Flunt.Validations;

namespace jwtStore.core.Context.AccountContext.UseCases.Get
{
    public static class Specification
    {
        public static Contract<Notification> Ensure(Request request) => new Contract<Notification>()
            .Requires().IsEmail(request.Email, "Email", "E-mail inválido");

    }
}
