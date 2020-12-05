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

### Implemented commands

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


## Sample Application
![Screenshot](docu/screenshot.png)

## Obtaining an Access Token
Homey does not yet provide an access token, however you can obtain one with the following steps.

1. Open https://developer.athom.com/
2. Press Login
3. Open the browser dev tools --> F12 and switch to the Network tab
4. Log in with your Homey mail and password
5. Enter Bearer into the serach box
6. You should find something like authorization: Bearer 888999-19fe-4fa0-9999-b32982499999:9c1a21da-1df4-9999-8594-46fd7e799999:c6d7b6096df10a5fd2d8888888ff447ff1ca0e9c
in a get request similar to https://*.connect.athom.com/api/manager/sessions/session/me
7. Use the whole token after Beaer
