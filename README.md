# Connect-REST-MVC4
MVC4 Sample apps for Plex Connect RPC over JSON.

This example is expected to run from a private client such as a web server accessing the Plex API as a Server Application. See [Daemon or Server Application to Web API](https://azure.microsoft.com/en-us/documentation/articles/active-directory-authentication-scenarios/#daemon-or-server-application-to-web-api) explanation in Microsoft's [Authentication Scenarios for Azure AD](https://azure.microsoft.com/en-us/documentation/articles/active-directory-authentication-scenarios/).

To get this example operational:

* Make sure, possibly by using Postman or some other REST Client, that the resource in ConsumeWebAPI.Helper.GetSeveralParts will return some usable parts for your tenant. Otherwize, change its filter appopriately.
* In the App_Data folder copy the  PlexConnectConfig.json.sample as PlexConnectConfig.json and fill it in with the relevant details obtained from Plex for your application. Review the ConsumeWebAPI.Helper.Execute method and make sure your config file or its contents is accessed from a secure location.
* At this time, this is the resource and token endpoint for your PlexConnectConfig.json (client id and secrets you get from Plex):
`"plexResource": "http://api.plex.com/",`
`"tokenEndPoint": "https://plexconnect.azure-api.net/oauth2/v1/token"`
* At this time, the PartList edit/copy/delete resources are down while they are being reconfigured. This is tracked on our internal issue PC-354. For now, to see somethinig, downlaod and build this solution which still allows you to return a Part List and click through to the Details of a part record.
