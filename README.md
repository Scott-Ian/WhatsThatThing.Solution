# <h1 align = "center"> Park Lookup

## <h3 align = "center"> C#/.NET API,  8.21.20

## <h2 align = "center"> About

<p align = "center"> This is an API for State and National Parks. It holds information for the parks including their name, the state in which they're found, and highlights of the park.

## **✅REQUIREMENTS**
* Install [Git v2.62.2+](https://git-scm.com/downloads/)
* Install [.NET version 3.1 SDK v2.2+](https://dotnet.microsoft.com/download/dotnet-core/2.2)
* Install [Visual Studio Code](https://code.visualstudio.com/)
* Install [MySql Workbench](https://www.mysql.com/products/workbench/)
* Install [Postman](https://www.postman.com/)

## **💻SETUP**
* to clone this content, copy the url provided by the green 'Code' button in GitHub
* in command line use the command 'git clone (GitHub url)'
* open the program in a code editor
* navigate to the ParkLookup directory and type **dotnet build** in the command line to compile the code
* remaining in the ParkLookup directory type **dotnet ef database update** to create the database
* type dotnet run in the command line to run the program
* open postman and navigate to your local server (likely http://localhost:5000)
* all commands in the documentation can be appended to the local host route to navigate API data
<br>

## Documentation

### HTTP Request
| Request | National Parks | State Parks | Result |
| :---------- | ----- | ----- | -----: |
| GET | /api/nationalpark | /api/statepark | list of all national or state parks |
| POST | /api/nationalpark | /api/statepark | create a national or state park |
| GET | /api/nationalpark/{id} | /api/statepark{id} | show selected (by id) national or state park |
| PUT | /api/nationalpark/{id} | /api/statepark{id} | edit selected (by id) national or state park |
| DELETE | /api/nationalpark/{id} | /api/statepark{id} | delete selected (by id) national or state park |
<br>

### Path Parameters
| Parameter | Type | Example |Description |
| :---------- | ----- | ----- | -----: |
| name | string | name=Olympic | Return matches by park name, accepts partial strings |
| state | string | state=Idaho | Return matches by park state, accepts partial strings |
| surprise | string | surprise=surprise | Return a surprise selection! | 
<br>

### Example Query

``` 
http://localhost:5000/api/nationalpark?state=washington&name=olympic 
```
<br>

### Sample JSON Response

``` 
{
    "stateParkId": 5,
    "stateParkName": "Smith Rock",
    "stateParkState": "Oregon",
    "stateParkHighlight": "climbing",
    "stateParkCamping": true,
    "stateParkHiking": true,
    "stateParkFishing": true
}
 ```
 <br>

### Pagination

The Park Lookup API returns a default of 10 parks per page. To 'scroll' through pages use the search **/api/nationalpark/?pageNumber=x**, where x is the page number you wish to view. To change the number of parks returned per page, use the search **/api/nationalpark/?pageSize=x**, where x is the number of parks you wish to view per page.
## 🐛Known Bugs

_No known bugs_

## 📫Support and contact details

Contact : Megan Hepner

## 🔧Technologies Used

* C#
* ASP.NET MVC
* Entity
* MySql


## **📘 License**
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)