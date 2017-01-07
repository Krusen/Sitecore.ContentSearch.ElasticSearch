using System;
using Sitecore.ContentSearch;

namespace ElasticSearch.ContentSearch
{
    public class ElasticIndexOperations : IIndexOperations
    {
        public void Update(IIndexable indexable, IProviderUpdateContext context, ProviderIndexConfiguration indexConfiguration)
        {
            throw new NotImplementedException();
        }

        public void Delete(IIndexable indexable, IProviderUpdateContext context)
        {
            throw new NotImplementedException();
        }

        public void Delete(IIndexableId id, IProviderUpdateContext context)
        {
            throw new NotImplementedException();
        }

        public void Delete(IIndexableUniqueId indexableUniqueId, IProviderUpdateContext context)
        {
            throw new NotImplementedException();
        }

        public void Add(IIndexable indexable, IProviderUpdateContext context, ProviderIndexConfiguration indexConfiguration)
        {
            throw new NotImplementedException();
        }
    }
}
