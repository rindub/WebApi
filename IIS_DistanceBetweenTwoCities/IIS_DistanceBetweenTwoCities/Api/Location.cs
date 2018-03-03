using IIS_DistanceBetweenTwoCities.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IIS_DistanceBetweenTwoCities.Api
{
    public class Location
    {
        public static GeoLocation Get(string cityName)
        {
            //WebRequest request = WebRequest.Create("https://maps.googleapis.com/maps/api/geocode/json?address="+ cityName+"&key=AIzaSyB13Hb0BZIcyNnUmthFPSgDe38HVYzloRU");
            //request.Method = "GET";
            //request.ContentType = "application/json";
            //WebResponse wr = request.GetResponseAsync().Result;
            //Stream receiveStream = wr.GetResponseStream();
            //StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
            //string content = reader.ReadToEnd();
            //return JsonConvert.DeserializeObject<GeoLocation>(content);

            using (WebClient wc = new WebClient())
            {

                var json = wc.DownloadString(@"https://maps.googleapis.com/maps/api/geocode/json?address=" + cityName + "&key=AIzaSyB13Hb0BZIcyNnUmthFPSgDe38HVYzloRU");
                dynamic parent = JObject.Parse(json);
                
                return new GeoLocation { Latitude = parent.results[0].geometry.location.lat, Longitude = parent.results[0].geometry.location.lng };
            }

        }
    }
}
