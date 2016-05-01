using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Baseline;
using Marten.Linq.Handlers;
using Marten.Schema;

namespace Marten.Linq
{
    public partial class MartenExpressionParser
    {
        public static readonly string CONTAINS = nameof(string.Contains);
        public static readonly string STARTS_WITH = nameof(string.StartsWith);
        public static readonly string ENDS_WITH = nameof(string.EndsWith);

        private static readonly IDictionary<ExpressionType, string> _operators = new Dictionary<ExpressionType, string>
        {
            {ExpressionType.Equal, "="},
            {ExpressionType.NotEqual, "!="},
            {ExpressionType.GreaterThan, ">"},
            {ExpressionType.GreaterThanOrEqual, ">="},
            {ExpressionType.LessThan, "<"},
            {ExpressionType.LessThanOrEqual, "<="}
        };

        private readonly ISerializer _serializer;
        private readonly StoreOptions _options;

        public MartenExpressionParser(ISerializer serializer, StoreOptions options)
        {
            _serializer = serializer;
            _options = options;
        }

        public IWhereFragment ParseWhereFragment(IDocumentMapping mapping, Expression expression)
        {
            if (expression is LambdaExpression)
            {
                expression = expression.As<LambdaExpression>().Body;
            }

            var visitor = new WhereClauseVisitor(this, mapping);
            visitor.Visit(expression);
            var whereFragment = visitor.ToWhereFragment();

            if (whereFragment == null)
            {
                throw new NotSupportedException("Marten does not (yet) support this Linq query type");
            }

            return whereFragment;
        }

        // The out of the box method call parsers
        private static readonly IList<IMethodCallParser> _parsers = new List<IMethodCallParser>
        {
            new StringContains(),
            new EnumerableContains(),
            new StringEndsWith(),
            new StringStartsWith(),
            new StringEquals(),

            // Added
            new IsOneOf(),
            new IsInGenericEnumerable()
        };


        private IWhereFragment buildSimpleWhereClause(IDocumentMapping mapping, BinaryExpression binary)
        {
            var isValueExpressionOnRight = binary.Right.IsValueExpression();
            var jsonLocatorExpression = isValueExpressionOnRight ? binary.Left : binary.Right;
            var valuExpression = isValueExpressionOnRight ? binary.Right : binary.Left;

            var op = _operators[binary.NodeType];

            var value = valuExpression.Value();

            if (mapping.PropertySearching == PropertySearching.ContainmentOperator &&
                binary.NodeType == ExpressionType.Equal && value != null)
            {
                return new ContainmentWhereFragment(_serializer, binary);
            }

            var jsonLocator = mapping.JsonLocator(jsonLocatorExpression);

            if (value == null)
            {
                var sql = binary.NodeType == ExpressionType.NotEqual
                    ? $"({jsonLocator}) is not null"
                    : $"({jsonLocator}) is null";

                return new WhereFragment(sql);
            }
            if (jsonLocatorExpression.NodeType == ExpressionType.Modulo)
            {
                var moduloByValue = MartenExpressionParser.moduloByValue(binary);
                return new WhereFragment("{0} % {1} {2} ?".ToFormat(jsonLocator, moduloByValue, op), value);
            }


            return new WhereFragment("{0} {1} ?".ToFormat(jsonLocator, op), value);
        }

        private static object moduloByValue(BinaryExpression binary)
        {
            var moduloExpression = binary.Left as BinaryExpression;
            var moduloValueExpression = moduloExpression?.Right as ConstantExpression;
            return moduloValueExpression != null ? moduloValueExpression.Value : 1;
        }
    }
}