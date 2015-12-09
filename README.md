# Connect-REST-MVC4
MVC4 Sample apps for Plex Connect REST: 2- and 3-legged.

This example is expected to run from a private client such as a web server accessing the Plex API as a Server Application. See [Daemon or Server Application to Web API](https://azure.microsoft.com/en-us/documentation/articles/active-directory-authentication-scenarios/#daemon-or-server-application-to-web-api) explanation in Microsoft's [Authentication Scenarios for Azure AD](https://azure.microsoft.com/en-us/documentation/articles/active-directory-authentication-scenarios/).

To get this example operational:

1. Make sure, possibly by using Postman or some other REST Client, that the resource in ConsumeWebAPI.Helper.GetSeveralParts will return some usable parts for your tenant.
2. Copy PlexConnectConfig.json.sample as PlexConnectConfig.json and fill it in with the relevant details obtained from Plex for your application. Review the ConsumeWebAPI.Helper.Execute method and make sure your config file or its contents is accessed from a secure location.