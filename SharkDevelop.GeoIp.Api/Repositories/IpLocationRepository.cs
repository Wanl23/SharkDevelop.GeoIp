using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SharkDevelop.GeoIp.Api.Repositories.Data;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace SharkDevelop.GeoIp.Api.Repositories
{
    public class IpLocationRepository : IIpLocationRepository
    {
        private IMongoCollection<BsonDocument> _collection;

        public IpLocationRepository()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["mongoDb"].ConnectionString;
            var client = new MongoClient(connectionString);
            var geoLocationDb = client.GetDatabase("GeoLocation");
            _collection = geoLocationDb.GetCollection<BsonDocument>("IpLocations");
        }

        public async Task<string> GetCountryNameAsync(string ipAddress)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Ip", ipAddress);
            var cursor = await _collection.FindAsync(filter);
            BsonDocument document = cursor.ToList().SingleOrDefault();
            if (document != null)
            {
                return BsonSerializer.Deserialize<IpLocation>(document).Country;
            }
            return null;
        }
    }
}
