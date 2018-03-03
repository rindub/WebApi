using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIS_DistanceBetweenTwoCities.Models
{
    public class GeoLocation
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public override string ToString()
        {
            return String.Format("({0},{1})",Longitude.ToString(),Latitude.ToString());
        }
    }
}
