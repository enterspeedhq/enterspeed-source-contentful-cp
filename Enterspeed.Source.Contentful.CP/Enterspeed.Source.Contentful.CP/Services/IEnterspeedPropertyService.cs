using System.Collections.Generic;
using Contentful.Core.Models;
using Enterspeed.Source.Sdk.Api.Models.Properties;

namespace Enterspeed.Source.Contentful.CP.Services;

public interface IEnterspeedPropertyService
{
    IDictionary<string, IEnterspeedProperty> GetProperties(Entry<dynamic> content);
    IDictionary<string, IEnterspeedProperty> GetProperties(Asset asset);
}