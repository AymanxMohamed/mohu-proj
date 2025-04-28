using System.Text.Json;
using Newtonsoft.Json;

namespace MOHU.Integration.WebApi.Common.Extensions;

public static class JsonElementExtensions
{
    public static T? ToObject<T>(this JsonElement jsonElement)
    {
        var jsonString = jsonElement.GetRawText();
        return JsonConvert.DeserializeObject<T>(jsonString);
    }
}
