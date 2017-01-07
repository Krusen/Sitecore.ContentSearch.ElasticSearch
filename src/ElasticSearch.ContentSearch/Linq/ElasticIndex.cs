using System;
using System.Collections.Generic;
using Sitecore.ContentSearch.Linq.Common;
using Sitecore.ContentSearch.Linq.Indexing;
using Sitecore.ContentSearch.Linq.Parsing;

namespace ElasticSearch.ContentSearch.Linq
{
    public class ElasticIndex<TItem> : Index<TItem, ElasticQuery>
    {
        protected override QueryMapper<ElasticQuery> QueryMapper { get; }
        protected override IQueryOptimizer QueryOptimizer { get; }
        protected override FieldNameTranslator FieldNameTranslator { get; }
        protected override IIndexValueFormatter ValueFormatter { get; }

        public override TResult Execute<TResult>(ElasticQuery query)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<TElement> FindElements<TElement>(ElasticQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
