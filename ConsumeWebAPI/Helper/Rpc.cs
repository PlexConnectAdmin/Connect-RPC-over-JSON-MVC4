using System;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace ConsumeWebAPI.Helper
{
  public class Rpc
  {
    private static Uri tokenEndpoint;
    private static string clientId;
    static IRestResponse response;

    public static IRestResponse Response
    {
      get { return response; }
      set { response = value; }
    }

    public static async Task<JToken> Execute(RestSharp.Method method, Uri rpcUri)
    {
      JToken jToken  = await Execute(method, rpcUri, null);
      return jToken;
    }

    public static async Task<JToken> Execute(RestSharp.Method method, Uri rpcUri, object body)
    {
      // 0. Get settings from JSON config file
      JToken bearerToken;
      string exeRuntimeDirectory = System.IO.Path.GetDirectoryName(HttpRuntime.AppDomainAppPath);

      // Looking up the token endpoint and app registration content in App_Data, which is by default not viewable by browsing the web site, see http://stackoverflow.com/questions/528858/what-is-the-app-data-folder-used-for-in-visual-studio
      // Regardless of the location you use, make sure it is similarly secure.
      PlexApiConfiguration plexApiConfiguration = JsonConvert.DeserializeObject<PlexApiConfiguration>(File.ReadAllText(exeRuntimeDirectory + @"\App_Data\PlexConnectConfig.json"));

      tokenEndpoint = plexApiConfiguration.tokenEndPoint;
      //RestClient restClient = new RestClient(tokenEndpoint);
      Rpc.clientId = plexApiConfiguration.clientId;

      // use the secret with the furthest out expiry. Use the JSON configuration file to manage your rolling secrets 
      List<ClientSecret> SortedClientSecretList = plexApiConfiguration.clientSecrets.OrderByDescending(o => o.expiry).ToList();

      // 1. get the bearer token
      RestClient restClient = new RestClient(rpcUri);
      RestRequest request = new RestRequest(method);

      if (body != null)
      {
        request.AddParameter("application/json", body, ParameterType.RequestBody);

        //request.AddJsonBody(body);
        request.AddHeader("content-type", "application/x-www-form-urlencoded");
      }
      else
      {
        request.AddHeader("content-type", "multipart/form-data");
      }

      request.AddHeader("cache-control", "no-cache");
      request.AddHeader("Ocp-Apim-Subscription-Key", plexApiConfiguration.subscriptionKey);
      Rpc.response = restClient.Execute(request);

      if ((Rpc.response.ErrorException==null)&&(string.IsNullOrEmpty(Rpc.response.ErrorMessage)))
      {

      // 1. get the bearer token
      // todo: check tokencache
        var uri = plexApiConfiguration.apiEndPointDomain + "oauth2/v1/token?";// + queryString;
        //var client = new RestClient("https://plexconnect.azure-api.net/oauth2/v1/token");
        var client = new RestClient(uri);
        request = new RestRequest(Method.POST);
        request.AddHeader("content-type", "application/x-www-form-urlencoded");
        request.AddHeader("ocp-apim-subscription-key", "e69d07d1085149c081b2ff2173c4f1a5");
        request.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials&client_id=995f6a9d-7d9d-4613-b907-eab6c6a77421&client_secret=ooFTOFHfmg%2BBlqQ3FD%2FXkWMYNgwvky%2FLCYYAuizbeUs%3D&resource=http%3A%2F%2Fapi.plex.com%2F", ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        //  // todo: assign bearerToken
          bearerToken = string.Empty;


      string clientId = Rpc.clientId;

      // resource uri for the protected Plex resource you are accessing, e.g. "http://api.plex.com/"
      string resourceUri = plexApiConfiguration.plexResource.ToString();

      //var client = new HttpClient();

      //// Request headers
      //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
      //client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", plexApiConfiguration.subscriptionKey);


      //HttpResponseMessage response;

      // //Request body
      //byte[] byteData = Encoding.UTF8.GetBytes("{body}");

      //using (var content = new ByteArrayContent(byteData))
      //{
      //  content.Headers.ContentType = new MediaTypeHeaderValue("application/json");//"< your content type, i.e. application/json >");
      //  response = await client.PostAsync(uri, content);
      //  // todo: assign bearerToken
      //  bearerToken = string.Empty;
      //}

      //// OAuth2 token endpoint for 2-legged, server-to-server application authorization
      //string authorityUri = tokenEndpoint + "?grant_type=client_credentials";

      //// Create a new AuthenticationContext passing an Authority URI.
      //Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext authContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(authorityUri);

      //string clientSecret = SortedClientSecretList[0].clientSecret;
      //ClientCredential clientCredential = new ClientCredential(clientId, clientSecret);

      /*********************************************************************/
       //2. extract bearer token from AuthenticationResult
      /*********************************************************************/

      //try
      //{
      //  // AAD includes an in memory cache, so this call may only receive a new token from the server if the cached token is expired.
      //  AuthenticationResult authenticationResult = authContext.AcquireToken(resourceUri, clientCredential);
      //  bearerToken = authenticationResult.AccessToken;
      //}
      //catch (Exception e)
      //{
      //  // handle any error
      //  throw e;
      //}

      /*********************************************************************/
       //3. use bearer token to get some result(s)
      /*********************************************************************/

      //RestClient restClient = new RestClient(rpcUri);
      //RestRequest request = new RestRequest(method);

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
      Rpc.response = restClient.Execute(request);

      }
      // if ((Rpc.response.ErrorException==null)&&(string.IsNullOrEmpty(Rpc.response.ErrorMessage)))
      else
      {
        Exception exception;
        if (Rpc.response.ErrorException == null)
        {
          exception = new Exception(Rpc.response.ErrorMessage);
        }
        else
        {
          exception = Rpc.response.ErrorException;
        }

        throw exception;
      }

      JToken jsonVal;
      if (string.IsNullOrEmpty(Rpc.response.Content))
      {
        ConsumeWebAPI.Models.HttpResponse httpResponse = new Models.HttpResponse();
        httpResponse.Message = Rpc.response.StatusDescription;
        httpResponse.StatusCode = Rpc.response.StatusCode.ToString();
        string json = JsonConvert.SerializeObject(httpResponse);
        jsonVal = JToken.Parse(json);
      }
      else
      {
        jsonVal = JToken.Parse(Rpc.response.Content);
      }

  return jsonVal;
    }

  }
}