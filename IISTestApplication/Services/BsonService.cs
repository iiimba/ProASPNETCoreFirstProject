using IISTestApplication.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.IO;

namespace IISTestApplication.Services
{
    public class BsonService : IBsonService
    {
        public string ToBson<T>(T value)
        {
            using (var ms = new MemoryStream())
            using (var datawriter = new BsonDataWriter(ms))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(datawriter, value);

                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public T FromBson<T>(string base64data)
        {
            var data = Convert.FromBase64String(base64data);

            using (var ms = new MemoryStream(data))
            using (var reader = new BsonDataReader(ms))
            {
                var serializer = new JsonSerializer();

                return serializer.Deserialize<T>(reader);
            }
        }
    }
}
