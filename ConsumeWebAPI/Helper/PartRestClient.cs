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

    public IEnumerable<PartModel> GetSeveral()
    {
      // 0. Get settings from JSON config file
      // below from http://stackoverflow.com/questions/10951599/getting-current-directory-in-net-web-application, better than System.Reflection.Assembly.GetExecutingAssembly().Location
      // todo: optimize this with caching
      string exeRuntimeDirectory = System.IO.Path.GetDirectoryName(HttpRuntime.AppDomainAppPath);
      TokenEndpoint tokenEndpoint = JsonConvert.DeserializeObject<TokenEndpoint>(File.ReadAllText(exeRuntimeDirectory + @"\PlexConnectConfig.json"));

      _tokenEndpoint = tokenEndpoint.tokenEndPoint;
      _clientId = tokenEndpoint.clientId;

      // use the secret with the furthest out expiry. Use the JSON configuration file to manage your rolling secrets 
      List<ClientSecret> SortedClientSecretList = tokenEndpoint.clientSecrets.OrderByDescending(o => o.expiry).ToList();

      // 1. get the bearer token
      var restClient = new RestClient(_tokenEndpoint);
      var request = new RestRequest(Method.POST);
      request.AddHeader("content-type", "application/x-www-form-urlencoded");
      request.AddHeader("cache-control", "no-cache");
      string clientId = _clientId;
      string clientSecret = SortedClientSecretList[0].clientSecret;
      request.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials&resource=" + tokenEndpoint.plexResource.ToString() + "&client_secret=" + System.Uri.EscapeDataString(clientSecret) + "&client_id=" + clientId, ParameterType.RequestBody);
      IRestResponse response = restClient.Execute(request);

      // 2. extract bearer token from JSON response
      JObject joResponse = JObject.Parse(response.Content);
      JToken jtBearerToken = joResponse["access_token"];
      string bearerToken = jtBearerToken.ToString();

      // 3. use bearer token to get some parts by status, etc. By default, this limits to 10 results
      restClient = new RestClient("http://dev.api.plex.com/Engineering/PartList/Parts?partType=Other&PartStatus=Production");
      request = new RestRequest(Method.GET);
      request.AddHeader("cache-control", "no-cache");
      request.AddHeader("authorization", "Bearer " + bearerToken);
      request.AddHeader("content-type", "multipart/form-data");
      response = restClient.Execute(request);

      JToken jsonVal = JToken.Parse(response.Content);
      joResponse = JObject.Parse(response.Content);

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
        throw new Exception(response.ErrorMessage);
    }
  }
}