using System;
using System.Collections.Generic;
using System.Threading;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Maintenance;
using Sitecore.ContentSearch.Maintenance.Strategies;
using Sitecore.ContentSearch.Security;
using Sitecore.ContentSearch.Sharding;

namespace ElasticSearch.ContentSearch
{
    public class ElasticSearchIndex : AbstractSearchIndex
    {
        public override string Name { get; }
        public override ISearchIndexSummary Summary { get; }
        public override ISearchIndexSchema Schema { get; }
        public override IIndexPropertyStore PropertyStore { get; set; }
        public override AbstractFieldNameTranslator FieldNameTranslator { get; set; }
        public override ProviderIndexConfiguration Configuration { get; set; }
        public override IIndexOperations Operations { get; }
        public override bool IsSharded { get; }
        public override IShardingStrategy ShardingStrategy { get; set; }
        public override IShardFactory ShardFactory { get; }
        public override IEnumerable<Shard> Shards { get; }

        public override void AddStrategy(IIndexUpdateStrategy strategy)
        {
            throw new NotImplementedException();
        }

        public override void Rebuild()
        {
            throw new NotImplementedException();
        }

        public override void Rebuild(IndexingOptions indexingOptions)
        {
            throw new NotImplementedException();
        }

        public override void Refresh(IIndexable indexableStartingPoint)
        {
            throw new NotImplementedException();
        }

        public override void Refresh(IIndexable indexableStartingPoint, IndexingOptions indexingOptions)
        {
            throw new NotImplementedException();
        }

        public override void Update(IIndexableUniqueId indexableUniqueId)
        {
            throw new NotImplementedException();
        }

        public override void Update(IIndexableUniqueId indexableUniqueId, IndexingOptions indexingOptions)
        {
            throw new NotImplementedException();
        }

        public override void Update(IEnumerable<IIndexableUniqueId> indexableUniqueIds)
        {
            throw new NotImplementedException();
        }

        public override void Update(IEnumerable<IIndexableUniqueId> indexableUniqueIds, IndexingOptions indexingOptions)
        {
            throw new NotImplementedException();
        }

        public override void Delete(IIndexableId indexableId)
        {
            throw new NotImplementedException();
        }

        public override void Delete(IIndexableId indexableId, IndexingOptions indexingOptions)
        {
            throw new NotImplementedException();
        }

        public override void Delete(IIndexableUniqueId indexableUniqueId)
        {
            throw new NotImplementedException();
        }

        public override void Delete(IIndexableUniqueId indexableUniqueId, IndexingOptions indexingOptions)
        {
            throw new NotImplementedException();
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override IProviderUpdateContext CreateUpdateContext()
        {
            throw new NotImplementedException();
        }

        public override IProviderDeleteContext CreateDeleteContext()
        {
            throw new NotImplementedException();
        }

        public override IProviderSearchContext CreateSearchContext(SearchSecurityOptions options = SearchSecurityOptions.Default)
        {
            throw new NotImplementedException();
        }

        protected override void PerformRebuild(IndexingOptions indexingOptions, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override void PerformRefresh(IIndexable indexableStartingPoint, IndexingOptions indexingOptions,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
