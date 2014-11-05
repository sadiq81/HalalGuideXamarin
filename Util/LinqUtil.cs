﻿using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;

namespace HalalGuide.Util
{
	public static class PredicateBuilder
	{
		public static Expression<Func<T, bool>> AndAlso<T> (this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
		{
			var parameter = Expression.Parameter (typeof(T));

			var leftVisitor = new ReplaceExpressionVisitor (expr1.Parameters [0], parameter);
			var left = leftVisitor.Visit (expr1.Body);

			var rightVisitor = new ReplaceExpressionVisitor (expr2.Parameters [0], parameter);
			var right = rightVisitor.Visit (expr2.Body);

			return Expression.Lambda<Func<T, bool>> (
				Expression.AndAlso (left, right), parameter);
		}


		private class ReplaceExpressionVisitor : ExpressionVisitor
		{
			private readonly Expression _oldValue;
			private readonly Expression _newValue;

			public ReplaceExpressionVisitor (Expression newValue, Expression oldValue)
			{
				_newValue = newValue;
				_oldValue = oldValue;
			}

			public override Expression Visit (Expression node)
			{
				if (node == _oldValue) {
					return _newValue;
				} else {
					return base.Visit (node);
				}
			}
		}
	}
}

