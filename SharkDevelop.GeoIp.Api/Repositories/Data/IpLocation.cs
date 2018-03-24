using MongoDB.Bson.Serialization.Attributes;

namespace SharkDevelop.GeoIp.Api.Repositories.Data
{
    public class IpLocation
    {
        [BsonId]
        public object _id { get; set; }
        public string Ip { get; set; }
        public string Country { get; set; }
    }
}