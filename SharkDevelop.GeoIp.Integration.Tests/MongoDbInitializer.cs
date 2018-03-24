using MongoDB.Bson;
using MongoDB.Driver;
using SharkDevelop.GeoIp.Api.Repositories.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace SharkDevelop.GeoIp.Api.Integration.Tests
{
    public class MongoDbInitializer
    {
        private IMongoCollection<BsonDocument> _collection;

        internal void Initialize()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["mongoDb"].ConnectionString;
            var client = new MongoClient(connectionString);
            var geoLocationDb = client.GetDatabase("GeoLocation");
            _collection = geoLocationDb.GetCollection<BsonDocument>("IpLocations");

            var ipLocations = getIpLocations();
            foreach (var ipLocation in ipLocations)
            {
                AddIpLocationIfNotExist(ipLocation);
            }
        }

        public void AddIpLocationIfNotExist(IpLocation ipLocation)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Ip", ipLocation.Ip);

            var document = _collection.Find(filter).ToList().SingleOrDefault();
            if (document == null)
            {
                _collection.InsertOne(ipLocation.ToBsonDocument());
            }
        }

        internal List<IpLocation> getIpLocations()
        {
            return new List<IpLocation>()
            {
                new IpLocation
                {
                    Ip = "45.65.58.0",
                    Country = "Afganistan"
                },
                new IpLocation
                {
                    Ip = "63.245.112.0",
                    Country = "Bahamas"
                },
                new IpLocation
                {
                    Ip = "24.231.32.0",
                    Country = "Bahamas"
                },
                new IpLocation
                {
                    Ip = "4.15.156.30",
                    Country = "Canada"
                },
                new IpLocation
                {
                    Ip = "4.69.143.233",
                    Country = "Denmark"
                },
                new IpLocation
                {
                    Ip = "81.248.56.0",
                    Country = "French Guiana",
                },
                new IpLocation
                {
                    Ip = "2.135.241.24",
                    Country = "Russian Federation"
                },
                new IpLocation
                {
                    Ip = "5.134.64.0",
                    Country = "Poland"
                },
                new IpLocation
                {
                    Ip = "5.134.208.0",
                    Country = "Poland"
                },
                new IpLocation
                {
                    Ip = "12.204.8.24",
                    Country = "PuertoRico"
                }
            };
        }
    }
}
