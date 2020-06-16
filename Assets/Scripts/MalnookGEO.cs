using System;
using System.Collections;
using System.Net;
using System.IO;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MalnookGEO
{
    //test class to debug functionality
    class Test
    {
        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            //#TODO to get GPS coordinates in Unity!!!
            GeoInfo instance = new GeoInfo(1.348700, 103.700371, 5000);
            //test places API requests

            /*ArrayList foodPlaces = instance.getFood();
            foreach (Place place in foodPlaces)
            {
                Console.WriteLine(place);
            }
            Console.WriteLine("\n\nTotal count: " + foodPlaces.Count + "\n");

            ArrayList eduPlaces = instance.getEdu();
            foreach (Place place in eduPlaces)
            {
                Console.WriteLine(place);
            }
            Console.WriteLine("\n\nTotal count: " + eduPlaces.Count + "\n");

            ArrayList shopPlaces = instance.getShops();
            foreach (Place place in shopPlaces)
            {
                Console.WriteLine(place);
            }
            Console.WriteLine("\n\nTotal count: " + shopPlaces.Count + "\n");*/

            //test weather api request
            Console.WriteLine(instance.getWeather());

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Elapsed time (milliseconds): " + elapsedMs);
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

        //returns weather as string. Possible outputs: "Thunderstorn", "Drizzle", "Rain", "Snow", "Clear", "Clouds"
        public string getWeather()
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
        public ArrayList getFood()
        {
            string[] foodTags = new string[]{"restaurant", "cafe", "supermarket"};
            ArrayList foodPlaces = new ArrayList();
            //make request to API for food places in radius
            foreach (string foodTag in foodTags)
            {
                //make search using foodtag, adding results to arraylist
                ArrayList results = makeSearch(foodTag);
                Console.WriteLine("total results for " + foodTag + " = " + results.Count);
                foodPlaces.AddRange(results);

            }            
            return foodPlaces;
        }
        //returns list of place objects (for education in specified radius around specified coordinates)
        public ArrayList getEdu()
        {
            string[] eduTags = new string[] { "primary_school", "secondary_school", "university" };
            ArrayList eduPlaces = new ArrayList();
            //make request to API for food places in radius
            foreach (string eduTag in eduTags)
            {
                //make search using foodtag, adding results to arraylist
                ArrayList results = makeSearch(eduTag);
                Console.WriteLine("total results for " + eduTag + " = " + results.Count);
                eduPlaces.AddRange(results);

            }
            return eduPlaces;
        }
        //returns list of place objects (for shops in specified radius around specified coordinates)
        public ArrayList getShops()
        {
            string[] shopTags = new string[] { "store"};
            ArrayList shopPlaces = new ArrayList();
            //make request to API for food places in radius
            foreach (string shopTag in shopTags)
            {
                //make search using foodtag, adding results to arraylist
                ArrayList results = makeSearch(shopTag);
                Console.WriteLine("total results for " + shopTag + " = " + results.Count);
                shopPlaces.AddRange(results);

            }
            return shopPlaces;
        }

        //makes a standard Google Maps Nearby Search API request
        private ArrayList makeSearch(string tag)
        {
            string baseURL = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?key=";
            string reqURL = baseURL + mapsAPIKey
                + "&location=" + lat + "," + lon
                + "&radius=" + rad
                + "&type=" + tag;
            //make json request         
            var reqJson = JsonConvert.DeserializeObject<PlacesResponse>(getJson(reqURL));
            return placesParse(reqJson);
        }

        //called if makeSearch response has more than one page. Utilizes nextPgToken (retrieved from makeSearch response)
        private ArrayList nextPgSearch(ArrayList rtnPlaces, string nextPgToken)
        {
            string baseURL = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?key=";
            string reqURL = baseURL + mapsAPIKey
                + "&pagetoken=" + nextPgToken;
            var reqJson = JsonConvert.DeserializeObject<PlacesResponse>(getJson(reqURL));

            rtnPlaces.AddRange(placesParse(reqJson));
            return rtnPlaces;
        }

        //parses each page of Nearby Search API results. If no next page exists, returns results. Else calls nextPgSearch
        private ArrayList placesParse(PlacesResponse parsedJson)
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
            if (nextPgToken != null)
            {
                //Console.WriteLine("next pg exists");
                System.Threading.Thread.Sleep(2000);
                return nextPgSearch(rtnPlaces, nextPgToken);
            } else
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

        public override string Tostring()
        {
            return "Place: " + name + " xDist: " + xdist + " yDist: " + ydist + " Dist: " + dist;
        }

    }

    //structure to deserialize Nearby Search API JSON responses
    class PlacesResponse
    {
        public string next_page_token { get; set; }
        public List<PlacesResult> results{ get; set; }
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
    
    
    
}
