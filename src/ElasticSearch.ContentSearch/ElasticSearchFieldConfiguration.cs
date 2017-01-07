using System;
using Sitecore.ContentSearch;

namespace ElasticSearch.ContentSearch
{
    public class ElasticSearchFieldConfiguration : AbstractSearchFieldConfiguration, ICloneable
    {
        // TODO: Only implemented with Solr
        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
