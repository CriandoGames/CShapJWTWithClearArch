using jwtStore.core.Context.AccountContext.Entities;
using jwtStore.core.Context.AccountContext.UseCases.Authenticate.Contracts;
using MediatR;


namespace jwtStore.core.Context.AccountContext.UseCases.Authenticate
{
    public class Handler : IRequestHandler<Request, Response>
    {

        private readonly IRepository repository;

        public Handler(IRepository repository) => this.repository = repository;


        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            // 01 valida a request

            #region 01. Valida a request

            try
            {
                var result = Specification.Ensure(request);

                if (!result.IsValid)
                    return new Response("Requisição invalida", 400, result.Notifications);
            }
            catch
            {
                return new Response("Não foi possivel validar sua requisição", 500);
            }

            #endregion

            #region 02. Busca o usuário

            User? user;

            try
            {

                user = await repository.GetUserByEmailAsync(request.Email, cancellationToken);

                if (user is null)
                    return new Response("Usuário não encontrado", 404);

            }
            catch
            {
                return new Response("Não foi possivel buscar o usuário", 500);

            }

            #endregion

            #region 03. Valida a senha


            if (!user.Password.Challenge(request.Password))
                return new Response("Senha inválida", 400);


            #endregion

            #region 04. verificar se user está ativo

            try
            {
                if (!user.Email.Verification.IsActive)
                    return new Response("Usuário inativo", 400);
            }
            catch
            {
                return new Response("Não foi possivel verificar se o usuário está ativo", 500);
            }

            #endregion

            #region 05. retorna os dados do usuário

            try
            {
                var data = new ResponseData
                {

                    Id = user.Id.ToString(),
                    Name = user.Name,
                    Email = user.Email.Value,
                    Token = "",
                    Roles = user.Roles.Select(x => x.Name).ToArray()

                };


                return new Response("Usuário autenticado com sucesso", data);


            }
            catch
            {

                return new Response("Não foi possivel retornar os dados do usuário", 500);

            }


            #endregion
        }
    }
}
