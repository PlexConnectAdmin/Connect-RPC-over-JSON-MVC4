using System;
using System.Collections.Generic;
using System.Net;
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

      // default limit is 10, here we set it to 50 by way of example
      Uri resource = new Uri("https://test.api.plex.com/Engineering/PartList/Parts?PartNo=z&limit=50");
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

    public void Add(PartAddModel part)
    {
      Rpc rpc = new Rpc();
      Uri resource = new Uri("https://test.api.plex.com/Engineering/PartList/Parts");

      string json = JsonConvert.SerializeObject(part);

      JToken jsonVal = rpc.Execute(Method.POST, resource, json);

      if ((rpc.Response.StatusDescription == "Missing Identity Response Field.") && (rpc.Response.StatusCode == HttpStatusCode.BadRequest))
      {
        // Creating parts is basically inop, but can be used, with caution
        // Yubo Dong: "The CreatePart failure is expected. The framework now looking for a resource identifier when creating new resource. The CreatePart action class in F5 currently doesn’t meet this requirement. Please disable the test for CreatePart for now."
        // This line will allow us it ignore the response error, since the part is actually created
        return;
      }

      // TODO: GENERalize this code into a private method
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