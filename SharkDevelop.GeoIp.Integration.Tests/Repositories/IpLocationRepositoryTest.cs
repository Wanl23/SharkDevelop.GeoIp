using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharkDevelop.GeoIp.Api.Repositories;
using SharkDevelop.GeoIp.Api.Repositories.Data;
using System.Collections.Generic;
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
            var mongoDbInitializer = new MongoDbInitializer();
            _target = new IpLocationRepository();
            if (existingIpLocations == null)
            {
                mongoDbInitializer.Initialize();
                existingIpLocations = mongoDbInitializer.getIpLocations();
            }
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
    }
}
