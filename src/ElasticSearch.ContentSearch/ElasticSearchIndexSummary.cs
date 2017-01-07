using System;
using System.Collections.Generic;
using Sitecore.ContentSearch;

namespace ElasticSearch.ContentSearch
{
    public class ElasticSearchIndexSummary : ISearchIndexSummary
    {
        public long NumberOfDocuments { get; }
        public bool IsOptimized { get; }
        public bool HasDeletions { get; }
        public bool IsHealthy { get; }
        public DateTime LastUpdated { get; set; }
        public int NumberOfFields { get; }
        public long NumberOfTerms { get; }
        public bool IsClean { get; }
        public string Directory { get; }
        public bool IsMissingSegment { get; }
        public int NumberOfBadSegments { get; }
        public bool OutOfDateIndex { get; }
        public IDictionary<string, string> UserData { get; }
        public long? LastUpdatedTimestamp { get; set; }
    }
}
