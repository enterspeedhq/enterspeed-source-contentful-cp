using Enterspeed.Source.Contentful.CP.Models;
using Enterspeed.Source.Sdk.Api.Models.Properties;

namespace Enterspeed.Source.Contentful.CP.Services.FieldValueConverters;

public class BooleanFieldValueConverter : IEnterspeedFieldValueConverter
{
    public bool IsConverter(ContentfulField field)
    {
        return field.Type == typeof(bool);
    }

    public IEnterspeedProperty Convert(ContentfulField field)
    {
        bool.TryParse(field.Value.ToString(), out var boolean);

        return new BooleanEnterspeedProperty(field.Name, boolean);
    }
}