using SharkDevelop.GeoIp.Api.Repositories;
using SharkDevelop.GeoIp.Api.Validators;
using System.Threading.Tasks;
using System.Web.Http;

namespace SharkDevelop.GeoIp.Api.Controllers
{
    public class LocatorController : ApiController
    {
        private readonly IIpLocationRepository _ipLocationRepository;
        private readonly IIpValidator _ipValidator;

        public LocatorController()
        {
            _ipLocationRepository = new IpLocationRepository();
            _ipValidator = new IpValidator();
        }

        public LocatorController(IIpLocationRepository ipLocationRepository, IIpValidator ipValidator)
        {
            _ipLocationRepository = ipLocationRepository;
            _ipValidator = ipValidator;
        }

        public async Task<IHttpActionResult> LocateCountryAsync(string ipAddress)
        {
            _ipValidator.IsIpValidAsync(ipAddress);

            var country = await _ipLocationRepository.GetCountryNameAsync(ipAddress);
            if (string.IsNullOrWhiteSpace(country))
            {
                return NotFound();
            }

            return Ok(country);
        }
    }
}
