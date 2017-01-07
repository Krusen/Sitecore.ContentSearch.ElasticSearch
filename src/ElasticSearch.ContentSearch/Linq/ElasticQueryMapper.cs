using System;
using Sitecore.ContentSearch.Linq.Parsing;

namespace ElasticSearch.ContentSearch.Linq
{
    public class ElasticQueryMapper : QueryMapper<ElasticQuery>
    {
        public override ElasticQuery MapQuery(IndexQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
