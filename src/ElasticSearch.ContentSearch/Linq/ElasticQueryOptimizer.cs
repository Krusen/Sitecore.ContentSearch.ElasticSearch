using System;
using Sitecore.ContentSearch.Linq.Nodes;
using Sitecore.ContentSearch.Linq.Parsing;

namespace ElasticSearch.ContentSearch.Linq
{
    public class ElasticQueryOptimizer : QueryOptimizer<ElasticQueryOptimizerState>
    {
        protected override QueryNode Visit(QueryNode node, ElasticQueryOptimizerState state)
        {
            throw new NotImplementedException();
        }
    }
}
