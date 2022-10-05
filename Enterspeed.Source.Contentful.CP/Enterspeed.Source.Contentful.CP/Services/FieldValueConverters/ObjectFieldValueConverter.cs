using System.Collections.Generic;
using Enterspeed.Source.Contentful.CP.Models;
using Enterspeed.Source.Sdk.Api.Models.Properties;

namespace Enterspeed.Source.Contentful.CP.Services.FieldValueConverters;

public class ObjectFieldValueConverter : IEnterspeedFieldValueConverter
{
    public bool IsConverter(ContentfulField field)
    {
        return field.Type == typeof(ContentfulResource);
    }

    public IEnterspeedProperty Convert(ContentfulField field)
    {
        var value = ((ContentfulObjectField)field).GetValue();
        var properties = new Dictionary<string, IEnterspeedProperty>
        {
            ["id"] = new StringEnterspeedProperty("id", value.SystemProperties.Id),
            ["type"] = new StringEnterspeedProperty("type", value.SystemProperties.Type),
            ["linkType"] = new StringEnterspeedProperty("linkType", value.SystemProperties.LinkType)
        };

        return new ObjectEnterspeedProperty(field.Name, properties);
    }
}