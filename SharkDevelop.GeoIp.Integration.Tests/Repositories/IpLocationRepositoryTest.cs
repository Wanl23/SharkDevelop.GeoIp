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
        private bool isDbInitialized = false;

        [TestInitialize]
        public void Setup()
        {
            _target = new IpLocationRepository();
        }

        [TestMethod]
        public async Task GivenExistingIpAddress_WhenGetCountryName_ThenReturnCountry()
        {
            InitializeDb();
            var existingIpLocation = existingIpLocations.First();

            var actual = await _target.GetCountryNameAsync(existingIpLocation.Ip);

            Assert.AreEqual(existingIpLocation.Country, actual);
        }

        [TestMethod]
        public async Task GivenNonExistingIpAddress_WhenGetCountryName_ThenReturnNull()
        {
            InitializeDb();
            var nonExistingIp = "192.111.111.001";

            var actual = await _target.GetCountryNameAsync(nonExistingIp);

            Assert.IsNull(actual);
        }

        private void InitializeDb()
        {
            if (!isDbInitialized)
            {
                var mongoDbInitializer = new MongoDbInitializer();
                mongoDbInitializer.Initialize();
                existingIpLocations = mongoDbInitializer.getIpLocations();
                isDbInitialized = true;
            }
        }
    }
}
