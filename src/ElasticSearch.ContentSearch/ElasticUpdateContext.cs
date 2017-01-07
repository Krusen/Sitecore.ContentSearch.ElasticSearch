using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq.Common;
using Sitecore.ContentSearch.Sharding;

namespace ElasticSearch.ContentSearch
{
    public class ElasticUpdateContext : IProviderUpdateContext
    {
        public bool IsParallel { get; }
        public ParallelOptions ParallelOptions { get; }
        public ISearchIndex Index { get; }
        public ICommitPolicyExecutor CommitPolicyExecutor { get; }
        public IEnumerable<Shard> ShardsWithPendingChanges { get; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Optimize()
        {
            throw new NotImplementedException();
        }

        public void AddDocument(object itemToAdd, IExecutionContext executionContext)
        {
            throw new NotImplementedException();
        }

        public void AddDocument(object itemToAdd, params IExecutionContext[] executionContexts)
        {
            throw new NotImplementedException();
        }

        public void UpdateDocument(object itemToUpdate, object criteriaForUpdate, IExecutionContext executionContext)
        {
            throw new NotImplementedException();
        }

        public void UpdateDocument(object itemToUpdate, object criteriaForUpdate, params IExecutionContext[] executionContexts)
        {
            throw new NotImplementedException();
        }

        public void Delete(IIndexableUniqueId id)
        {
            throw new NotImplementedException();
        }

        public void Delete(IIndexableId id)
        {
            throw new NotImplementedException();
        }
    }
}
