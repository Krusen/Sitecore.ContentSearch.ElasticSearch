﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ElasticSearch.ContentSearch.Extensions;
using Nest;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Common;
using Sitecore.ContentSearch.Linq.Extensions;
using Sitecore.ContentSearch.Linq.Methods;
using Sitecore.ContentSearch.Linq.Nodes;
using Sitecore.ContentSearch.Linq.Parsing;

namespace ElasticSearch.ContentSearch.Linq
{
    public class ElasticQueryMapper : QueryMapper<ElasticQuery>
    {
        public ElasticIndexParameters Parameters { get; set; }

        protected readonly IFieldQueryTranslatorMap<IFieldQueryTranslator> FieldQueryTranslators;

        protected FieldNameTranslator FieldNameTranslator { get; set; }

        public ElasticQueryMapper(ElasticIndexParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            Parameters = parameters;
            ValueFormatter = parameters.ValueFormatter;
            FieldQueryTranslators = parameters.FieldQueryTranslators;
            FieldNameTranslator = parameters.FieldNameTranslator;
        }

        public override ElasticQuery MapQuery(IndexQuery query)
        {
            //throw new NotImplementedException();

            // TODO: Execution contexts
            var state = new ElasticQueryMapperState(Enumerable.Empty<IExecutionContext>());
            var mappedQuery = Visit(query.RootNode, state);

            // TODO: Execution contexts
            return new ElasticQuery(mappedQuery, state.FilterQuery, state.AdditionalQueryMethods,
                state.VirtualFieldProcessors, state.FacetQueries, Enumerable.Empty<IExecutionContext>());
        }

        protected virtual QueryBase Visit(QueryNode node, ElasticQueryMapperState state)
        {
            switch (node.NodeType)
            {
                case QueryNodeType.All:
                    StripAll((AllNode)node, state.AdditionalQueryMethods);
                    return Visit(((AllNode)node).SourceNode, state);

                case QueryNodeType.And:
                    return VisitAnd((AndNode)node, state);

                case QueryNodeType.Any:
                    StripAny((AnyNode)node, state.AdditionalQueryMethods);
                    return Visit(((AnyNode)node).SourceNode, state);

                case QueryNodeType.Between:
                    return VisitBetween((BetweenNode)node, state);

                case QueryNodeType.Cast:
                    StripCast((CastNode)node, state.AdditionalQueryMethods);
                    return Visit(((CastNode)node).SourceNode, state);

                case QueryNodeType.Contains:
                    return VisitContains((ContainsNode)node, state);

                case QueryNodeType.Count:
                    StripCount((CountNode)node, state.AdditionalQueryMethods);
                    return Visit(((CountNode)node).SourceNode, state);

                case QueryNodeType.ElementAt:
                    StripElementAt((ElementAtNode)node, state.AdditionalQueryMethods);
                    return Visit(((ElementAtNode)node).SourceNode, state);

                case QueryNodeType.EndsWith:
                    return VisitEndsWith((EndsWithNode)node, state);

                case QueryNodeType.Equal:
                    return VisitEqual((EqualNode)node, state);

                case QueryNodeType.Field:
                    return VisitField((FieldNode)node, state);

                case QueryNodeType.First:
                    StripFirst((FirstNode)node, state.AdditionalQueryMethods);
                    return Visit(((FirstNode)node).SourceNode, state);

                case QueryNodeType.GreaterThan:
                    return VisitGreaterThan((GreaterThanNode)node, state);

                case QueryNodeType.GreaterThanOrEqual:
                    return VisitGreaterThanOrEqual((GreaterThanOrEqualNode)node, state);

                case QueryNodeType.Last:
                    StripLast((LastNode)node, state.AdditionalQueryMethods);
                    return Visit(((LastNode)node).SourceNode, state);

                case QueryNodeType.LessThan:
                    return VisitLessThan((LessThanNode)node, state);

                case QueryNodeType.LessThanOrEqual:
                    return VisitLessThanOrEqual((LessThanOrEqualNode)node, state);

                case QueryNodeType.MatchAll:
                    return VisitMatchAll((MatchAllNode)node, state);

                case QueryNodeType.MatchNone:
                    return VisitMatchNone((MatchNoneNode)node, state);

                case QueryNodeType.Max:
                    StripMax((MaxNode)node, state.AdditionalQueryMethods);
                    return Visit(((MaxNode)node).SourceNode, state);

                case QueryNodeType.Min:
                    StripMin((MinNode)node, state.AdditionalQueryMethods);
                    return Visit(((MinNode)node).SourceNode, state);

                case QueryNodeType.Not:
                    return VisitNot((NotNode)node, state);

                case QueryNodeType.Or:
                    return VisitOr((OrNode)node, state);

                case QueryNodeType.OrderBy:
                    StripOrderBy((OrderByNode)node, state.AdditionalQueryMethods);
                    return Visit(((OrderByNode)node).SourceNode, state);

                case QueryNodeType.Select:
                    StripSelect((SelectNode)node, state.AdditionalQueryMethods);
                    return Visit(((SelectNode)node).SourceNode, state);

                case QueryNodeType.Single:
                    StripSingle((SingleNode)node, state.AdditionalQueryMethods);
                    return Visit(((SingleNode)node).SourceNode, state);

                case QueryNodeType.Skip:
                    StripSkip((SkipNode)node, state.AdditionalQueryMethods);
                    return Visit(((SkipNode)node).SourceNode, state);

                case QueryNodeType.StartsWith:
                    return VisitStartsWith((StartsWithNode)node, state);

                case QueryNodeType.Take:
                    StripTake((TakeNode)node, state.AdditionalQueryMethods);
                    return Visit(((TakeNode)node).SourceNode, state);

                case QueryNodeType.Where:
                    return VisitWhere((WhereNode)node, state);

                case QueryNodeType.Matches:
                    return VisitMatches((MatchesNode)node, state);

                case QueryNodeType.Filter:
                    state.FilterQuery = state.FilterQuery != null
                        ? state.FilterQuery && VisitFilter((FilterNode)node, state)
                        : VisitFilter((FilterNode)node, state);
                    return Visit(((FilterNode)node).SourceNode, state);

                case QueryNodeType.GetResults:
                    StripGetResults((GetResultsNode)node, state.AdditionalQueryMethods);
                    return Visit(((GetResultsNode)node).SourceNode, state);

                case QueryNodeType.GetFacets:
                    StripGetFacets((GetFacetsNode)node, state.AdditionalQueryMethods);
                    return Visit(((GetFacetsNode)node).SourceNode, state);

                case QueryNodeType.FacetOn:
                    StripFacetOn((FacetOnNode)node, state);
                    return Visit(((FacetOnNode)node).SourceNode, state);

                case QueryNodeType.FacetPivotOn:
                    StripFacetPivotOn((FacetPivotOnNode)node, state);
                    return Visit(((FacetPivotOnNode)node).SourceNode, state);

                case QueryNodeType.WildcardMatch:
                    return VisitWildcardMatch((WildcardMatchNode)node, state);

                case QueryNodeType.Like:
                    return VisitLike((LikeNode)node, state);

                case QueryNodeType.Join:
                    // TODO: Not sure if this is correct, copied from Solr
                    StripJoin((JoinNode)node, state);
                    return null;

                case QueryNodeType.GroupJoin:
                    // TODO: Not sure if this is correct, copied from Solr
                    StripGroupJoin((GroupJoinNode)node, state);
                    return null;

                case QueryNodeType.SelfJoin:
                    return VisitSelfJoin((SelfJoinNode)node, state);

                case QueryNodeType.SelectMany:
                    // TODO: Not sure if this is correct, copied from Solr
                    StripSelectMany((SelectManyNode)node, state);
                    return null;

                case QueryNodeType.InContext:
                    List<IExecutionContext> executionContexts = state.ExecutionContexts.ToList();
                    StripInContext((InContextNode)node, executionContexts);
                    state.ExecutionContexts = executionContexts;
                    return Visit(((InContextNode)node).SourceNode, state);

                case QueryNodeType.Union:
                    StripUnion((UnionNode)node, state);
                    return VisitUnion((UnionNode)node, state);

                // TODO: What about these? They don't appear to be implemented by Solr or Lucene
                //case QueryNodeType.Boost:
                //    break;
                //case QueryNodeType.Constant:
                //    break;
                //case QueryNodeType.Custom:
                //    break;
                //case QueryNodeType.NotEqual:
                //    break;
                //case QueryNodeType.Negate:
                //    break;

                default:
                    throw new NotSupportedException($"Unknown query node type: '{node.NodeType}'");
            }
        }

        // TODO: Actually go through these methods
        #region Strip methods

        protected virtual void StripAll(AllNode node, HashSet<QueryMethod> additionalQueryMethods)
        {
            additionalQueryMethods.Add(new AllMethod());
        }

        protected virtual void StripAny(AnyNode node, HashSet<QueryMethod> additionalQueryMethods)
        {
            additionalQueryMethods.Add(new AnyMethod());
        }

        protected virtual void StripCast(CastNode node, HashSet<QueryMethod> additionalQueryMethods)
        {
            additionalQueryMethods.Add(new CastMethod(node.TargetType));
        }

        protected virtual void StripCount(CountNode node, HashSet<QueryMethod> additionalQueryMethods)
        {
            additionalQueryMethods.Add(new CountMethod(node.IsLongCount));
        }

        protected virtual void StripElementAt(ElementAtNode node, HashSet<QueryMethod> additionalQueryMethods)
        {
            additionalQueryMethods.Add(new ElementAtMethod(node.Index, node.AllowDefaultValue));
        }

        protected virtual void StripFirst(FirstNode node, HashSet<QueryMethod> additionalQueryMethods)
        {
            additionalQueryMethods.Add(new FirstMethod(node.AllowDefaultValue));
        }

        protected virtual void StripMin(MinNode node, HashSet<QueryMethod> additionalQueryMethods)
        {
            additionalQueryMethods.Add(new MinMethod(node.AllowDefaultValue));
        }

        protected virtual void StripMax(MaxNode node, HashSet<QueryMethod> additionalQueryMethods)
        {
            additionalQueryMethods.Add(new MaxMethod(node.AllowDefaultValue));
        }

        protected virtual void StripLast(LastNode node, HashSet<QueryMethod> additionalQueryMethods)
        {
            additionalQueryMethods.Add(new LastMethod(node.AllowDefaultValue));
        }

        protected virtual void StripOrderBy(OrderByNode node, HashSet<QueryMethod> additionalQueryMethods)
        {
            string lowerInvariant = node.Field.ToLowerInvariant();
            additionalQueryMethods.Add(new OrderByMethod(lowerInvariant.Replace(" ", "_"), node.FieldType, node.SortDirection));
        }

        protected virtual void StripSingle(SingleNode node, HashSet<QueryMethod> additionalQueryMethods)
        {
            additionalQueryMethods.Add(new SingleMethod(node.AllowDefaultValue));
        }

        protected virtual void StripSkip(SkipNode node, HashSet<QueryMethod> additionalQueryMethods)
        {
            additionalQueryMethods.Add(new SkipMethod(node.Count));
        }

        protected virtual void StripTake(TakeNode node, HashSet<QueryMethod> additionalQueryMethods)
        {
            additionalQueryMethods.Add(new TakeMethod(node.Count));
        }

        protected virtual void StripSelect(SelectNode node, HashSet<QueryMethod> additionalQueryMethods)
        {
            additionalQueryMethods.Add(new SelectMethod(node.Lambda, node.FieldNames));
        }

        protected virtual void StripGetResults(GetResultsNode node, HashSet<QueryMethod> additionalQueryMethods)
        {
            additionalQueryMethods.Add(new GetResultsMethod(node.Options));
        }

        protected virtual void StripInContext(InContextNode node, List<IExecutionContext> executionContexts)
        {
            executionContexts.Add(node.ExecutionContext);
        }

        protected virtual void StripGetFacets(GetFacetsNode node, HashSet<QueryMethod> methods)
        {
            methods.Add(new GetFacetsMethod());
        }

        protected virtual void StripFacetOn(FacetOnNode node, ElasticQueryMapperState state)
        {
            state.FacetQueries.Add(new FacetQuery(node.Field, new[] { node.Field }, node.MinimumNumberOfDocuments, node.FilterValues));
        }

        protected virtual void StripFacetPivotOn(FacetPivotOnNode node, ElasticQueryMapperState state)
        {
            state.FacetQueries.Add(new FacetQuery(null, node.Fields, node.MinimumNumberOfDocuments, node.FilterValues));
        }

        protected virtual void StripJoin(JoinNode node, ElasticQueryMapperState state)
        {
            state.AdditionalQueryMethods.Add(new JoinMethod(node.GetOuterQueryable(), node.GetInnerQueryable(), node.OuterKey, node.InnerKey, node.OuterKeyExpression, node.InnerKeyExpression, node.SelectQuery, node.EqualityComparer));
        }

        protected virtual void StripGroupJoin(GroupJoinNode node, ElasticQueryMapperState state)
        {
            state.AdditionalQueryMethods.Add(new GroupJoinMethod(node.GetOuterQueryable(), node.GetInnerQueryable(), node.OuterKey, node.InnerKey, node.OuterKeyExpression, node.InnerKeyExpression, node.SelectQuery, node.EqualityComparer));
        }

        // TODO: Sitecore 8.2 only (UnionNode/UnionMethod)?
        protected virtual void StripUnion(UnionNode node, ElasticQueryMapperState state)
        {
            state.AdditionalQueryMethods.Add(new UnionMethod(node.GetOuterQueryable(), node.GetInnerQueryable()));
        }

        protected virtual void StripSelectMany(SelectManyNode node, ElasticQueryMapperState state)
        {
            state.AdditionalQueryMethods.Add(new SelectManyMethod(node.GetSourceQueryable(), node.CollectionSelectorExpression, node.ResultSelectorExpression));
        }

        #endregion

        #region Visit methods

        protected QueryBase VisitAnd(AndNode node, ElasticQueryMapperState state)
        {
            return Visit(node.LeftNode, state) && Visit(node.RightNode, state);
        }

        protected QueryBase VisitBetween(BetweenNode node, ElasticQueryMapperState state)
        {
            // TODO: Same as GreaterThan etc. - i.e. check type for date, number or string etc.
            var fieldName = FormatFieldName(node.Field);
            var includeLower = node.Inclusion == Inclusion.Both || node.Inclusion == Inclusion.Lower;
            var includeUpper = node.Inclusion == Inclusion.Both || node.Inclusion == Inclusion.Upper;

            // TODO: Allow null as one of parameters?

            // TODO: If number
            if (true)
            {
                var lowerNumber = double.Parse(node.From.ToString()); // TODO:
                var upperNumber = double.Parse(node.To.ToString()); // TODO:

                var query = new NumericRangeQuery
                {
                    Field = fieldName,
                    Boost = node.Boost
                };

                // TODO: Refactor this shit to something less shitty looking
                if (includeLower)
                {
                    query.GreaterThanOrEqualTo = lowerNumber;
                }
                else
                {
                    query.GreaterThan = lowerNumber;
                }

                if (includeUpper)
                {
                    query.LessThanOrEqualTo = upperNumber;
                }
                else
                {
                    query.LessThan = upperNumber;
                }
            }

            // TODO: If date
            // TODO: If term (string)

            throw new NotImplementedException();
        }

        // TODO: ProcessAsVirtualField? Solr does this, but not Lucene
        protected QueryBase VisitContains(ContainsNode node, ElasticQueryMapperState state)
        {
            // TODO: Move these 3 lines to separate method for reuse?
            var fieldName = GetFormattedFieldName(node);
            var valueNode = node.GetValueNode<string>();
            var value = ValueFormatter.FormatValueForIndexStorage(valueNode.Value, fieldName);

            // TODO: Use WildcardQuery if it's a keyword field, other wise if full-text field use MatchQuery?

            return new WildcardQuery
            {
                Field = fieldName,
                Value = "*" + value.ToStringOrEmpty() + "*",
                Boost = node.Boost
            };
        }

        protected QueryBase VisitEndsWith(EndsWithNode node, ElasticQueryMapperState state)
        {
            // TODO: Move these 3 lines to separate method for reuse?
            var fieldName = GetFormattedFieldName(node);
            var valueNode = node.GetValueNode<string>();
            var value = ValueFormatter.FormatValueForIndexStorage(valueNode.Value, fieldName);

            return new WildcardQuery
            {
                Field = fieldName,
                Value = "*" + value.ToStringOrEmpty(),
                Boost = node.Boost
            };
        }

        // TODO: ProcessAsVirtualField? Both Solr and Lucene do this
        // TODO: null value check (change to !ExistsQuery/MissingQuery then?)
        protected QueryBase VisitEqual(EqualNode node, ElasticQueryMapperState state)
        {
            // TODO: Move these 3 lines to separate method for reuse?
            var fieldName = GetFormattedFieldName(node);
            var valueNode = node.GetValueNode<string>();
            var value = ValueFormatter.FormatValueForIndexStorage(valueNode.Value, fieldName);

            // TODO: Maybe use Term if keyword field, otherwise MatchPhrase?
            // TODO: Match/MatchPhrase?
            /* Like the match query, the match_phrase query first analyzes the query string to produce a list of terms.
             * It then searches for all the terms, but keeps only documents that contain all of the search terms,
             * in the same positions relative to each other.
             */

            // TODO: Term query
            /* the term query looks in the inverted index for the exact term only; it won’t match any variants */

            return new TermQuery
            {
                Field = fieldName,
                Value = value,
                Boost = node.Boost
            };
        }

        // TODO: Handle actual bool field value? Or am I missing something? Solr and Lucene both just pass 'true'
        protected QueryBase VisitField(FieldNode node, ElasticQueryMapperState state)
        {
            if (node.FieldType != typeof(bool))
                throw new NotSupportedException($"The query node type '{node.NodeType}' is not supported in this context.");

            var fieldName = FormatFieldName(node.FieldKey);
            return new TermQuery { Field = fieldName, Value = true };
        }

        // TODO: ProcessAsVirtualField? Both Solr and Lucene do this
        protected QueryBase VisitGreaterThan(GreaterThanNode node, ElasticQueryMapperState state)
        {
            return GetSingleTermRangeQuery(node, RangeQueryPropertyNames.GreaterThan);
        }

        protected QueryBase VisitGreaterThanOrEqual(GreaterThanOrEqualNode node, ElasticQueryMapperState state)
        {
            return GetSingleTermRangeQuery(node, RangeQueryPropertyNames.GreaterThanOrEqualTo);
        }

        protected QueryBase VisitLessThan(LessThanNode node, ElasticQueryMapperState state)
        {
            return GetSingleTermRangeQuery(node, RangeQueryPropertyNames.LessThan);
        }

        protected QueryBase VisitLessThanOrEqual(LessThanOrEqualNode node, ElasticQueryMapperState state)
        {
            return GetSingleTermRangeQuery(node, RangeQueryPropertyNames.LessThanOrEqualTo);
        }

        protected QueryBase VisitMatchAll(MatchAllNode node, ElasticQueryMapperState state)
        {
            return new MatchAllQuery();
        }

        protected QueryBase VisitMatchNone(MatchNoneNode node, ElasticQueryMapperState state)
        {
            return new MatchNoneQuery();
        }

        protected QueryBase VisitNot(NotNode node, ElasticQueryMapperState state)
        {
            // TODO: Copied from Lucene - might need extra logic as in Solr
            return !Visit(node.Operand, state);

            // TODO: Solr implementation
            //AbstractSolrQuery abstractSolrQuery = this.Visit(node.Operand, state);
            //SolrQueryByField solrQueryByField = abstractSolrQuery as SolrQueryByField;
            //if (solrQueryByField != null && string.IsNullOrEmpty(solrQueryByField.FieldValue))
            //    return (AbstractSolrQuery)new SolrMultipleCriteriaQuery((IEnumerable<ISolrQuery>)new ISolrQuery[2]
            //    {
            //      (ISolrQuery) new SolrNotQuery((ISolrQuery) abstractSolrQuery),
            //      (ISolrQuery) new SolrHasValueQuery(solrQueryByField.FieldName)
            //    });

            //SolrNotQuery solrNotQuery = abstractSolrQuery as SolrNotQuery;

            //if (solrNotQuery != null)
            //    return (AbstractSolrQuery)solrNotQuery.Query;

            //return (AbstractSolrQuery)new SolrMultipleCriteriaQuery((IEnumerable<ISolrQuery>)new ISolrQuery[2]
            //{
            //    (ISolrQuery) new SolrNotQuery((ISolrQuery) abstractSolrQuery),
            //    (ISolrQuery) SolrQuery.All
            //});
        }

        protected QueryBase VisitOr(OrNode node, ElasticQueryMapperState state)
        {
            // TODO: NullOrEmpty check and maybe more
            var query1 = Visit(node.LeftNode, state);
            var query2 = Visit(node.RightNode, state);

            // TODO: This is the same as Union - not sure if that is correct
            // TODO: Need to check if this is correct, but it should be: https://www.elastic.co/guide/en/elasticsearch/guide/current/bool-query.html
            return query1 || query2;
        }

        // TODO: ProcessAsVirtualField? Solr does this, but not Lucene
        protected QueryBase VisitStartsWith(StartsWithNode node, ElasticQueryMapperState state)
        {
            // TODO: Move these 3 lines to separate method for reuse?
            var fieldName = GetFormattedFieldName(node);
            var valueNode = node.GetValueNode<string>();
            var value = ValueFormatter.FormatValueForIndexStorage(valueNode.Value, fieldName);

            return new PrefixQuery
            {
                Field = fieldName,
                Value = value.ToStringOrEmpty(), // TODO: StartsWith can only be done on strings (?) so it should actually already always be a string
                Boost = node.Boost
            };
        }

        protected QueryBase VisitWhere(WhereNode node, ElasticQueryMapperState state)
        {
            var query1 = Visit(node.PredicateNode, state);
            var query2 = Visit(node.SourceNode, state);

            if (query1 is IMatchAllQuery && query2 is IMatchAllQuery)
                return query1;

            // If one of them is not MatchAll, then return that one
            if (query1 is IMatchAllQuery || query2 is IMatchAllQuery)
                return query1 is IMatchAllQuery ? query2 : query1;

            return query1 && query2;
        }

        protected QueryBase VisitMatches(MatchesNode node, ElasticQueryMapperState state)
        {
            // TODO: Move these 3 lines to separate method for reuse?
            var fieldName = GetFormattedFieldName(node);
            var valueNode = node.GetValueNode<string>();
            var value = ValueFormatter.FormatValueForIndexStorage(valueNode.Value, fieldName);

            // TODO: RegexOptions - does Elastic support this?
            return new RegexpQuery
            {
                Field = fieldName,
                Value = value.ToStringOrEmpty(),
                Boost = node.Boost
            };
        }

        protected QueryBase VisitFilter(FilterNode node, ElasticQueryMapperState state)
        {
            // TODO: Both Solr and Lucene creates a new state from the ExecutionContexts - not sure why
            // var state2 = new LuceneQueryMapperState((IEnumerable<IExecutionContext>) state.ExecutionContexts);

            return Visit(node.PredicateNode, state);
        }

        protected QueryBase VisitWildcardMatch(WildcardMatchNode node, ElasticQueryMapperState state)
        {
            // TODO: Move these 3 lines to separate method for reuse?
            var fieldName = GetFormattedFieldName(node);
            var valueNode = node.GetValueNode<string>();
            var value = ValueFormatter.FormatValueForIndexStorage(valueNode.Value, fieldName);

            return new WildcardQuery
            {
                Field = fieldName,
                Value = value.ToStringOrEmpty(), // TODO: StartsWith can only be done on strings (?) so it should actually already always be a string
                Boost = node.Boost
            };
        }

        protected QueryBase VisitLike(LikeNode node, ElasticQueryMapperState state)
        {
            // TODO: Move these 3 lines to separate method for reuse?
            var fieldName = GetFormattedFieldName(node);
            var valueNode = node.GetValueNode<string>();
            var value = ValueFormatter.FormatValueForIndexStorage(valueNode.Value, fieldName);

            // TODO: Match/MatchPhrase?
            /* Like the match query, the match_phrase query first analyzes the query string to produce a list of terms.
             * It then searches for all the terms, but keeps only documents that contain all of the search terms,
             * in the same positions relative to each other.
             */
            var query = new MatchQuery
            {
                Field = fieldName,
                Query = value.ToStringOrEmpty(),
                Boost = node.Boost,

                // TODO: Not sure if this is the best way to handle slop/similarity.
                // Use EditDistance if Slop > 0, otherwise use Ratio if MinimumSimilarity > 0 and not default, otherwise Auto
                Fuzziness = node.Slop > 0
                    ? Fuzziness.EditDistance(node.Slop)
                    : node.MinimumSimilarity > 0 && node.MinimumSimilarity != 0.5f
                        ? Fuzziness.Ratio(node.MinimumSimilarity)
                        : Fuzziness.Auto
            };

            return query;
        }

        protected QueryBase VisitSelfJoin(SelfJoinNode node, ElasticQueryMapperState state)
        {
            throw new NotImplementedException();
        }

        protected QueryBase VisitUnion(UnionNode node, ElasticQueryMapperState state)
        {
            var query1 = Visit(node.InnerQuery, state);
            var query2 = Visit(node.OuterQuery, state);

            // TODO: Need to check if this is correct, but it should be: https://www.elastic.co/guide/en/elasticsearch/guide/current/bool-query.html
            return query1 || query2;
        }

        #endregion

        // TODO: Refactor so we can use for BetweenQuery as well?
        private QueryBase GetSingleTermRangeQuery(BinaryNode node, string propertyName)
        {
            var fieldName = GetFormattedFieldName(node);
            // TODO: Don't think we need to format this?
            //var valueNode = node.GetValueNode<string>();
            //var value = ValueFormatter.FormatValueForIndexStorage(valueNode.Value, fieldName);

            var valueType = node.GetConstantNode().Type; // TODO: This is a bit dodgy, might need testing.
            var value = node.GetValueNode<object>().Value;

            // TODO: Use this for type checking instead? Maybe store the types in a list and do DateTimeTypes.Contains(nodeType)
            //var valueNode = node.GetConstantNode(); // Add method parameter 'nodeType'
            //var isDate = valueNode.Type == typeof (DateTime) || valueNode.Type == typeof (DateTime?) ||
            //             valueNode.Type == typeof (DateTimeOffset) || valueNode.Type == typeof (DateTimeOffset?);

            // Number
            double number;
            if (double.TryParse(value.ToString(), out number))
            {
                var query = new NumericRangeQuery
                {
                    Field = fieldName,
                    Boost = node.Boost
                };

                return SetProperty(query, number, propertyName);
            }

            // Date
            var date = value as DateTime? ?? (value as DateTimeOffset?)?.UtcDateTime;
            if (date != null)
            {
                var query = new DateRangeQuery
                {
                    Field = fieldName,
                    Boost = node.Boost
                };

                // TODO: We might need to use RoundTo(). Could we maybe specify this on field configuration?
                return SetProperty(query, DateMath.Anchored(date.Value), propertyName);
            }

            // String
            var term = value as string;
            if (term != null)
            {
                var query = new TermRangeQuery
                {
                    Field = fieldName,
                    Boost = node.Boost
                };

                return SetProperty(query, term, propertyName);
            }

            // TODO: Handle or throw special exception for null string (when type is string but it is null)

            throw new NotSupportedException($"The query node type '{valueType}' is not supported in 'greater than' comparisons. Supported types: numeric, DateTime, DateTimeOffset and string");
        }

        // TODO: Rename
        private static QueryBase SetProperty(QueryBase query, object value, string propertyName)
        {
            var property = query.GetType().GetProperty(propertyName);
            property.SetValue(query, value);

            return query;
        }

        protected string GetFormattedFieldName(BinaryNode node)
        {
            return FormatFieldName(node.GetFieldNode().FieldKey);
        }
        protected string FormatFieldName(string fieldName)
        {
            return fieldName.ToLowerInvariant().Replace(" ", "_");
        }

        protected static class RangeQueryPropertyNames
        {
            public const string GreaterThan = "GreaterThan";
            public const string GreaterThanOrEqualTo = "GreaterThanOrEqualTo";
            public const string LessThan = "LessThan";
            public const string LessThanOrEqualTo = "LessThanOrEqualTo";
        }

        protected class ElasticQueryMapperState
        {
            public HashSet<QueryMethod> AdditionalQueryMethods { get; set; }

            public QueryBase FilterQuery { get; set; }

            public List<IFieldQueryTranslator> VirtualFieldProcessors { get; set; }

            public List<FacetQuery> FacetQueries { get; set; }

            public IEnumerable<IExecutionContext> ExecutionContexts { get; set; }

            public ElasticQueryMapperState(IEnumerable<IExecutionContext> executionContexts)
            {
                AdditionalQueryMethods = new HashSet<QueryMethod>();
                VirtualFieldProcessors = new List<IFieldQueryTranslator>();
                FacetQueries = new List<FacetQuery>();
                ExecutionContexts = executionContexts;
            }
        }
    }
}
