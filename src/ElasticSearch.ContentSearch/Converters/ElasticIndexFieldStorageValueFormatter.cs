using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.ContentSearch.Converters;

namespace ElasticSearch.ContentSearch.Converters
{
    public class ElasticIndexFieldStorageValueFormatter : IndexFieldStorageValueFormatter
    {
        public override object FormatValueForIndexStorage(object value, string fieldName)
        {
            throw new NotImplementedException();
        }
    }
}
