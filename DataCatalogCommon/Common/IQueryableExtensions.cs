﻿using System;
using System.Linq;
using System.Linq.Dynamic;

namespace DataCatalogCommon.Common
{
    /// <summary>
    /// Provides additional functionality to implementation so the IQueryable
    /// interface via extensions.
    /// </summary>
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Custom sort extension for IQueryable implmentations which takes a comma delimited
        /// list of fields to create a string based sort expression. This extension using the
        /// System.Linq.Dynamic nuget package, documentation found in github here
        /// https://github.com/kahanu/System.Linq.Dynamic
        /// </summary>
        /// <typeparam name="T">Support for generic type IQueryable instances</typeparam>
        /// <param name="source">The instance of data to sort</param>
        /// <param name="sortParameters">Comma delimited string of sort fields to build the expression</param>
        /// <returns></returns>
        public static IQueryable<T> SortByExpression<T>(this IQueryable<T> source, string sortParameters)
        {
            // Source must be not null to continue
            if (source == null)
            {
                throw new ArgumentNullException("source", "Data source can not be null.");
            }

            // Return the source as is if nothing is available in the sort parameters
            if (sortParameters == null || string.IsNullOrEmpty(sortParameters))
            {
                return source;
            }

            // Split the sort input and iterate through each item
            var arrSort = sortParameters.Split(',');
            string sortSyntax = "";

            foreach(var sortParam in arrSort)
            {
                if(sortParam.StartsWith("-"))
                {
                    // Add descending if the sort parameters begins with a "-" sign
                    sortSyntax = string.Format("{0}{1} descending,",sortSyntax, sortParam.Remove(0, 1));
                }
                else
                {
                    sortSyntax = string.Format("{0}{1} ,", sortSyntax, sortParam);
                }
            }

            if(!string.IsNullOrWhiteSpace(sortSyntax))
            {
                // Complete the sort removing the last comma character
                source = source.OrderBy(sortSyntax.Remove(sortSyntax.Count() - 1));
            }

            return source;
        }
    }
}
