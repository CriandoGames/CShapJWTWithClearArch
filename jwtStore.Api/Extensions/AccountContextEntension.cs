using jwtStore.core.Context.AccountContext.UseCases.Create;
using jwtStore.core.Context.AccountContext.UseCases.Create.Contracts;
using jwtStore.Infra.Context.AccountContext.UseCases.Create;
using MediatR;

namespace jwtStore.Api.Extensions
{
    public static class AccountContextEntension
    {

        public static void AddAccountContext(this WebApplicationBuilder builder)
        {

            #region Create injection for AccountContext
            builder.Services.AddTransient<IRepository, Repository>();
            builder.Services.AddTransient<IService, Service>();
            #endregion


            #region Authenticate injection for AccountContext

            builder.Services.AddTransient<
                jwtStore.core.Context.AccountContext.UseCases.Authenticate.Contracts.IRepository,
                jwtStore.Infra.Context.AccountContext.UseCases.Authenticate.Repository>();

            #endregion
        }



        public static void MapAccountEndPoints(this WebApplication app)
        {
            #region Create

            app.MapPost("api/v1/users", async (Request request, IRequestHandler<Request, Response> handle) =>
            {

                var result = await handle.Handle(request, new CancellationToken());


                return result.IsSuccess ? Results.Created($"api/v1/users/{result?.Data?.Id}", result) : Results.BadRequest(result);

            });


            #endregion

            #region Authenticate

            app.MapPost("api/v1/authenticate", async (
                jwtStore.core.Context.AccountContext.UseCases.Authenticate.Request request,
                IRequestHandler<jwtStore.core.Context.AccountContext.UseCases.Authenticate.Request,
                jwtStore.core.Context.AccountContext.UseCases.Authenticate.Response> handle) =>
            {


            var result = await handle.Handle(request, new CancellationToken());

            if (!result.IsSuccess)
                return Results.Json(result, statusCode: result.StatusCode);

            if (result.Data is null)
                return Results.Json(result, statusCode: result.StatusCode);



            result.Data.Token = JwtExtension.Generate(result.Data);

            return Results.Ok(result);

            });

            #endregion


        }

    }
    
}
