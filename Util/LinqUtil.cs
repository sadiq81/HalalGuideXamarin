using System;
using System.Linq.Expressions;

namespace HalalGuide.Util
{
	public static class LinqUtil
	{
		public static Expression<TDelegate> AndAlso<TDelegate> (this Expression<TDelegate> left, Expression<TDelegate> right)
		{
			return Expression.Lambda<TDelegate> (Expression.AndAlso (left, right), left.Parameters);
		}
	}
}

