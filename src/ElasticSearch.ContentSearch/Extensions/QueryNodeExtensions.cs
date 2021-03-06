﻿using System;
using Sitecore.ContentSearch.Linq.Helpers;
using Sitecore.ContentSearch.Linq.Nodes;

namespace ElasticSearch.ContentSearch.Extensions
{
    internal static class QueryNodeExtensions
    {
        public static bool? GetBooleanValue(this BinaryNode node)
        {
            return QueryHelper.GetBooleanValue(node);
        }

        public static string GetStringValue(this BinaryNode node)
        {
            return QueryHelper.GetStringValue(node);
        }

        public static FieldNode GetFieldNode(this BinaryNode node)
        {
            return QueryHelper.GetFieldNode(node);
        }

        public static ConstantNode GetValueNode(this BinaryNode node, Type type)
        {
            return QueryHelper.GetValueNode(node, type);
        }

        public static ConstantNode GetValueNode<T>(this BinaryNode node)
        {
            return QueryHelper.GetValueNode<T>(node);
        }

        public static ConstantNode GetConstantNode(this BinaryNode node)
        {
            var constantNode = node.LeftNode as ConstantNode ?? node.RightNode as ConstantNode;
            if (constantNode == null)
            {
                throw new NotSupportedException(
                    $"No constant node in query node of type: '{node.GetType().FullName}'. Left: '{node.LeftNode.GetType().FullName}'.  Right: '{node.RightNode.GetType().FullName}'.");
            }
            return constantNode;
        }
    }
}
