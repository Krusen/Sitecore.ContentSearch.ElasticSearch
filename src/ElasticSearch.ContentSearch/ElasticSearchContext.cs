using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq.Common;
using Sitecore.ContentSearch.Security;

namespace ElasticSearch.ContentSearch
{
    public class ElasticSearchContext : IProviderSearchContext
    {
        public ISearchIndex Index { get; }
        public bool ConvertQueryDatesToUtc { get; set; }
        public SearchSecurityOptions SecurityOptions { get; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TItem> GetQueryable<TItem>()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TItem> GetQueryable<TItem>(IExecutionContext executionContext)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TItem> GetQueryable<TItem>(params IExecutionContext[] executionContexts)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SearchIndexTerm> GetTermsByFieldName(string fieldName, string prefix)
        {
            throw new NotImplementedException();
        }
    }
}
