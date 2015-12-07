using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web;
using System.Linq;
using ConsumeWebAPI.Models;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace ConsumeWebAPI.Helper
{
  public class PartRestClient : IPartRestClient
  {
    private readonly RestClient _client;
    private Uri _tokenEndpoint;
    private string _clientId;

    public PartRestClient()
    {
      _client = new RestClient(_tokenEndpoint);
    }

    public IEnumerable<PartModel> GetSeveralParts()
    {
      // 0. Get settings from JSON config file
      JToken bearerToken;
      string exeRuntimeDirectory = System.IO.Path.GetDirectoryName(HttpRuntime.AppDomainAppPath);
      TokenEndpoint tokenEndpoint = JsonConvert.DeserializeObject<TokenEndpoint>(File.ReadAllText(exeRuntimeDirectory + @"\PlexConnectConfig.json"));

      _tokenEndpoint = tokenEndpoint.tokenEndPoint;
      _clientId = tokenEndpoint.clientId;

      // use the secret with the furthest out expiry. Use the JSON configuration file to manage your rolling secrets 
      List<ClientSecret> SortedClientSecretList = tokenEndpoint.clientSecrets.OrderByDescending(o => o.expiry).ToList();

      // 1. get the bearer token

      // Client app ID. 
      string clientId = _clientId;

      // resource uri for the protected Plex resource you are accessing
      string resourceUri = tokenEndpoint.plexResource.ToString();
      AuthenticationResult authenticationResult;

      // OAuth2 token endpoint for 2-legged, server-to-server application authorization
      string authorityUri = _tokenEndpoint + "?grant_type=client_credentials";

      // Create a new AuthenticationContext passing an Authority URI.
      Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext authContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(authorityUri);

      string clientSecret = SortedClientSecretList[0].clientSecret;
      ClientCredential clientCredential = new ClientCredential(clientId, clientSecret);

      // 2. extract bearer token from AuthenticationResult
      try
      {
        // AAD includes an in memory cache, so this call may only receive a new token from the server if the cached token is expired.
        authenticationResult = authContext.AcquireToken(resourceUri, clientCredential);
        bearerToken = authenticationResult.AccessToken;
      }
      catch (Exception e)
      {
        // handle any error
        throw e;
      }

      // 3. use bearer token to get some parts by status, etc. By default, this limits to 10 results
      //     At least of these controls must be specified: PartNo,Name,PartStatus,GroupKey,ProductTypeKey,CustomerNo,CustomerPart,DepartmentNo,PlannerKey,GradeKey,Master,ExclusiveUseRightsCustomerNo,PartDrawingNumber,PartProductGroupKey

      // other sample call
      // RestClient restClient = new RestClient("https://test.api.plex.com/Engineering/PartList/Parts?partType=Other&PartStatus=Production");
      // todo: put this filter in the UI, or a config setting or something
      RestClient restClient = new RestClient("https://test.api.plex.com/Engineering/PartList/Parts?PartNo=z");
      RestRequest request = new RestRequest(Method.GET);
      request.AddHeader("cache-control", "no-cache");
      request.AddHeader("authorization", "Bearer " + bearerToken);
      request.AddHeader("content-type", "multipart/form-data");
      IRestResponse response = restClient.Execute(request);

      JToken jsonVal = JToken.Parse(response.Content);

      // todo: response has links "next" and "last" (previous) for paging: ...offset=380&limit=10... etc. Implement this paging.

      JArray joResponses = ((dynamic)jsonVal).embedded;

      List<PartModel> Parts = null;
      if (joResponses == null)
      {
        // todo: handle no parts returned
      }
      else
      {
        Parts = joResponses.ToObject<List<PartModel>>();
      }

      // todo: try/catch for error handling on all code

      return Parts;
    }

    public PartModel GetById(int id)
    {
      // 0. Get settings from JSON config file
      JToken bearerToken;
      string exeRuntimeDirectory = System.IO.Path.GetDirectoryName(HttpRuntime.AppDomainAppPath);
      TokenEndpoint tokenEndpoint = JsonConvert.DeserializeObject<TokenEndpoint>(File.ReadAllText(exeRuntimeDirectory + @"\PlexConnectConfig.json"));

      _tokenEndpoint = tokenEndpoint.tokenEndPoint;
      _clientId = tokenEndpoint.clientId;

      // use the secret with the furthest out expiry. Use the JSON configuration file to manage your rolling secrets 
      List<ClientSecret> SortedClientSecretList = tokenEndpoint.clientSecrets.OrderByDescending(o => o.expiry).ToList();

      // 1. get the bearer token

      // Client app ID. 
      string clientId = _clientId;

      // resource uri for the protected Plex resource you are accessing
      string resourceUri = tokenEndpoint.plexResource.ToString();
      AuthenticationResult authenticationResult;

      // OAuth2 token endpoint for 2-legged, server-to-server application authorization
      string authorityUri = _tokenEndpoint + "?grant_type=client_credentials";

      // Create a new AuthenticationContext passing an Authority URI.
      Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext authContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(authorityUri);

      string clientSecret = SortedClientSecretList[0].clientSecret;
      ClientCredential clientCredential = new ClientCredential(clientId, clientSecret);

      // 2. extract bearer token from AuthenticationResult
      try
      {
        // AAD includes an in memory cache, so this call may only receive a new token from the server if the cached token is expired.
        authenticationResult = authContext.AcquireToken(resourceUri, clientCredential);
        bearerToken = authenticationResult.AccessToken;
      }
      catch (Exception e)
      {
        // handle any error
        throw e;
      }

      // 3. use bearer token and HTTP Method DELETE to delete the part. There is no confirmation before execution
      RestClient restClient = new RestClient("https://test.api.plex.com/Engineering/PartList/Parts/" + id.ToString());
      RestRequest request = new RestRequest(Method.GET);
      request.AddHeader("cache-control", "no-cache");
      request.AddHeader("authorization", "Bearer " + bearerToken);
      request.AddHeader("content-type", "multipart/form-data");
      IRestResponse response = restClient.Execute(request);

      PartModel Part = null;
      if (response == null)
      {
        // todo: handle no parts returned
      }
      else
      {
        JToken jsonVal = JToken.Parse(response.Content);
        Part = jsonVal.ToObject<PartModel>();
      }

      return Part;
    }

    public PartModel GetByType(int type)
    {
      var request = new RestRequest("api/part/type/{datatype}", Method.GET)
      {
        RequestFormat = DataFormat.Json
      };

      request.AddParameter("datatype", type, ParameterType.UrlSegment);

      var response = _client.Execute<PartModel>(request);

      return response.Data;
    }

    public PartModel GetByIP(int ip)
    {
      var request = new RestRequest("api/part/ip/{ip}", Method.GET) { RequestFormat = DataFormat.Json };
      request.AddParameter("ip", ip, ParameterType.UrlSegment);

      var response = _client.Execute<PartModel>(request);

      return response.Data;
    }

    public void Add(PartModel part)
    {
      var request = new RestRequest("api/part", Method.POST) { RequestFormat = DataFormat.Json };
      request.AddBody(part);

      var response = _client.Execute<PartModel>(request);

      if (response.StatusCode != HttpStatusCode.Created)
        throw new Exception(response.ErrorMessage);

    }

    public void Update(PartModel part)
    {
      var request = new RestRequest("api/part/{id}", Method.PUT) { RequestFormat = DataFormat.Json };
      request.AddParameter("id", part.PartKey, ParameterType.UrlSegment);
      request.AddBody(part);

      var response = _client.Execute<PartModel>(request);

      if (response.StatusCode == HttpStatusCode.NotFound)
        throw new Exception(response.ErrorMessage);
    }

    public void Delete(int id)
    {
      // 0. Get settings from JSON config file
      JToken bearerToken;
      string exeRuntimeDirectory = System.IO.Path.GetDirectoryName(HttpRuntime.AppDomainAppPath);
      TokenEndpoint tokenEndpoint = JsonConvert.DeserializeObject<TokenEndpoint>(File.ReadAllText(exeRuntimeDirectory + @"\PlexConnectConfig.json"));

      _tokenEndpoint = tokenEndpoint.tokenEndPoint;
      _clientId = tokenEndpoint.clientId;

      // use the secret with the furthest out expiry. Use the JSON configuration file to manage your rolling secrets 
      List<ClientSecret> SortedClientSecretList = tokenEndpoint.clientSecrets.OrderByDescending(o => o.expiry).ToList();

      // 1. get the bearer token

      // Client app ID. 
      string clientId = _clientId;

      // resource uri for the protected Plex resource you are accessing
      string resourceUri = tokenEndpoint.plexResource.ToString();
      AuthenticationResult authenticationResult;

      // OAuth2 token endpoint for 2-legged, server-to-server application authorization
      string authorityUri = _tokenEndpoint + "?grant_type=client_credentials";

      // Create a new AuthenticationContext passing an Authority URI.
      Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext authContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(authorityUri);

      string clientSecret = SortedClientSecretList[0].clientSecret;
      ClientCredential clientCredential = new ClientCredential(clientId, clientSecret);

      // 2. extract bearer token from AuthenticationResult
      try
      {
        // AAD includes an in memory cache, so this call may only receive a new token from the server if the cached token is expired.
        authenticationResult = authContext.AcquireToken(resourceUri, clientCredential);
        bearerToken = authenticationResult.AccessToken;
      }
      catch (Exception e)
      {
        // handle any error
        throw e;
      }

      // 3. use bearer token and HTTP Method DELETE to delete the part. There is no confirmation before execution
      RestClient restClient = new RestClient("https://test.api.plex.com/Engineering/PartList/Parts/" + id.ToString());
      RestRequest request = new RestRequest(Method.DELETE);
      request.AddHeader("cache-control", "no-cache");
      request.AddHeader("authorization", "Bearer " + bearerToken);
      request.AddHeader("content-type", "multipart/form-data");
      IRestResponse response = restClient.Execute(request);

      JToken jsonVal = JToken.Parse(response.Content);

      if (response.StatusCode == HttpStatusCode.NotFound)
      {
        throw new Exception(response.ErrorMessage);
      }
    }
  }
}