using Microsoft.Owin.Security.OAuth;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;


namespace Monitor.Api
{
    public class AuthorizationApi : OAuthAuthorizationServerProvider
    {
        private readonly Container _container;
        //public IHandler<DomainNotification> Notifications;

        public AuthorizationApi(Container container)
        {
            _container = container;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //AutenticarUsuarioResponse autenticarUsuarioResponse;

            using (_container.BeginExecutionContextScope())
            {
                //Notifications = DomainEvent.Container.GetInstance<IHandler<DomainNotification>>();
                //var appServiceUsuario = _container.GetInstance<IAppServiceUsuario>();

                //autenticarUsuarioResponse = appServiceUsuario.Autenticar(context.UserName, context.Password);
                //if (autenticarUsuarioResponse == null)
                //{
                //    context.SetError("invalid_grant", Notifications.Notify().FirstOrDefault()?.Value);
                //    return;
                //}
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            //Definindo as Claims
            //identity.AddClaim(new Claim("UsuarioId", autenticarUsuarioResponse.UsuarioId.ToString()));
            //identity.AddClaim(new Claim(ClaimTypes.Name, autenticarUsuarioResponse.Nome));
            //identity.AddClaim(new Claim(ClaimTypes.Email, autenticarUsuarioResponse.Email));
            //identity.AddClaim(new Claim("DataUltimoAcesso", autenticarUsuarioResponse.DataUltimoAcesso.ToString()));
            //identity.AddClaim(new Claim("Ativo", autenticarUsuarioResponse.Ativo.ToString()));

            GenericPrincipal principal = new GenericPrincipal(identity, new string[] { });

            Thread.CurrentPrincipal = principal;

            context.Validated(identity);
        }


    }
}