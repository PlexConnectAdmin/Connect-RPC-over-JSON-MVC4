using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace ConsumeWebAPI.Helper
{
  public class Rpc
  {
    private Uri _tokenEndpoint;
    private string _clientId;
    private readonly RestClient _client;
    IRestResponse _response;

    public IRestResponse Response
    {
      get { return _response; }
      set { _response = value; }
    }

    public JToken Execute(RestSharp.Method method, Uri rpcUri)
    {
      JToken jToken = Execute(method, rpcUri, null);
      return jToken;
    }

    public JToken Execute(RestSharp.Method method, Uri rpcUri, object body)
    {
      // 0. Get settings from JSON config file
      JToken bearerToken;
      string exeRuntimeDirectory = System.IO.Path.GetDirectoryName(HttpRuntime.AppDomainAppPath);
      TokenEndpoint tokenEndpoint = JsonConvert.DeserializeObject<TokenEndpoint>(File.ReadAllText(exeRuntimeDirectory + @"\PlexConnectConfig.json"));

      _tokenEndpoint = tokenEndpoint.tokenEndPoint;
      RestClient _client = new RestClient(_tokenEndpoint);
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

      // 3. use bearer token to get some result(s)
      RestClient restClient = new RestClient(rpcUri);
      RestRequest request = new RestRequest(method);

      if (body != null)
      {
        request.AddParameter("application/json", body, ParameterType.RequestBody);

        //request.AddJsonBody(body);
        request.AddHeader("content-type", "application/json");
      }
      else
      {
        request.AddHeader("content-type", "multipart/form-data");
      }

      request.AddHeader("cache-control", "no-cache");
      request.AddHeader("authorization", "Bearer " + bearerToken);
      _response = restClient.Execute(request);


      JToken jsonVal;
      if (string.IsNullOrEmpty(_response.Content))
      {
        ConsumeWebAPI.Models.HttpResponse httpResponse = new Models.HttpResponse();
        httpResponse.Message = _response.StatusDescription;
        httpResponse.StatusCode = _response.StatusCode.ToString();
        string json = JsonConvert.SerializeObject(httpResponse);
        jsonVal = JToken.Parse(json);
      }
      else
      {
        jsonVal = JToken.Parse(_response.Content);
      }

      return jsonVal;
    }
  }
}