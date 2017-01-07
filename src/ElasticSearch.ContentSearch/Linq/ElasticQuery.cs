using System;
using System.Collections.Generic;
using System.IO;
using Sitecore.ContentSearch.Linq.Common;
using Sitecore.ContentSearch.Linq.Methods;

namespace ElasticSearch.ContentSearch.Linq
{
    public class ElasticQuery : IQuery
    {
        public List<QueryMethod> Methods { get; }
        public List<IFieldQueryTranslator> VirtualFieldProcessors { get; }
        public List<FacetQuery> FacetQueries { get; }
        public List<IExecutionContext> ExecutionContexts { get; }

        public void WriteTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
