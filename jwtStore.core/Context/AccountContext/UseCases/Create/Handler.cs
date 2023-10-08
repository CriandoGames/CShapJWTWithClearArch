using jwtStore.core.AccountContext.ValueObjects;
using jwtStore.core.Context.AccountContext.Entities;
using jwtStore.core.Context.AccountContext.UseCases.Create.Contracts;
using jwtStore.core.Context.AccountContext.ValueObjects;
using MediatR;

namespace jwtStore.core.Context.AccountContext.UseCases.Create
{

    // O handle é responsavel por receber a requisição e fazer a validação e chamar o repositorio para salvar os dados.

    //Repository -> tudo que for relacionado a banco de dados
    //Service -> Tudo que for relacionado a Serviços externos
    //Handler -> Tudo que for relacionado a regra de negocio
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IRepository _repository;
        private readonly IService _service;

        public Handler(IRepository repository, IService service)
        {
            _repository = repository;
            _service = service;
        }


        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {

            // 01 - Validar a requisição
            // 02   Gerar os Objetos e entidades
            // 03 - Verificar se o usuario ja existe
            // 04 - Persistir os dados
            // 05 - Enviar E-mail de ativação


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

            #region 02 Gerar os Objetos e entidades

            Email email;
            Password password;
            User user;

            try
            {
                email = new(request.Email);
                password = new(request.Password);

                user = new(request.Name, email, password);
            }
            catch (Exception ex)
            {
                return new Response(ex.Message, 400);
            }


            #endregion

            #region 03 Verificar se o usuario ja existe
            try
            {
                var exist = await _repository.UserExistsAsync(email, cancellationToken);
                if (exist)
                    return new Response("Esté E-mail já está em uso", 400);

            }
            catch (Exception ex)
            {


                return new Response("Não foi possivel verificar se o usuario existe" + ex.Message, 500);
            }
            #endregion

            #region 04 Persistir os dados

            try
            {
                await _repository.CreateAsync(user, cancellationToken);
            }
            catch
            {
                return new Response("Não foi possivel criar o usuario", 500);
            }

            #endregion

            #region 05 Enviar E-mail de ativação

            try
            {
                await _service.SendEmailAsync(user, cancellationToken);
            }
            catch
            {
              // Do nothing
            }

            #endregion



            return new Response("Usuario criado com sucesso", new ResponseData(user.Id, user.Name, user.Email.Value));
        }

    }
}
