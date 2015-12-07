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

      // resource uri
      string resourceUri = tokenEndpoint.plexResource.ToString();
      AuthenticationResult ar;

      // OAuth2 token endpoint for 2-legged, server-to-server application authorization
      string authorityUri = _tokenEndpoint + "?grant_type=client_credentials";

      // Create a new AuthenticationContext passing an Authority URI.
      Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext authContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(authorityUri);

      string clientSecret = SortedClientSecretList[0].clientSecret;
      ClientCredential clientCredential = new ClientCredential(clientId, clientSecret);

      // 2. extract bearer token from JSON response
      try
      {
        // AAD includes an in memory cache, so this call may only receive a new token from the server if the cached token is expired.
        ar = authContext.AcquireToken(resourceUri, clientCredential);

        JObject payload = new JObject();
        bearerToken = ar.AccessToken;
      }
      catch (Exception e)
      {
        // handle any error
        throw e;
      }

      // 3. use bearer token to get some parts by status, etc. By default, this limits to 10 results
      var restClient = new RestClient(_tokenEndpoint);
      var request = new RestRequest(Method.POST);
      restClient = new RestClient("http://dev.api.plex.com/Engineering/PartList/Parts?partType=Other&PartStatus=Production");
      request = new RestRequest(Method.GET);
      request.AddHeader("cache-control", "no-cache");
      request.AddHeader("authorization", "Bearer " + bearerToken);
      request.AddHeader("content-type", "multipart/form-data");
      IRestResponse response = restClient.Execute(request);

      JToken jsonVal = JToken.Parse(response.Content);
      JObject joResponse = JObject.Parse(response.Content);

      JArray joResponses = ((dynamic)jsonVal).embedded;

      List<PartModel> Users = joResponses.ToObject<List<PartModel>>();

      return Users;

      // todo: try/catch for error handling
    }

    public PartModel GetById(int id)
    {
      var request = new RestRequest("api/part/{id}", Method.GET) { RequestFormat = DataFormat.Json };

      request.AddParameter("id", id, ParameterType.UrlSegment);

      var response = _client.Execute<PartModel>(request);

      if (response.Data == null)
        throw new Exception(response.ErrorMessage);

      return response.Data;
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
      var request = new RestRequest("api/part/{id}", Method.DELETE);
      request.AddParameter("id", id, ParameterType.UrlSegment);

      var response = _client.Execute<PartModel>(request);

      if (response.StatusCode == HttpStatusCode.NotFound)
      {
        throw new Exception(response.ErrorMessage);
      }
    }
  }
}