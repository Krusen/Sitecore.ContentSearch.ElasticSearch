using System;
using System.Collections.Concurrent;
using Sitecore.ContentSearch;

namespace ElasticSearch.ContentSearch
{
    public class ElasticDocumentBuilder : AbstractDocumentBuilder<ConcurrentDictionary<string, object>>
    {
        public ElasticDocumentBuilder(IIndexable indexable, IProviderUpdateContext context) : base(indexable, context)
        {
        }

        public override void AddField(string fieldName, object fieldValue, bool append = false)
        {
            throw new NotImplementedException();
        }

        public override void AddField(IIndexableDataField field)
        {
            throw new NotImplementedException();
        }

        public override void AddBoost()
        {
            throw new NotImplementedException();
        }

        public override void AddComputedIndexFields()
        {
            throw new NotImplementedException();
        }
    }
}
