using System;
using System.Collections;
using System.Net;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class MalnookGEO : MonoBehaviour
{
    private GeoInfo geo;
    public WeatherControl weather;
    public double lat, lon, rad;
    public Tilemap foodMap, educationMap, shopMap, poiMap;
    public Tile foodTile, educationTile, shopTile, poiTile;
    public TileGenerator tileGen;
    public Canvas buildingInfo;
    public Animator generating;
    public POIObject poiObj;
    void Start()
    {
        Debug.Log("STARTING!");

        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
        StartCoroutine(InitGPS());
        // FOR TESTING REMOVE ME FOR ACTUAL GPS
        //InitMap();
    }
    IEnumerator InitGPS()
    {
        // THIS WAITS FOR EDITOR TO CONNECT
        //yield return new WaitForSeconds(5);

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("No locations enabled in the device");
            SceneManager.LoadScene("Menu");
            yield break;
        }

        Input.location.Start();

        // Wait until service initializes
        int maxWait = 10;
        while (Input.location.status != LocationServiceStatus.Running && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            SceneManager.LoadScene("Menu");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            SceneManager.LoadScene("Menu");
            yield break;
        }

        lat = Input.location.lastData.latitude;
        lon = Input.location.lastData.longitude;
        Debug.Log(lat + ", " + lon);

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();

        InitMap();
    }

    void InitMap()
    {
        geo = new GeoInfo(lat, lon, rad);
        InitWeather();
        InitBuildings();
        generating.SetTrigger("Hide");
        
    }
    void InitWeather()
    {
        weather.Generate(geo.GetWeather());
    }

    void InitBuildings()
    {
        ArrayList foodList = geo.GetFood(3);
        ArrayList educationList = geo.GetEdu(3);
        ArrayList shopList = geo.GetShops(3);
        ArrayList poiList = geo.GetPOI(3);

        InitBuilding(foodMap, foodTile, foodList);
        InitBuilding(educationMap, educationTile, educationList);
        InitBuilding(shopMap, shopTile, shopList);
        InitBuilding(poiMap, poiTile, poiList);
    }

    void InitBuilding(Tilemap tilemap, Tile tile, ArrayList list)
    {
        foreach (Place place in list)
        {
            int tileX = DistanceToTile(place.xdist, tileGen.width);
            int tileY = DistanceToTile(place.ydist, tileGen.height);

            Vector3Int pos = new Vector3Int(tileX, tileY, 0);
            if (foodMap.GetTile(pos) != null || educationMap.GetTile(pos) != null || shopMap.GetTile(pos) != null) continue;

            Canvas spawnedInfo = Instantiate(buildingInfo, new Vector3(tileX + 0.3f, tileY + 1.2f, 0), Quaternion.identity);
            spawnedInfo.GetComponentInChildren<Text>().text = place.name;

            if (tilemap == poiMap)
            {
                Instantiate(poiObj, new Vector3(tileX + 0.5f, tileY + 0.5f, 0), Quaternion.identity);
            }
            //Debug.Log(place + " at " + tileX + " ," + tileY);
            tilemap.SetTile(pos, tile);
        }
    }

    int DistanceToTile(double dist, int tileSize)
    {
        int halfTile = tileSize / 2;
        double distPercent = (dist * 1000) / rad;
        return (int) (distPercent * halfTile) + halfTile;
    }
}

class GeoInfo
{
    static string mapsAPIKey = "AIzaSyDZtGO0M6PE4IALF2dugR_w287rTTismvQ"; //Google Maps API Key
    static string openWeatherAPIKey = "d9194e8aea7fc3a6c2dac1e0acab526c"; //OpenWeatherMaps API Key
    double lat; //current lat
    double lon; //current lon
    double rad; //current search radius
                //takes lat and long (from GPS ideally) and initial radius
    public GeoInfo(double lat, double lon, double initRad)
    {
        this.lat = lat;
        this.lon = lon;
        this.rad = initRad;
    }

    //returns weather as string. Possible outputs: "Thunderstorm", "Drizzle", "Rain", "Snow", "Clear", "Clouds"
    public string GetWeather()
    {
        string reqURL = "http://api.openweathermap.org/data/2.5/weather?"
            + "lat=" + this.lat
            + "&"
            + "lon=" + this.lon
            + "&"
            + "appid=" + openWeatherAPIKey;
        var reqJson = JsonConvert.DeserializeObject<WeatherResponse>(getJson(reqURL));
        return reqJson.weather[0].main;
    }

    //returns list of place objects (for food in specified radius around specified coordinates)
    public ArrayList GetFood(int pages)
    {
        string keyword = "food";
        //make request to API for food places in radius
        ArrayList results = makeSearch(keyword, pages);
        Console.WriteLine("total results for " + keyword + " = " + results.Count);
        return results;
    }

    //returns list of place objects (for edu in specified radius around specified coordinates)
    public ArrayList GetEdu(int pages)
    {
        string keyword = "school";
        //make request to API for edu places in radius
        ArrayList results = makeSearch(keyword, pages);
        Console.WriteLine("total results for " + keyword + " = " + results.Count);
        return results;
    }

    //returns list of place objects (for shops in specified radius around specified coordinates)
    public ArrayList GetShops(int pages)
    {
        string keyword = "shop";
        //make request to API for shop places in radius
        ArrayList results = makeSearch(keyword, pages);
        Console.WriteLine("total results for " + keyword + " = " + results.Count);
        return results;
    }

    //returns list of place objects (for poi in specified radius around specified coordinates)
    public ArrayList GetPOI(int pages)
    {
        string keyword = "tourist+attraction";
        //make request to API for shop places in radius
        ArrayList results = makeSearch(keyword, pages);
        Console.WriteLine("total results for " + keyword + " = " + results.Count);
        return results;
    }


    //makes a standard Google Maps Nearby Search API request
    private ArrayList makeSearch(string tag, int pages)
    {
        string baseURL = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?key=";
        string reqURL = baseURL + mapsAPIKey
            + "&location=" + lat + "," + lon
            + "&radius=" + rad
            + "&keyword=" + tag;
        //make json request         
        var reqJson = JsonConvert.DeserializeObject<PlacesResponse>(getJson(reqURL));
        return placesParse(reqJson, pages - 1); //-1 as reqJson is first page of 20
    }

    //called if makeSearch response has more than one page. Utilizes nextPgToken (retrieved from makeSearch response)
    private ArrayList nextPgSearch(ArrayList rtnPlaces, string nextPgToken, int pages)
    {
        string baseURL = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?key=";
        string reqURL = baseURL + mapsAPIKey
            + "&pagetoken=" + nextPgToken;
        var reqJson = JsonConvert.DeserializeObject<PlacesResponse>(getJson(reqURL));

        rtnPlaces.AddRange(placesParse(reqJson, pages - 1));
        return rtnPlaces;
    }

    //parses each page of Nearby Search API results. If no next page exists, returns results. Else calls nextPgSearch
    private ArrayList placesParse(PlacesResponse parsedJson, int pages)
    {
        ArrayList rtnPlaces = new ArrayList();
        //Console.WriteLine("parsing places");
        //process results
        List<PlacesResponse.PlacesResult> places = parsedJson.results;
        //add all results to return list
        foreach (PlacesResponse.PlacesResult placeResult in places)
        {
            double lat = placeResult.geometry.location.lat;
            double lon = placeResult.geometry.location.lng;
            Place place = new Place(placeResult.name, getXDist(lat, lon), getYDist(lat));
            rtnPlaces.Add(place);
        }
        //check for next page token
        string nextPgToken = parsedJson.next_page_token;
        if (nextPgToken != null && pages > 0) //next page exists and still ned more resultss
        {
            //Console.WriteLine("next pg exists");
            System.Threading.Thread.Sleep(2000);
            return nextPgSearch(rtnPlaces, nextPgToken, pages);
        }
        else
        {
            return rtnPlaces;
        }
    }

    //calculate X distance of a Place object from the specified coordinates
    private double getXDist(double lat, double lon)
    {
        double dx = (this.lon - lon) * 40000 * Math.Cos((this.lat + lat) * Math.PI / 360) / 360;
        return dx;
    }

    //calculate Y distance of a Place object from the specified coordinates
    private double getYDist(double lat)
    {
        double dy = (this.lat - lat) * 40000 / 360;
        return dy;
    }

    //method to get Json response when provided with a API request URl
    private string getJson(string reqURL)
    {
        var request = (HttpWebRequest)WebRequest.Create(reqURL);
        var response = (HttpWebResponse)request.GetResponse();
        string json = new StreamReader(response.GetResponseStream()).ReadToEnd();
        //Console.WriteLine(json);
        return json;
    }
}

//represents a Place object with it's name, X, Y and total distance.
class Place
{
    public string name;
    public double dist;
    public double xdist;
    public double ydist;

    //calculate and returns total distance
    static double calcDist(double xdist, double ydist)
    {
        return Math.Sqrt(Math.Pow(xdist, 2) + Math.Pow(ydist, 2));
    }
    public Place(string name, double xdist, double ydist)
    {
        this.name = name;
        this.xdist = xdist;
        this.ydist = ydist;
        this.dist = calcDist(xdist, ydist);
    }

    public override string ToString()
    {
        return "Place: " + name + " xDist: " + xdist + " yDist: " + ydist + " Dist: " + dist;
    }

}

//structure to deserialize Nearby Search API JSON responses
class PlacesResponse
{
    public string next_page_token { get; set; }
    public List<PlacesResult> results { get; set; }
    public class PlacesResult
    {
        public PlacesGeometry geometry { get; set; }
        public class PlacesGeometry
        {
            public GeometryLocation location { get; set; }
            public class GeometryLocation
            {
                public double lat { get; set; }
                public double lng { get; set; }
            }
        }
        public string name { get; set; }
    }

}

//structure to deserialize OpenWeather API JSON responses
class WeatherResponse
{
    public List<Weather> weather { get; set; }
    public class Weather
    {
        public string main { get; set; }
    }
}
