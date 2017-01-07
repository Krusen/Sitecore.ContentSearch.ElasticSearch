using System;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq.Common;
using Sitecore.ContentSearch.Linq.Parsing;

namespace ElasticSearch.ContentSearch.Linq
{
    public class ElasticIndexParameters : IIndexParameters
    {
        public IIndexValueFormatter ValueFormatter { get; }
        public IFieldQueryTranslatorMap<IFieldQueryTranslator> FieldQueryTranslators { get; }
        public FieldNameTranslator FieldNameTranslator { get; }
        public IExecutionContext[] ExecutionContexts { get; }
        public IFieldMapReaders FieldMap { get; protected set; }

        public ElasticIndexParameters(IIndexValueFormatter valueFormatter, IFieldQueryTranslatorMap<IFieldQueryTranslator> fieldQueryTranslators, FieldNameTranslator fieldNameTranslator, IExecutionContext executionContext)
            : this (valueFormatter, fieldQueryTranslators, fieldNameTranslator, executionContext == null ? null : new[] { executionContext }, null)
        {
        }

        public ElasticIndexParameters(IIndexValueFormatter valueFormatter, IFieldQueryTranslatorMap<IFieldQueryTranslator> fieldQueryTranslators, FieldNameTranslator fieldNameTranslator, IExecutionContext[] executionContexts)
            : this(valueFormatter, fieldQueryTranslators, fieldNameTranslator, executionContexts, null)
        {
        }

        public ElasticIndexParameters(IIndexValueFormatter valueFormatter, IFieldQueryTranslatorMap<IFieldQueryTranslator> fieldQueryTranslators, FieldNameTranslator fieldNameTranslator, IExecutionContext[] executionContexts, IFieldMapReaders fieldMap)
        {
            if (valueFormatter == null) throw new ArgumentNullException(nameof(valueFormatter));
            if (fieldQueryTranslators == null) throw new ArgumentNullException(nameof(fieldQueryTranslators));
            if (fieldNameTranslator == null) throw new ArgumentNullException(nameof(fieldNameTranslator));

            ValueFormatter = valueFormatter;
            FieldQueryTranslators = fieldQueryTranslators;
            FieldNameTranslator = fieldNameTranslator;
            ExecutionContexts = executionContexts;
            FieldMap = fieldMap;
        }
    }
}
