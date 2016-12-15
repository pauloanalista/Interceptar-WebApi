using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Monitor.Api.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        //[HttpPost]
        [Route("v1/EnviarGet")]
        [HttpGet]
        public Task<HttpResponseMessage> EnviarGet(string parametro = "Paulo")
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var result = new { Message = "Api funcionando!", IsSuccess = true };

                response = Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPost]
        [Route("v1/EnviarPost")]
        public Task<HttpResponseMessage> EnviarPost(PessoaRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                Task.Delay(10000);

                string mensagem = "Não pode voltar, você é de menor";
                if (request.Idade >=16)
                {
                    mensagem = "Pode votar!";
                }
                
                var result = new { Message = mensagem, IsSuccess = true, NomeResponse= request.Nome,  PropriedadeQueEuQuiser="Valor da propriedade que eu quiser" };

                response = Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPut]
        [Route("v1/EnviarPut")]
        public Task<HttpResponseMessage> EnviarPut(PessoaRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                Task.Delay(10000);

                string mensagem = "Não pode voltar, você é de menor";
                if (request.Idade >= 16)
                {
                    mensagem = "Pode votar!";
                }

                var result = new { Message = mensagem, IsSuccess = true, NomeResponse = request.Nome, PropriedadeQueEuQuiser = "Valor da propriedade que eu quiser" };

                response = Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }
    }

    public class PessoaRequest
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public PessoaRequest()
        {

        }

    }
}

