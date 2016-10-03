// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DynamicExtensions.cs" company="Map Of Medicine">
//   Copyright (c) 2016 Map Of Medicine. All rights reserved.
// </copyright>
// <summary>
//   Defines the DynamicExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Core
{
    using System;
    using System.Collections.Generic;

    public static class DynamicExtensions
    {
        public static IEnumerable<T> ToArrayOf<T>(dynamic array, Func<dynamic, T> creator)
        {
            var created = new List<T>();
            foreach (var item in array)
            {
                created.Add(creator(item));
            }

            return created;
        }
    }
}
