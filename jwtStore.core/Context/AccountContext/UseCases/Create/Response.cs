using Flunt.Notifications;

namespace jwtStore.core.Context.AccountContext.UseCases.Create
{
    public class Response : SharedContext.UseCases.Response
    {

        protected Response() { }

        public Response(string Message, int status, IEnumerable<Notification>? notifications = null)
        {
            this.Message = Message;
            this.StatusCode = status;
            this.Notifications = notifications;
        }

        public Response(string message, ResponseData data)
        {

            Message = message;
            StatusCode = 201;
            this.Notifications = null;
            this.Data = data;



        }

        public ResponseData? Data { get; set; }

    }
}

// não precisaria ter essa quantidade de campos para uma criação de conta , porem vou deixa como exemplo como seria um retorno mais complexo.
public record ResponseData(Guid Id, string Name, string Email);


