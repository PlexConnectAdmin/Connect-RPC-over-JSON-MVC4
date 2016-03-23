using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ConsumeWebAPI.Configuration;
using ConsumeWebAPI.Models;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsumeWebAPI.Helper
{
  /// <summary>
  /// class for Part RestClient 
  /// </summary>
  internal sealed class PartRestClient : IPartRestClient
  {
    /// <summary>
    /// Gets the several parts.
    /// </summary>
    /// <returns>IEnumerable PartModel</returns>
    public IEnumerable<PartModel> GetSeveralParts()
    {
      Uri resource = new Uri(string.Format("{0}test/PartSetup/v1/PartList/Parts?PartNo=1&limit=50", ApplicationConfiguration.PlexApiConfiguration.apiEndPointDomain));
      
      // used in local testing at Plex
       // resource = new Uri(string.Format("http://local.api.plex.com/engineering/PartList/Parts?PartNo=1&limit=50", ApplicationConfiguration.PlexApiConfiguration.apiEndPointDomain));
      
      Task<JToken> jsonValTask = Rpc.Execute(Method.GET, resource);

      // another possible check: if (jsonValTask.Status == TaskStatus.Faulted)
      if (jsonValTask.Exception == null)
      {
        JToken jsonVal = jsonValTask.Result;

        // todo: HAL-formatted response has links "next" and "last" (previous) for paging: ...offset=380&limit=10... etc. Implement this paging.
        JArray array = ((dynamic)jsonVal).embedded;

        List<PartModel> parts = null;
        if (array == null)
        {
          // todo: handle no parts returned
        }
        else
        {
          parts = array.ToObject<List<PartModel>>();
        }

        // todo: try/catch for error handling on all code
        return parts;
      }
      else
      {
        // some error occurred
        // first, see if no records were found
        if (jsonValTask.Exception.InnerExceptions.Count == 1)
        {
          if (jsonValTask.Exception.InnerExceptions[0].Message == "{ \"statusCode\": 404, \"message\": \"Resource not found\" }")
          {
            // no records found
            return null;
          }
          else
          {
            throw jsonValTask.Exception;
          }
        }

        return null;
      }
    }

    /// <summary>
    /// Gets the by identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>the model</returns>
    public PartModel GetById(int id)
    {
      Rpc rpc = new Rpc();

      Uri resource = new Uri(string.Format("{0}test/PartSetup/v1/PartList/Parts/" + id.ToString(), ApplicationConfiguration.PlexApiConfiguration.apiEndPointDomain)); 
      Task<JToken> jsonValTask = Rpc.Execute(Method.GET, resource);
      JToken jsonVal = jsonValTask.Result;

      PartModel part = null;
      if (jsonVal == null)
      {
        // todo: handle no parts returned
      }
      else
      {
        part = jsonVal.ToObject<PartModel>();
      }

      return part;
    }

    /// <summary>
    /// Adds the specified part.
    /// </summary>
    /// <param name="part">The part.</param>
    public void Add(PartAddModel part)
    {
      System.Exception exception = new Exception("Not Implemented");
      throw exception;
    }

    /// <summary>
    /// Copies the specified part copy model.
    /// </summary>
    /// <param name="partCopyModel">The part copy model.</param>
    /// <param name="partKey">The part key.</param>
    public void Copy(PartCopyModel partCopyModel, int partKey)
    {
      System.Exception exception = new Exception("Not Implemented");
      throw exception;
    }

    /// <summary>
    /// Deletes the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    public void Delete(int id)
    {
      System.Exception exception = new Exception("Not Implemented");
      throw exception;
    }
  }
}