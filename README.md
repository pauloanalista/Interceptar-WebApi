# Interceptar-WebApi
Criei este projeto com a finalidade de mostrar como podemos fazer para interceptar os dados de uma WebApi feita em C#


# Conheça a classe LogApiHandler
Classe criada com a finalidade de interceptar todas as requisições enviadas para uma WebApi feita em C#.

Para cada solicitação enviada para WebApi, será logado em um banco de dados todos os dados de request e response.

### Vantagens de armazenar os dados
Com os dados armazenados será possível identificar quantas requisições estamos recebendo por minuto. Além de ter a possíbilidade de monitorar essas requisições em um DashBoard, poderemos também negar requisições caso o sistema atinja um certo limite.



# Dados armazenados
Id
Application
Machine
RequestContentBody
RequestContentType
RequestHeaders
RequestIpAddress
RequestMethod
RequestRouteData
RequestRouteTemplate
RequestTimestamp
RequestUri
ResponseContentBody
ResponseContentType
ResponseHeaders
ResponseStatusCode
ResponseTimestamp
User

# Como implementar?
Basicamente crie uma pasta no projeto da sua API chamada Handlers e adicione a classe LogApiHandler.

Agora adicione a classe ao MessageHandlers de sua API, veja o código abaixo:
```sh
     private void ConfigureWebApi(HttpConfiguration config)
        {
           config.MessageHandlers.Add(new LogApiHandler());
        }
```
   
Não esqueça também de setar sua string de conexão com o banco de dados.
 ```sh
 <connectionStrings>
    <clear />
    <add name="MonitorConnectionString" providerName="System.Data.SqlClient" connectionString="Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Monitor;Data Source=.\sqlexpress" />
  </connectionStrings>
```

# OBS:
Lembrando que este projeto não segue nenhum padrão de arquitetura, logo aconselho tirar o armazenamento de dentro da classe e jogar dentro de um repositório.
