using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IIS_DistanceBetweenTwoCities.Api;
using IIS_DistanceBetweenTwoCities.Models;
using Microsoft.AspNetCore.Mvc;

namespace IIS_DistanceBetweenTwoCities.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private const double TO_MILES = 0.621371192;

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            GeoLocation location = Location.Get("Zagreb");

            return new string[] { location.ToString() };

            
        }

        // GET api/values/StockHolm
        [HttpGet("{firstCityName}")]
        public IEnumerable<string> Get(string firstCityName)
        {
            GeoLocation location = Location.Get(firstCityName);

            return new string[] { location.ToString() };
        }


        // GET api/values/Stockhholm/Berlin
        [HttpGet("{firstCityName}/{secondCityName}")]
        public IEnumerable<string> Get(string firstCityName, string secondCityName)
        {
            GeoLocation locationFirstCity = Location.Get(firstCityName);
            GeoLocation locationSecondCity = Location.Get(secondCityName);


            return new string[] { firstCityName.ToUpper()+ locationFirstCity.ToString(), secondCityName.ToUpper()  + locationSecondCity.ToString() };
        }


        // GET api/values/Stockhholm/Berlin/km
        [HttpGet("{firstCityName}/{secondCityName}/{measuringUnit}")]
        public IEnumerable<string> Get(string firstCityName, string secondCityName, string measuringUnit)
        {
            GeoLocation locationFirstCity = Location.Get(firstCityName);
            GeoLocation locationSecondCity = Location.Get(secondCityName);


            var R = 6371; 
            var φ1 = DegreeToRadian(locationFirstCity.Latitude);
            var φ2 = DegreeToRadian(locationSecondCity.Latitude);
            var Δφ = DegreeToRadian((locationSecondCity.Latitude - locationFirstCity.Latitude));
            var Δλ = DegreeToRadian((locationSecondCity.Longitude - locationFirstCity.Longitude));

            var a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                    Math.Cos(φ1) * Math.Cos(φ2) *
                    Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var distance = R * c;

            measuringUnit.ToLower();

            switch (measuringUnit)
            {
                case "kilometers":
                    return new string[] {String.Format( "The airline distance between {0} and {1} is {2} {3}.",firstCityName,secondCityName, distance,measuringUnit) };
                case "miles":
                    return new string[] { String.Format("The airline distance between {0} and {1} is {2} {3}.", firstCityName, secondCityName, distance*TO_MILES, measuringUnit) };
                default:
                    return new string[] { "You wrote something wrong" };
                    
            }


        }



    }
}
