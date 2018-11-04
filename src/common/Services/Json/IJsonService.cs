using System;
using System.Collections.Generic;
using System.Text;

namespace common.Services.Json
{
    public interface IJsonService : ICommonService
    {
        string Serialize<T>(T @object);

        T Deerialize<T>(string serializedObject);
    }
}
