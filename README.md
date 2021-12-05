# Homey-.Net

|Description      |Link        |
|-----------------|------------|
|Build            |[![Build Status](https://my-tech-projects.visualstudio.com/Homey-.Net/_apis/build/status/Release%20Build?branchName=master)](https://my-tech-projects.visualstudio.com/Homey-.Net/_build/latest?definitionId=12&branchName=master)|
|Nuget            |[![Nuget](https://img.shields.io/nuget/v/Homey.Net)](https://www.nuget.org/packages/Homey.Net)|
|Nuget Prerelease |![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Homey.Net)

## Intro
This repo contains the source code for a C# Api for Athom's Homey.
It does not yet cover the complete feature set.
There is a java script api client available https://developer.athom.com/docs/api.
The Rest Api can be guessed according to the documentation.

## Usage

### Initialize with authentication
```
// Homey Login credentials
private const string UserName = "<USER_NAME>";
private const string Password = "<PASSWORD>";

// client id and client secret from: https://tools.developer.homey.app/api/projects
// cloud id from: https://tools.developer.homey.app/tools/system 
private static HomeyApiConfig _apiConfig = new HomeyApiConfig
{
    ClientId = "<CLIENT_ID>",
    ClientSecret = "<CLIENT_SECRET>",
    CloudId = "<CLOUD_ID>"
};

// You can check the local homey ip over the app with More/Settings/General/About
HomeyClient client = new HomeyClient
{
    HomeyIp = HomeyIp
};

client.Authenticate(_apiConfig, UserName, Password)

```

### Initialize with bearer token
See section below how to extract the bearer token manually

```
// You can check the local homey ip over the app with More/Settings/General/Abou
HomeyClient client = new HomeyClient
{
    Token = BearerToken,
    HomeyIp = HomeyIp
};
```

### Fetch Data

Implemented commands
* Get device for id
* Get all devices
* Get devices for zone id
* Get capability time log
* Gel all zones
* Get all flows
* Get flow for flow id
* Get all alarms
* Update alarm
* Get system info
* Set boolean capability
* Enable flow
* Trigger flow

```
IList<Device> devices = await client.GetDevices();
IList<Flow> flows = await client.GetFlows();
IList<Alarm> alarms = await client.GetAlarms();
...
```

## Sample Application
![Screenshot](docu/screenshot.png)

## Authentication
During my reverse engineering I stumbled over the following blog post:
https://homey.solweb.no/advanced-api-usage/bearertoken

The javascript code did not work out of the box for me so I did slight modifications in c#
The authentication is done with six requests to the following endpoints
1. https://accounts.athom.com/login
2. https://accounts.athom.com/oauth2/authorise?
3. https://accounts.athom.com/authorise?client_id=
4. https://api.athom.com/oauth2/token
5. https://api.athom.com/delegation/token?audience=homey
6. https://' + cloudid + '.connect.athom.com/api/manager/users/login

## Obtaining an Access Token manually
Homey does not yet provide an access token, however you can obtain one with the following steps.

1. Open https://developer.athom.com/
2. Press Login
3. Open the browser dev tools --> F12 and switch to the Network tab
4. Log in with your Homey mail and password
    You should find a Get request "api/manager/sessions/session/me" which contains you local homey ip address if you are in the local network. Request URL: "https://LOCAL-HOMEY-IP.homey.homeylocal.com/api/manager/sessions/session/me" There you should fine something like the following under Request Header
    "authorization: Bearer 888999-19fe-4fa0-9999-b32982499999:9c1a21da-1df4-9999-8594-46fd7e799999:c6d7b6096df10a5fd2d8888888ff447ff1ca0e9c"
5. Use the whole token after Beaer in that case it would be "888999-19fe-4fa0-9999-b32982499999:9c1a21da-1df4-9999-8594-46fd7e799999:c6d7b6096df10a5fd2d8888888ff447ff1ca0e9c"
