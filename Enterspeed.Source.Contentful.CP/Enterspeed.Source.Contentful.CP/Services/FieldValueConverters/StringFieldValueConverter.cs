using Enterspeed.Source.Contentful.CP.Models;
using Enterspeed.Source.Sdk.Api.Models.Properties;

namespace Enterspeed.Source.Contentful.CP.Services.FieldValueConverters;

public class StringFieldValueConverter : IEnterspeedFieldValueConverter
{
    public bool IsConverter(ContentfulField field)
    {
        return field.Type == typeof(string);
    }

    public IEnterspeedProperty Convert(ContentfulField field)
    {
        return new StringEnterspeedProperty(field.Name, field.Value.ToString());
    }
}