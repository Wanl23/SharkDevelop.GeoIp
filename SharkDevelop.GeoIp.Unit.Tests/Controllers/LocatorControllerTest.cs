using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharkDevelop.GeoIp.Api.Controllers;
using SharkDevelop.GeoIp.Api.Repositories;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Moq;
using SharkDevelop.GeoIp.Api.Validators;

namespace SharkDevelop.GeoIp.Unit.Api.Tests
{
    [TestClass]
    public class LocatorControllerTest
    {
        private LocatorController _target;

        private Mock<IIpLocationRepository> _ipLocationRepositoryMock;
        private Mock<IIpValidator> _ipValidator;

        [TestInitialize]
        public void Setup()
        {
            _ipLocationRepositoryMock = new Mock<IIpLocationRepository>();
            _ipValidator = new Mock<IIpValidator>();
            _target = new LocatorController(_ipLocationRepositoryMock.Object, _ipValidator.Object);

        }

        [TestMethod]
        public async Task GevenInvalidIpAddress_WhenLocateCountry_ThenReturnBadRequestResult()
        {
            var invalidIp = "invalidIp";
            _ipValidator.Setup(x => x.IsIpValidAsync(invalidIp)).ReturnsAsync(false);

            var result = await _target.LocateCountryAsync(invalidIp);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task GevenExistingIpAddress_WhenLocateCountry_ThenReturnOk()
        {
            var existingIp = "128.168.1.1";
            _ipLocationRepositoryMock.Setup(x => x.GetCountryNameAsync(existingIp)).ReturnsAsync("Russia");

            var result = await _target.LocateCountryAsync(existingIp);

            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<string>));
        }

        [TestMethod]
        public async Task GevenExistingIpAddress_WhenLocateCountry_ThenReturnCountry()
        {
            var existingIp = "128.168.1.1";
            var expectedCountry = "Russia";
            _ipLocationRepositoryMock.Setup(x => x.GetCountryNameAsync(existingIp)).ReturnsAsync(expectedCountry);

            var actual = (OkNegotiatedContentResult<string>) await _target.LocateCountryAsync(existingIp);

            Assert.AreEqual(expectedCountry, actual.Content);
        }

        [TestMethod]
        public async Task GevenNotExistingIpAddress_WhenLocateCountry_ThenReturnNotFound()
        {
            var notExistingIp = "128.168.1.1";
            string notExistingCountry = null;
            _ipLocationRepositoryMock.Setup(x => x.GetCountryNameAsync(notExistingIp)).ReturnsAsync(notExistingCountry);

            var result = await _target.LocateCountryAsync(notExistingIp);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
