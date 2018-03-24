using System.Threading.Tasks;

namespace SharkDevelop.GeoIp.Api.Validators
{
    public interface IIpValidator
    {
        bool IsIpValid(string ipAddress);
    }
}