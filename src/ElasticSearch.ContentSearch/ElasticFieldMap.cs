using System;
using System.Collections.Generic;
using System.Xml;
using Sitecore.ContentSearch;

namespace ElasticSearch.ContentSearch
{
    public class ElasticFieldMap : IFieldMap, ISearchIndexInitializable
    {
        public AbstractSearchFieldConfiguration GetFieldConfiguration(string fieldName)
        {
            throw new NotImplementedException();
        }

        public AbstractSearchFieldConfiguration GetFieldConfiguration(Type type)
        {
            throw new NotImplementedException();
        }

        public void AddTypeMatch(XmlNode configNode)
        {
            throw new NotImplementedException();
        }

        public void AddTypeMatch(string typeName, Type settingType, IDictionary<string, string> attributes, XmlNode configNode)
        {
            throw new NotImplementedException();
        }

        public void AddFieldByFieldName(XmlNode configNode)
        {
            throw new NotImplementedException();
        }

        public void AddFieldByFieldName(string fieldName, Type settingType, IDictionary<string, string> attributes, XmlNode configNode)
        {
            throw new NotImplementedException();
        }

        public void AddFieldByFieldTypeName(XmlNode configNode)
        {
            throw new NotImplementedException();
        }

        public void AddFieldByFieldTypeName(Type settingType, IEnumerable<string> fieldTypeNames, IDictionary<string, string> attributes, XmlNode configNode)
        {
            throw new NotImplementedException();
        }

        public AbstractSearchFieldConfiguration GetFieldConfiguration(IIndexableDataField field)
        {
            throw new NotImplementedException();
        }

        public AbstractSearchFieldConfiguration GetFieldConfiguration(IIndexableDataField field, Func<AbstractSearchFieldConfiguration, bool> fieldVisitorFunc)
        {
            throw new NotImplementedException();
        }

        public AbstractSearchFieldConfiguration GetFieldConfigurationByFieldTypeName(string fieldTypeName)
        {
            throw new NotImplementedException();
        }

        public void Initialize(ISearchIndex searchIndex)
        {
            throw new NotImplementedException();
        }
    }
}
