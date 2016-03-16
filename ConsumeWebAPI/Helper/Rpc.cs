using System;
using System.Threading.Tasks;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using ConsumeWebAPI.Configuration;
using Plex.Restful.Api.Testing.Models;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ConsumeWebAPI.Helper
{
  public class Rpc
  {
    static IRestResponse response;

    public static IRestResponse Response
    {
      get { return response; }
      set { response = value; }
    }

    public static async Task<JToken> Execute(RestSharp.Method method, Uri rpcUri)
    {
      JToken jToken = await Execute(method, rpcUri, null);
      return jToken;
    }

    public static async Task<JToken> Execute(RestSharp.Method method, Uri rpcUri, object body)
    {
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
      request.AddHeader("Ocp-Apim-Subscription-Key", ApplicationConfiguration.PlexApiConfiguration.subscriptionKey);
      Rpc.response = restClient.Execute(request);

      if ((Rpc.response.ErrorException == null) && (string.IsNullOrEmpty(Rpc.response.ErrorMessage)))
      {

        // 1. get the bearer token
        // todo: check tokencache
        var uri = ApplicationConfiguration.PlexApiConfiguration.apiEndPointDomain + "oauth2/v1/token?";
        var client = new RestClient(uri);
        request = new RestRequest(Method.POST);
        request.AddHeader("content-type", "application/x-www-form-urlencoded");
        request.AddHeader("ocp-apim-subscription-key", ApplicationConfiguration.PlexApiConfiguration.subscriptionKey);
        string urlEncodedBody = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&resource={2}", ApplicationConfiguration.PlexApiConfiguration.clientId, HttpUtility.UrlEncode(ApplicationConfiguration.ClientSecret()), HttpUtility.UrlEncode(ApplicationConfiguration.PlexApiConfiguration.plexResource.ToString()));
        request.AddParameter("application/x-www-form-urlencoded", urlEncodedBody, ParameterType.RequestBody);
        IRestResponse restResponse = client.Execute(request);

        //2. extract bearer token
        Token token = JObject.Parse(((RestResponseBase)restResponse).Content).ToObject<Token>();
        JToken bearerToken;
        if (token.AccessToken != null)
        {
          bearerToken = token.AccessToken;
        }
        else
        {
          throw new Exception("Failure to get Bearer Token");
        }

        /*********************************************************************/
        //3. use bearer token to get some result(s)
        /*********************************************************************/

        restClient = new RestClient(rpcUri);
        request = new RestRequest(method);

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
        request.AddHeader("ocp-apim-subscription-key", ApplicationConfiguration.PlexApiConfiguration.subscriptionKey);
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

      // HttpStatusCode.NotFound (404) is used for no records found.
      if ((Rpc.response.StatusCode == System.Net.HttpStatusCode.OK) && (Rpc.response.StatusCode != System.Net.HttpStatusCode.NotFound))
      {
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
      }
      else
      {
        Exception exception;
        if (Rpc.response.ErrorException == null)
        {
          exception = string.IsNullOrEmpty(Rpc.response.ErrorMessage) ? new Exception(Rpc.response.Content) : new Exception(Rpc.response.ErrorMessage);
        }
        else
        {
          exception = Rpc.response.ErrorException;
        }

        throw exception;
      }

      return jsonVal;
    }
  }
}