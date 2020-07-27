# GeolocationWebService
A simple web service that provides IP address location information using free data sources.

## Installation

### Requirements

Windows 10 or compatible operating system.  
Approximately 2 GB of free space will be required.

### Download IP2Location LITE DB11 Data

We aren't allowed to redistribute the location database, and we wouldn't want to anyway.  It is relatively large and updated often.  So you will need to get it yourself.

1. Go to [https://lite.ip2location.com/](https://lite.ip2location.com/) and locate DB11.LITE.

There are separate files for IPv4 and IPv6, *but you only need one of them*.

To support IPv4 and IPv6 addresses, download only the DB11.LITE **IPV6 CSV** file from the link above. This includes all IPv4 addresses mapped to IPv6, and therefore can be used to resolve both IPv4 and IPv6 addresses.  

If you need only IPv4 support, you can save a little disk space by getting only the DB11.LITE **IPV4 CSV**.

2. Extract the .csv file from the zip archive.

### Install GeolocationWebService

1. Download a release from [the releases tab](https://github.com/bp2008/GeolocationWebService/releases).  
2. Extract the zip wherever you like.  
3. Run `GeolocationWebService.exe`.

![screenshot](https://i.imgur.com/cGJY2bD.png)

4. Close the service manager. Note the `Settings.cfg` which has been created next to the executable.  You may configure the HTTP or HTTPS listening ports here, or use the default HTTP port of 52280.

### Import location data

1. Reopen `GeolocationWebService.exe` and click "Setup".
2. Browse to the .csv file you exported, and begin the import.  This should take around one minute or less depending on your CPU and disk speed.
3. If you imported the IPv4 table, uncheck the box "Resolve IPv4 queries with the IPv6 table".  If you imported the IPv6 table, leave the box checked.

### Run as a Windows Service

1. Use the `Install Service` and `Start Service` buttons to manage the Windows service.  If you want to move the program, you will need to uninstall and reinstall using the provided buttons.

## Usage

Open the web service (if you are not sure how, there is an `Open Web Interface` button in the service manager).  You will be presented with an informational page demonstrating the service's various endpoints.

![screenshot](https://i.imgur.com/yriVhbfm.png)

![screenshot](https://i.imgur.com/EgHiQGgm.png)

### JSON Endpoint

You will probably want to utilize the JSON endpoint for integration with your own software.

Example:  
```
/ip/8.8.8.8
```
``` json
{"ip_from":134744064,"ip_to":134744319,"country_code":"US","country_name":"United States of America","region_name":"California","city_name":"Mountain View","latitude":37.405992,"longitude":-122.078515,"zip_code":"94043","time_zone":"-08:00"}
```

### Map Endpoint

To retrieve a map showing the location, use the map endpoint.

`/map/{zoom}/{IP}.jpg`

Example zoomed out: `/map/0/8.8.8.8.jpg`

![map zoom 0](https://i.imgur.com/Jh3Q5U0.jpg)

The maximum zoom level is 5. `/map/5/8.8.8.8.jpg`

![map zoom 5](https://i.imgur.com/S9Y23yh.jpg)

**Note about zoom**: The mapping function is not very efficient, loading an image of the entire earth and cropping it.  To help avoid excessive resource consumption, only one request at a time will be fulfilled for maps at zoom levels 3-5.

## Building From Source

To build from source, clone the repository and also my [BPUtil repository](https://github.com/bp2008/BPUtil).  Open in Visual Studio 2019 community edition.  You may need to remove and re-add the `BPUtil` project to the solution, and add `BPUtil` as a reference in the `GeolocationWebService` project.
