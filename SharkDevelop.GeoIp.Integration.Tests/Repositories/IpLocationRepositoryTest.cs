using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using SharkDevelop.GeoIp.Api.Repositories;
using SharkDevelop.GeoIp.Api.Repositories.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace SharkDevelop.GeoIp.Api.Integration.Tests.Repositories
{
    [TestClass]
    public class IpLocationRepositoryTest
    {
        private IpLocationRepository _target;
        private List<IpLocation> existingIpLocations;

        [TestInitialize]
        public void Setup()
        {
            DbInitialize();
            _target = new IpLocationRepository();
        }

        [TestMethod]
        public async Task GivenExistingIpAddress_WhenGetCountryName_ThenReturnCountry()
        {
            var existingIpLocation = existingIpLocations.First();

            var actual = await _target.GetCountryNameAsync(existingIpLocation.Ip);

            Assert.AreEqual(existingIpLocation.Country, actual);
        }

        [TestMethod]
        public async Task GivenNonExistingIpAddress_WhenGetCountryName_ThenReturnNull()
        {
            var nonExistingIp = "192.111.111.001";

            var actual = await _target.GetCountryNameAsync(nonExistingIp);

            Assert.IsNull(actual);
        }

        private void DbInitialize()
        {
            BsonClassMap.RegisterClassMap<IpLocation>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(c => c._id)
                        .SetIgnoreIfDefault(true)
                        .SetIdGenerator(ObjectIdGenerator.Instance);
            });

            var connectionString = ConfigurationManager.ConnectionStrings["mongoDb"].ConnectionString;
            var client = new MongoClient(connectionString);
            var geoLocationDb = client.GetDatabase("GeoLocation");
            var collection = geoLocationDb.GetCollection<BsonDocument>("IpLocations");

            var ipLocations = getIpLocations();
            foreach (var ipLocation in ipLocations)
            {
                collection.InsertOne(ipLocation.ToBsonDocument());
            }
        }

        private List<IpLocation> getIpLocations()
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
