using System.Collections.Generic;
using Contentful.Core.Models;
using Enterspeed.Source.Contentful.CP.Constants;
using Enterspeed.Source.Contentful.CP.Services;
using Enterspeed.Source.Sdk.Api.Models;
using Enterspeed.Source.Sdk.Api.Models.Properties;

namespace Enterspeed.Source.Contentful.CP.Models;

public class EnterspeedEntity : IEnterspeedEntity
{
    public EnterspeedEntity(Entry<dynamic> entry, IEnterspeedPropertyService enterspeedPropertyService, IEntityIdentityService entityIdentityService)
    {
        Id = entityIdentityService.GetId(entry);
        Type = entry.SystemProperties.ContentType.SystemProperties.Id;
        Properties = enterspeedPropertyService.GetProperties(entry);
    }

    public EnterspeedEntity(Asset asset, IEnterspeedPropertyService enterspeedPropertyService, IEntityIdentityService entityIdentityService)
    {
        Id = entityIdentityService.GetId(asset);
        Type = WebhooksConstants.Types.Asset.ToLower();
        Properties = enterspeedPropertyService.GetProperties(asset);
    }

    public string Id { get; }
    public string Type { get; }
    public string Url => null;
    public string[] Redirects => null;
    public string ParentId { get; }
    public IDictionary<string, IEnterspeedProperty> Properties { get; }
}