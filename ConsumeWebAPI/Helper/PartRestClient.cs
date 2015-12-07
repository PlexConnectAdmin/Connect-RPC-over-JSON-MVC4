using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web;
using System.Linq;
using ConsumeWebAPI.Models;
using RestSharp;
using Newtonsoft.Json;
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

    public void Copy(PartCopyModel partCopyModel, int partKey)
    {
      Rpc rpc = new Rpc();
      Uri resource = new Uri("https://test.api.plex.com/Engineering/PartList/Parts/" + partKey.ToString() + "/PartCopy");

      string json = JsonConvert.SerializeObject(partCopyModel);

      JToken jsonVal = rpc.Execute(Method.POST, resource, json);

      if (rpc.Response.StatusCode != HttpStatusCode.OK)
      {
        string message = rpc.Response.StatusCode + ": " + ((dynamic)jsonVal).detail;

        if (rpc.Response.ErrorMessage != null)
        {
          message += rpc.Response.ErrorMessage;
        }

        System.Exception exception = new Exception(message);
        throw exception;
      }

      // JObject joResponse = JObject.Parse(response.Content);

      JValue status = (JValue)jsonVal["status"];
      if (status == null)
      {
        JObject joResult = (JObject)jsonVal["result"];
        JObject data = (JObject)joResult["data"];

        JToken errorMessage = data["errorMessage"];
        if (errorMessage.HasValues)
        {
          System.Exception exception = new Exception(errorMessage.ToString());
          throw exception;
        }
        else
        {
          JValue success = (JValue)data["success"];
          if ((bool)success)
          {
            // todo: get new part key here and navigate to its detail form w/success message

            // Example of success response
            //                {
            //  "result": {
            //    "data": {
            //      "errorMessage": null,
            //      "success": true,
            //      "partKey": 2260280,
            //      "partNo": "o0ionnoicn24n9o"
            //    },
            //    "validationResult": null,
            //    "revisionTrackingContext": null
            //  }
            //}
          }
          else
          {
            System.Exception exception = new Exception(jsonVal.ToString());
            throw exception;

            // Example of unsuccessful response
            //                {
            //  "result": {
            //    "data": {
            //      "errorMessage": "The Part No and Revision you specified already exists",
            //      "success": false,
            //      "partKey": 0,
            //      "partNo": null
            //    },
            //    "validationResult": null,
            //    "revisionTrackingContext": null
            //  }
            //}
          }
        }
      }
      else
      {
        System.Exception exception = new Exception(status.ToString() + jsonVal.ToString());
        throw exception;
      }


      // todo: clean up the code above, possible using dynamic: 
      //((dynamic)jsonVal).detail

      //string jsonErrMsg (dynamic)jsonVal.result.data.errorMessage;
      //if  != null)
      //{
      //  System.Exception exception = new Exception((((dynamic)jsonVal).result.data.errorMessage));
      //  throw exception;
      //}
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
      }
    }
  }
}