using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Enterspeed.Source.Contentful.CP.Models;

public class ContentfulField
{
    public string Name { get; set; }
    public Type Type { get; set; }
    public object Value { get; set; }

    public ContentfulField(dynamic field)
    {
        if (field.Value.GetType() == typeof(JObject))
        {
            var test = JsonConvert.DeserializeObject<ContentfulResource>(field.Value.ToString());
        }
        else if (field.Value.GetType() == typeof(JArray))
        {
            var test = JsonConvert.DeserializeObject<ContentfulResource[]>(field.Value.ToString());
        }
        else
        {
            Name = field.Name;
            Value = field.Value.Value;
            Type = field.Value.Value.GetType();
        }
    }
}

//"sys": {
//    "type": "Link",
//    "linkType": "Asset",
//    "id": "2KIDwADCxQefPkHK3SQ8tf"
//}
//"sys": {
//    "type": "Link",
//    "linkType": "Entry",
//    "id": "2KIDwADCxQefPkHK3SQ8tf"
//}