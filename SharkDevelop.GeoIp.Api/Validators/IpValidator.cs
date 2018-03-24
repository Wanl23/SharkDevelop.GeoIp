using System.Text.RegularExpressions;

namespace SharkDevelop.GeoIp.Api.Validators
{
    public class IpValidator : IIpValidator
    {
        public bool IsIpValid(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress)) return false;
            Regex regex = new Regex(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$");
            var match = regex.Match(ipAddress);

            return match.Success;
        }
    }
}