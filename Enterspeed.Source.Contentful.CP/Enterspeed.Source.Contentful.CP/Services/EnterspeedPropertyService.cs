using Enterspeed.Source.Sdk.Api.Models.Properties;
using System.Collections.Generic;
using System.Linq;
using Contentful.Core.Models;
using Enterspeed.Source.Contentful.CP.Constants;
using Enterspeed.Source.Contentful.CP.Models;
using Enterspeed.Source.Contentful.CP.Services.FieldValueConverters;

namespace Enterspeed.Source.Contentful.CP.Services;

public class EnterspeedPropertyService : IEnterspeedPropertyService
{
    private const string MetaData = "metaData";
    private const string Title = "title";
    private const string Description = "description";
    private const string File = "file";
    private readonly IEnumerable<IEnterspeedFieldValueConverter> _fieldValueConverters;

    public EnterspeedPropertyService(IEnumerable<IEnterspeedFieldValueConverter> fieldValueConverters)
    {
        _fieldValueConverters = fieldValueConverters;
    }

    public IDictionary<string, IEnterspeedProperty> GetProperties(Entry<dynamic> content)
    {
        var properties = new Dictionary<string, IEnterspeedProperty>();
        foreach (var field in content.Fields)
        {
            var contentfulField = new ContentfulField(field);

            var converter = _fieldValueConverters.FirstOrDefault(x => x.IsConverter(contentfulField));

            var value = converter?.Convert(contentfulField);

            if (value == null)
            {
                continue;
            }

            properties.Add(value.Name, value);
        }

        properties.Add(MetaData, CreateMetaData(content));

        return properties;
    }

    public IDictionary<string, IEnterspeedProperty> GetProperties(Asset asset)
    {
        var properties = new Dictionary<string, IEnterspeedProperty>
        {
            { Title, new StringEnterspeedProperty(Title, asset.Title) },
            { Description, new StringEnterspeedProperty(Description, asset.Description) },
            { File, CreateFileData(asset.File) },
            { MetaData, CreateMetaData(asset) }
        };

        return properties;
    }

    private static IEnterspeedProperty CreateMetaData(Entry<dynamic> content)
    {
        var metaData = new Dictionary<string, IEnterspeedProperty>
        {
            ["locale"] = new StringEnterspeedProperty("locale", content.SystemProperties.Locale),
            ["type"] = new StringEnterspeedProperty("type", content.SystemProperties.Type),
            ["contentType"] = new StringEnterspeedProperty("contentType", content.SystemProperties.ContentType.SystemProperties.Id),
            ["environment"] = new StringEnterspeedProperty("environment", content.SystemProperties.Environment.SystemProperties.Id),
            ["createDate"] = new StringEnterspeedProperty("createDate", content.SystemProperties.CreatedAt?.ToString("yyyy-MM-ddTHH:mm:ss")),
            ["updateDate"] = new StringEnterspeedProperty("updateDate", content.SystemProperties.UpdatedAt?.ToString("yyyy-MM-ddTHH:mm:ss"))
        };

        return new ObjectEnterspeedProperty(MetaData, metaData);
    }

    private static IEnterspeedProperty CreateFileData(File file)
    {
        var fileData = new Dictionary<string, IEnterspeedProperty>
        {
            ["fileName"] = new StringEnterspeedProperty("fileName", file.FileName),
            ["url"] = new StringEnterspeedProperty("url", file.Url),
            ["contentType"] = new StringEnterspeedProperty("contentType", file.ContentType),
            ["size"] = new NumberEnterspeedProperty("environment", file.Details.Size)
        };

        if (file.Details.Image is not null)
        {
            fileData.Add("width", new NumberEnterspeedProperty("width", file.Details.Image.Width));
            fileData.Add("height", new NumberEnterspeedProperty("height", file.Details.Image.Height));
        }

        return new ObjectEnterspeedProperty("file", fileData);
    }

    private static IEnterspeedProperty CreateMetaData(Asset content)
    {
        var metaData = new Dictionary<string, IEnterspeedProperty>
        {
            ["locale"] = new StringEnterspeedProperty("locale", content.SystemProperties.Locale),
            ["type"] = new StringEnterspeedProperty("type", content.SystemProperties.Type),
            ["contentType"] = new StringEnterspeedProperty("contentType", WebhooksConstants.Types.Asset),
            ["environment"] = new StringEnterspeedProperty("environment", content.SystemProperties.Environment.SystemProperties.Id),
            ["createDate"] = new StringEnterspeedProperty("createDate", content.SystemProperties.CreatedAt?.ToString("yyyy-MM-ddTHH:mm:ss")),
            ["updateDate"] = new StringEnterspeedProperty("updateDate", content.SystemProperties.UpdatedAt?.ToString("yyyy-MM-ddTHH:mm:ss"))
        };

        return new ObjectEnterspeedProperty(MetaData, metaData);
    }
}