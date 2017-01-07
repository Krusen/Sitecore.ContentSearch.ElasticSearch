using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nest;
using Sitecore.ContentSearch.Linq.Common;
using Sitecore.ContentSearch.Linq.Methods;
using IQuery = Sitecore.ContentSearch.Linq.Common.IQuery;

namespace ElasticSearch.ContentSearch.Linq
{
    public class ElasticQuery : IQuery
    {
        public QueryContainer Query { get; protected set; }
        public QueryContainer Filter { get; protected set; }
        public List<QueryMethod> Methods { get; }
        public List<IFieldQueryTranslator> VirtualFieldProcessors { get; }
        public List<FacetQuery> FacetQueries { get; }
        public List<IExecutionContext> ExecutionContexts { get; }

        public ElasticQuery(QueryContainer query, QueryContainer filter, IEnumerable<QueryMethod> methods, IEnumerable<IFieldQueryTranslator> virtualFieldProcessors, IEnumerable<FacetQuery> facetQueries, IEnumerable<IExecutionContext> executionContexts)
        {
            Query = query;
            Filter = filter;
            Methods = methods.ToList();
            VirtualFieldProcessors = virtualFieldProcessors.ToList();
            FacetQueries = facetQueries.ToList();
            ExecutionContexts = executionContexts.ToList();
        }

        public void WriteTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
