using Enterspeed.Source.Contentful.CP.Models;

namespace Enterspeed.Source.Contentful.CP.Factories;

public interface IContentfulFieldFactory
{
    ContentfulField Create(dynamic field);
}