using Dapper;
using Microsoft.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Routing;

namespace Monitor.Api.Handlers
{
    //artigo http://blog.alisuleymantopuz.com/2015/12/30/web-api-request-and-response-log-with-delegating-handler/
    public class LogApiHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //Crio um ID para a requisição
            string idRequisicao = Guid.NewGuid().ToString();

            //Obtenho os dados da requisição feita para API
            var logApi = ObterDadosInterceptadosDaRequisicaoDaApi(request, idRequisicao);

            if (request.Content != null)
            {
                await request.Content.ReadAsStringAsync()
                    .ContinueWith(task => logApi.RequestContentBody = task.Result, cancellationToken);
            }

            return await base.SendAsync(request, cancellationToken).ContinueWith(task =>
            {
                //Obtenho os dados de resposta da api.
                var response = task.Result;

                logApi.ResponseStatusCode = (int)response.StatusCode;
                logApi.ResponseTimestamp = DateTime.Now;

                if (response.Content != null)
                {
                    logApi.ResponseContentBody = response.Content.ReadAsStringAsync().Result;
                    logApi.ResponseContentType = response.Content.Headers.ContentType.MediaType;
                    logApi.ResponseHeaders = SetaDadosDoHeaderParaUmDicionario(response.Content.Headers);
                }

                //Salvo os dados da Api no banco de dados
                SalvarLogDaApiNoBancoDeDados(logApi);

                return response;
            }, cancellationToken);
        }

        private void SalvarLogDaApiNoBancoDeDados(LogApi logApi)
        {
            
            //string content = JsonConvert.SerializeObject(apiLogEntry);

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MonitorConnectionString"].ConnectionString))
            {
                connection.Execute(
                    @"INSERT INTO LogApi (Id, Application, Machine, RequestContentBody, RequestContentType, RequestHeaders, RequestIpAddress, RequestMethod, RequestRouteData, RequestRouteTemplate, RequestTimestamp, RequestUri, ResponseContentBody, ResponseContentType, ResponseHeaders, ResponseStatusCode, ResponseTimestamp, [User]) 
                    VALUES (@Id, @Application, @Machine, @RequestContentBody, @RequestContentType, @RequestHeaders, @RequestIpAddress, @RequestMethod, @RequestRouteData, @RequestRouteTemplate, @RequestTimestamp, @RequestUri, @ResponseContentBody, @ResponseContentType, @ResponseHeaders, @ResponseStatusCode, @ResponseTimestamp, @User)",
                    new
                    {
                        @Id = logApi.Id,
                        @Application = logApi.Application,
                        @Machine = logApi.Machine,
                        @RequestContentBody = logApi.RequestContentBody,
                        @RequestContentType = logApi.RequestContentType,
                        @RequestHeaders = JsonConvert.SerializeObject(logApi.RequestHeaders),
                        @RequestIpAddress = logApi.RequestIpAddress,
                        @RequestMethod = logApi.RequestMethod,
                        @RequestRouteData = logApi.RequestRouteData?.ToString(),
                        @RequestRouteTemplate = logApi.RequestRouteTemplate,
                        @RequestTimestamp = logApi.RequestTimestamp,
                        @RequestUri = logApi.RequestUri,
                        @ResponseContentBody = logApi.ResponseContentBody,
                        @ResponseContentType = logApi.ResponseContentType,
                        @ResponseHeaders = JsonConvert.SerializeObject(logApi.ResponseHeaders.ToList()),
                        @ResponseStatusCode = logApi.ResponseStatusCode,
                        @ResponseTimestamp = logApi.ResponseTimestamp,
                        @User = logApi.User
                    }
                    );
            }
        }

        private LogApi ObterDadosInterceptadosDaRequisicaoDaApi(HttpRequestMessage request, string id)
        {
            //var context = ((HttpContextBase)request.Properties["MS_HttpContext"]);

            var context = ((OwinContext)request.Properties["MS_OwinContext"]);
            var routeData = request.GetRouteData();

            var logApi = new LogApi();

            //Obtenho nome do projeto
            logApi.Application = Assembly.GetExecutingAssembly().FullName;

            if (context.Request.User != null)
            {
                logApi.User = context.Request.User.Identity.Name;
            }

            logApi.Machine = Environment.MachineName;

            logApi.Id = id;

            if (context.Request != null)
            {
                logApi.RequestContentType = context.Request.ContentType;

                if (routeData != null)
                {
                    logApi.RequestRouteTemplate = routeData.Route.RouteTemplate;
                    logApi.RequestRouteData = routeData;
                }

                logApi.RequestIpAddress = context.Request.RemoteIpAddress;
            }
            if (request.Method != null)
            {
                logApi.RequestMethod = request.Method.Method;
            }
            if (request.Headers != null)
            {
                logApi.RequestHeaders = SetaDadosDoHeaderParaUmDicionario(request.Headers);
            }

            logApi.RequestTimestamp = DateTime.Now;

            if (request.RequestUri != null)
            {
                logApi.RequestUri = request.RequestUri.AbsoluteUri;
            }

            return logApi;
        }

        private Dictionary<string, string> SetaDadosDoHeaderParaUmDicionario(HttpHeaders headers)
        {
            var dicionario = new Dictionary<string, string>();

            foreach (var item in headers.ToList())
            {
                if (item.Value == null) continue;

                var header = item.Value.Aggregate(String.Empty, (current, value) => current + (value + " "));

                header = header.TrimEnd(" ".ToCharArray());

                dicionario.Add(item.Key, header);
            }
            return dicionario;
        }

    }

    public class LogApi
    {

        public LogApi()
        {

        }

        public string Id { get; internal set; }
        public string Application { get; internal set; }
        public string Machine { get; internal set; }
        public string RequestContentBody { get; internal set; }
        public string RequestContentType { get; internal set; }
        public Dictionary<string, string> RequestHeaders { get; internal set; }
        public string RequestIpAddress { get; internal set; }
        public string RequestMethod { get; internal set; }
        public IHttpRouteData RequestRouteData { get; internal set; }
        public string RequestRouteTemplate { get; internal set; }
        public DateTime RequestTimestamp { get; internal set; }
        public string RequestUri { get; internal set; }
        public string ResponseContentBody { get; internal set; }
        public string ResponseContentType { get; internal set; }
        public Dictionary<string, string> ResponseHeaders { get; internal set; }
        public int ResponseStatusCode { get; internal set; }
        public DateTime ResponseTimestamp { get; internal set; }
        public string User { get; internal set; }
    }
}