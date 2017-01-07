using System;
using System.Collections.Generic;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq.Common;

namespace ElasticSearch.ContentSearch
{
    public class ElasticDocumentPropertyMapper : DefaultDocumentMapper<Dictionary<string, object>>
    {
        [Obsolete]
        protected override void ReadDocumentFields<TElement>(Dictionary<string, object> document, IEnumerable<string> fieldNames, DocumentTypeMapInfo documentTypeMapInfo,
            IEnumerable<IFieldQueryTranslator> virtualFieldProcessors, TElement result)
        {
        }

        protected override IEnumerable<string> GetDocumentFieldNames(Dictionary<string, object> document)
        {
            // TODO: Copied from SolrDocumentPropertyMapper (returns null in DefaultLuceneDocumentTypeMapper)
            return document.Keys;
        }

        protected override IDictionary<string, object> ReadDocumentFields(Dictionary<string, object> document, IEnumerable<string> fieldNames, IEnumerable<IFieldQueryTranslator> virtualFieldProcessors)
        {
            throw new NotImplementedException();
        }
    }
}
