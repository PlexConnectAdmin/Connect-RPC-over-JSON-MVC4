using System;
using System.Runtime.Caching;
using System.Web;
using ConsumeWebAPI.Configuration;
using Newtonsoft.Json.Linq;
using Plex.Restful.Api.Testing.Models;
using RestSharp;

namespace ConsumeWebAPI.Models
{
  internal static class BearerToken
  {

    /// <summary>
    /// Retrieves the bearer token.
    /// </summary>
    /// <returns></returns>
    public static string RetrieveBearerToken()
    {
      // Create a cache instance  
      ObjectCache cache = MemoryCache.Default;

      string accessToken;

      // check cache
      if (cache.Contains("bearerToken"))
      {
        // get an item from the cache
        Token bearerToken = (Token)cache.Get("bearerToken");
        accessToken = bearerToken.AccessToken;
      }
      else
      {
        var uri = ApplicationConfiguration.PlexApiConfiguration.apiEndPointDomain + "oauth2/v1/token?";
        var client = new RestClient(uri);
        RestRequest request = new RestRequest(Method.POST);
        request.AddHeader("content-type", "application/x-www-form-urlencoded");
        request.AddHeader("ocp-apim-subscription-key", ApplicationConfiguration.PlexApiConfiguration.subscriptionKey);
        string urlEncodedBody = string.Format(
          "grant_type=client_credentials&client_id={0}&client_secret={1}&resource={2}",
          ApplicationConfiguration.PlexApiConfiguration.clientId,
          HttpUtility.UrlEncode(ApplicationConfiguration.ClientSecret()),
          HttpUtility.UrlEncode(ApplicationConfiguration.PlexApiConfiguration.plexResource.ToString()));
        request.AddParameter("application/x-www-form-urlencoded", urlEncodedBody, ParameterType.RequestBody);
        IRestResponse restResponse = client.Execute(request);

        // 2. extract bearer token
        Token token = JObject.Parse(((RestResponseBase)restResponse).Content).ToObject<Token>();

        if (token.AccessToken != null)
        {
          accessToken = token.AccessToken;

          CacheItemPolicy cachePolicy = new CacheItemPolicy
          {
            // 75% of the expected longevity
            AbsoluteExpiration =
              new DateTimeOffset(
                DateTime.UtcNow.AddSeconds(token.ExpiresIn * .75))
          };

          cache.Add("bearerToken", token, cachePolicy);
        }
        else
        {
          throw new Exception("Failure to get Bearer Token");
        }
      }

      return accessToken;
    }
  }
}