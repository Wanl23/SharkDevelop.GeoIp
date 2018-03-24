using System.Threading.Tasks;

namespace SharkDevelop.GeoIp.Api.Repositories
{
    public interface IIpLocationRepository
    {
        Task<string> GetCountryNameAsync(string ipAddress);
    }
}
