using jwtStore.core.Context.AccountContext.Entities;
using jwtStore.core.Context.AccountContext.UseCases.Get.Contracts;
using MediatR;

namespace jwtStore.core.Context.AccountContext.UseCases.Get
{
    public class Handler : IRequestHandler<Request, Response>
    {

        private readonly IRepository _repository;

        public Handler(IRepository repository) => _repository = repository;

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {

            #region 01 Validar a requisição

            try
            {
                var response = Specification.Ensure(request);

                if (!response.IsValid)
                    return new Response("Erro ao validar a requisição", 400, response.Notifications);

            }
            catch
            {
                return new Response("Não foi possivel validar sua requisição", 500);
            }

            #endregion


            #region 02 Verifica se o usuario existe
            User user;

            try { 

            var response = await _repository.GetByEmailAsync(request.Email);

               if (response is null)
                 return new Response("Usuário não encontrado", 404);

               user = response;

            }catch {
            
             return new Response("Não foi possivel buscar o usuário", 500);

            }

            #endregion


            #region 03 retorna o usuario

            ResponseData data = new(user.Id, user.Email, user.Name);

            return new Response("Usuário encontrado", data);

            #endregion


        }
    }
}
