﻿using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Framework.Infrastructure
{
    public static class CacheUtil
    {
        /// <summary>
        /// TypePropertyCache
        /// </summary>
        public static readonly ConcurrentDictionary<Type, PropertyInfo[]> TypePropertyCache = new ConcurrentDictionary<Type, PropertyInfo[]>();
    }
}
