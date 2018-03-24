using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharkDevelop.GeoIp.Api.Validators;

namespace SharkDevelop.GeoIp.Unit.Api.Tests.Validaors
{
    [TestClass]
    public class IpValidatorTest
    {
        private IpValidator _target;

        [TestInitialize]
        public void Setup()
        {
            _target = new IpValidator();
        }
        
        [TestMethod]
        public void GivenValidIp_WhenIsIpValid_ThenReturnTrue()
        {
            var validIp = "192.168.1.1";

            var actual = _target.IsIpValid(validIp);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void GivenNull_WhenIsIpValid_ThenReturnFalse()
        {
            var actual = _target.IsIpValid(null);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void GivenEmptyString_WhenIsIpValid_ThenReturnFalse()
        {
            var actual = _target.IsIpValid(string.Empty);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void GivenWhiteSpace_WhenIsIpValid_ThenReturnFalse()
        {
            var actual = _target.IsIpValid(" ");

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void GivenIpStartWithZero_WhenIsIpValid_ThenReturnFalse()
        {
            var actual = _target.IsIpValid("092.168.1.1");

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void GivenMalformedId_WhenIsIpValid_ThenReturnFalse()
        {
            var actual = _target.IsIpValid("192.168.0");

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void GivenIpWithText_WhenIsIpValid_ThenReturnFalse()
        {
            var actual = _target.IsIpValid("192.xft.1.1");

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void GivenIpWithextraNumbers_WhenIsIpValid_ThenReturnFalse()
        {
            var actual = _target.IsIpValid("192.168.1.1009");

            Assert.IsFalse(actual);
        }
    }
}
