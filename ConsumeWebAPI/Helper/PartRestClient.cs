using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web;
using System.Linq;
using ConsumeWebAPI.Models;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace ConsumeWebAPI.Helper
{
  public class PartRestClient : IPartRestClient
  {

    public PartRestClient()
    {
    }

    public IEnumerable<PartModel> GetSeveralParts()
    {
      Rpc rpc = new Rpc();
      Uri resource = new Uri("https://test.api.plex.com/Engineering/PartList/Parts?PartNo=z");
      JToken jsonVal = rpc.Execute(Method.GET, resource);

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
      Rpc rpc = new Rpc();
      Uri resource = new Uri("https://test.api.plex.com/Engineering/PartList/Parts/" + id.ToString());
      JToken jsonVal = rpc.Execute(Method.GET, resource);

      PartModel Part = null;
      if (jsonVal == null)
      {
        // todo: handle no parts returned
      }
      else
      {
        Part = jsonVal.ToObject<PartModel>();
      }

      return Part;
    }

    public PartModel GetByType(int type)
    {
      return null;
      //var request = new RestRequest("api/part/type/{datatype}", Method.GET)
      //{
      //  RequestFormat = DataFormat.Json
      //};

      //request.AddParameter("datatype", type, ParameterType.UrlSegment);

      //var response = _client.Execute<PartModel>(request);

      //return response.Data;
    }

    public PartModel GetByIP(int ip)
    {
      return null;
      //var request = new RestRequest("api/part/ip/{ip}", Method.GET) { RequestFormat = DataFormat.Json };
      //request.AddParameter("ip", ip, ParameterType.UrlSegment);

      //var response = _client.Execute<PartModel>(request);

      //return response.Data;
    }

    public void Add(PartModel part)
    {
      //var request = new RestRequest("api/part", Method.POST) { RequestFormat = DataFormat.Json };
      //request.AddBody(part);

      //var response = _client.Execute<PartModel>(request);

      //if (response.StatusCode != HttpStatusCode.Created)
      //  throw new Exception(response.ErrorMessage);

    }

    public void Update(PartModel part)
    {
      //var request = new RestRequest("api/part/{id}", Method.PUT) { RequestFormat = DataFormat.Json };
      //request.AddParameter("id", part.PartKey, ParameterType.UrlSegment);
      //request.AddBody(part);

      //var response = _client.Execute<PartModel>(request);

      //if (response.StatusCode == HttpStatusCode.NotFound)
      //  throw new Exception(response.ErrorMessage);
    }

    public void Delete(int id)
    {
      Rpc rpc = new Rpc();
      Uri resource = new Uri("https://test.api.plex.com/Engineering/PartList/Parts/" + id.ToString());
      JToken jsonVal = rpc.Execute(Method.DELETE, resource);


      if (rpc.Response.StatusCode != HttpStatusCode.OK)
      {
        string message = rpc.Response.StatusCode + ": " + ((dynamic)jsonVal).detail;

        if (rpc.Response.ErrorMessage != null)
        {
          message += rpc.Response.ErrorMessage;
        }

        System.Exception exception = new Exception(message);
        throw exception;
        //return false;
      }
    }
  }
}