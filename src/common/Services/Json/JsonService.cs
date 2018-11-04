using System;
using System.Collections.Generic;
using System.Text;

namespace common.Services.Json
{
    public class JsonService : IJsonService
    {
        public T Deerialize<T>(string serializedObject)
            => Newtonsoft.Json.JsonConvert.DeserializeObject<T>(serializedObject);

        public string Serialize<T>(T @object)
            => Newtonsoft.Json.JsonConvert.SerializeObject(@object);
    }
}
