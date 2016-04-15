﻿using System.Linq;
using System.Linq.Expressions;
using Marten.Schema;

namespace Marten.Linq.Handlers
{
    public class IsOneOf : IMethodCallParser
    {
        public bool Matches(MethodCallExpression expression)
        {
            return expression.Method.Name == nameof(LinqExtensions.IsOneOf)
                   && expression.Method.DeclaringType.Equals(typeof (LinqExtensions));
        }

        public IWhereFragment Parse(IDocumentMapping mapping, ISerializer serializer, MethodCallExpression expression)
        {
            var finder = new FindMembers();
            finder.Visit(expression);

            var members = finder.Members;

            var locator = mapping.FieldFor(members).SqlLocator;
            var values = expression.Arguments.Last().Value();


            return new WhereFragment($"{locator} = ANY(?)", values);
        }
    }
}