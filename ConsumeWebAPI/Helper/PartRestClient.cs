using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
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

      // todo:  default limit is 10, here we set it to 50 by way of example
      // todo: read from config
      Uri resource = new Uri("https://plexconnect.azure-api.net/test/PartSetup/v1/PartList/Parts?PartNo=1");
      Task<JToken> jsonValTask = Rpc.Execute(Method.GET, resource);

      // another possible check: if (jsonValTask.Status == TaskStatus.Faulted)
      if (jsonValTask.Exception == null)
      {
        JToken jsonVal = jsonValTask.Result;

        // todo: HAL-formatted response has links "next" and "last" (previous) for paging: ...offset=380&limit=10... etc. Implement this paging.

        JArray joResponses = ((dynamic)jsonVal).embedded;

        List<PartModel> parts = null;
        if (joResponses == null)
        {
          // todo: handle no parts returned
        }
        else
        {
          parts = joResponses.ToObject<List<PartModel>>();
        }

        // todo: try/catch for error handling on all code

        return parts;
      }
      else // some error occurred
      {
        // first, see if no records were found
        if (jsonValTask.Exception.InnerExceptions.Count==1)
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

    public PartModel GetById(int id)
    {
      Rpc rpc = new Rpc();
      Uri resource = new Uri("https://test.api.plex.com/Engineering/PartList/Parts/" + id.ToString());
      Task<JToken> jsonValTask = Rpc.Execute(Method.GET, resource);
      JToken jsonVal = jsonValTask.Result;

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

    public void Add(PartAddModel part)
    {
      Rpc rpc = new Rpc();
      Uri resource = new Uri("https://test.api.plex.com/Engineering/PartList/Parts");

      string json = JsonConvert.SerializeObject(part);

      Task<JToken> jsonValTask = Rpc.Execute(Method.POST, resource, json);
      JToken jsonVal = jsonValTask.Result;

      if ((Rpc.Response.StatusDescription == "Missing Identity Response Field.") && (Rpc.Response.StatusCode == HttpStatusCode.BadRequest))
      {
        // Creating parts is basically inop, but can be used, with caution
        // Yubo Dong: "The CreatePart failure is expected. The framework now looking for a resource identifier when creating new resource. The CreatePart action class in F5 currently doesn’t meet this requirement. Please disable the test for CreatePart for now."
        // This line will allow us it ignore the response error, since the part is actually created
        return;
      }

      // TODO: GENERalize this code into a private method
      if (Rpc.Response.StatusCode != HttpStatusCode.OK)
      {

        string message = Rpc.Response.StatusCode + ": " + ((dynamic)jsonVal).detail;

        if (Rpc.Response.ErrorMessage != null)
        {
          message += Rpc.Response.ErrorMessage;
        }

        System.Exception exception = new Exception(message);
        throw exception;
      }
    }

    public void Copy(PartCopyModel partCopyModel, int partKey)
    {
      System.Exception exception = new Exception("Not Implemented");
      throw exception;

      //Rpc rpc = new Rpc();
      //Uri resource = new Uri("https://test.api.plex.com/Engineering/PartList/Parts/" + partKey.ToString() + "/PartCopy");

      //string json = JsonConvert.SerializeObject(partCopyModel);

      //Task<JToken> jsonValTask = rpc.Execute(Method.POST, resource, json);
      //JToken jsonVal = jsonValTask.Result;

      //if (Rpc.Response.StatusCode != HttpStatusCode.OK)
      //{
      //  string message = Rpc.Response.StatusCode + ": " + ((dynamic)jsonVal).detail;

      //  if (Rpc.Response.ErrorMessage != null)
      //  {
      //    message += Rpc.Response.ErrorMessage;
      //  }

      //  System.Exception exception = new Exception(message);
      //  throw exception;
      //}

      //// JObject joResponse = JObject.Parse(response.Content);

      //JValue status = (JValue)jsonVal["status"];
      //if (status == null)
      //{
      //  JObject joResult = (JObject)jsonVal["result"];
      //  JObject data = (JObject)joResult["data"];

      //  JToken errorMessage = data["errorMessage"];
      //  if (errorMessage.HasValues)
      //  {
      //    System.Exception exception = new Exception(errorMessage.ToString());
      //    throw exception;
      //  }
      //  else
      //  {
      //    JValue success = (JValue)data["success"];
      //    if ((bool)success)
      //    {
      //      // todo: get new part key here and navigate to its detail form w/success message

      //      // Example of success response
      //      //                {
      //      //  "result": {
      //      //    "data": {
      //      //      "errorMessage": null,
      //      //      "success": true,
      //      //      "partKey": 2260280,
      //      //      "partNo": "o0ionnoicn24n9o"
      //      //    },
      //      //    "validationResult": null,
      //      //    "revisionTrackingContext": null
      //      //  }
      //      //}
      //    }
      //    else
      //    {
      //      System.Exception exception = new Exception(jsonVal.ToString());
      //      throw exception;

      //      // Example of unsuccessful response
      //      //                {
      //      //  "result": {
      //      //    "data": {
      //      //      "errorMessage": "The Part No and Revision you specified already exists",
      //      //      "success": false,
      //      //      "partKey": 0,
      //      //      "partNo": null
      //      //    },
      //      //    "validationResult": null,
      //      //    "revisionTrackingContext": null
      //      //  }
      //      //}
      //    }
      //  }
      //}
      //else
      //{
      //  System.Exception exception = new Exception(status.ToString() + jsonVal.ToString());
      //  throw exception;
      //}
    }

    public void Delete(int id)
    {
      System.Exception exception = new Exception("Not Implemented");
      throw exception;

      //Rpc rpc = new Rpc();
      //Uri resource = new Uri("https://test.api.plex.com/Engineering/PartList/Parts/" + id.ToString());
      //Task<JToken> jsonValTask = Rpc.Execute(Method.DELETE, resource);
      //JToken jsonVal = jsonValTask.Result;

      //if (Rpc.Response.StatusCode != HttpStatusCode.OK)
      //{
      //  string message = Rpc.Response.StatusCode + ": " + ((dynamic)jsonVal).detail;

      //  if (Rpc.Response.ErrorMessage != null)
      //  {
      //    message += Rpc.Response.ErrorMessage;
      //  }

      //  System.Exception exception = new Exception(message);
      //  throw exception;
      //  }
    }
  }
}