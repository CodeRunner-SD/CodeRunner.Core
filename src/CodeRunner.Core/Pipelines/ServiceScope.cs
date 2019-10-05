using CodeRunner.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CodeRunner.Pipelines
{
    public sealed class ServiceScope : IServiceScope
    {
        private Dictionary<Type, Dictionary<string, ServiceItem>> Pool { get; }

        public string Name { get; }

        internal ServiceScope(string name, Dictionary<Type, Dictionary<string, ServiceItem>> pool)
        {
            Pool = pool;
            Name = name;
        }

        private Dictionary<string, ServiceItem> OpenOrCreateSubDictionary<T>()
        {
            Type type = typeof(T);
            if (!Pool.TryGetValue(type, out Dictionary<string, ServiceItem>? list))
            {
                list = new Dictionary<string, ServiceItem>();
                Pool.Add(type, list);
            }
            return list;
        }

        private Dictionary<string, ServiceItem>? FindSubDictionary<T>() => Pool.TryGetValue(typeof(T), out Dictionary<string, ServiceItem>? list) ? list : null;

        public void Add<T>(T item, string id = "") where T : notnull
        {
            Assert.ArgumentNotNull(item, nameof(item));
            Assert.ArgumentNotNull(id, nameof(id));

            OpenOrCreateSubDictionary<T>().Add(id, new ServiceItem(item, Name));
        }

        public void Replace<T>(T item, string id = "") where T : notnull
        {
            Assert.ArgumentNotNull(item, nameof(item));
            Assert.ArgumentNotNull(id, nameof(id));

            Dictionary<string, ServiceItem> list = OpenOrCreateSubDictionary<T>();
            if (list.ContainsKey(id))
            {
                list[id] = new ServiceItem(item, Name);
            }
            else
            {
                list.Add(id, new ServiceItem(item, Name));
            }
        }

        public void Remove<T>(string id = "")
        {
            Assert.ArgumentNotNull(id, nameof(id));

            Dictionary<string, ServiceItem>? list = FindSubDictionary<T>();
            if (list != null)
            {
                if (list.Remove(id))
                {
                    if (list.Count == 0)
                    {
                        _ = Pool.Remove(typeof(T));
                    }
                }
            }
        }

        public T GetService<T>(string id = "") where T : notnull
        {
            Assert.ArgumentNotNull(id, nameof(id));
            return (T)FindSubDictionary<T>()![id].Value;
        }

        public string GetSource<T>(string id = "") where T : notnull
        {
            Assert.ArgumentNotNull(id, nameof(id));
            return FindSubDictionary<T>()![id].Source;
        }

        public bool TryGet<T>([NotNullWhen(true), MaybeNullWhen(false)] out T value, string id = "") where T : notnull
        {
            Assert.ArgumentNotNull(id, nameof(id));

#pragma warning disable CS8653 // 默认表达式为类型参数引入了 null 值。
            value = default;
#pragma warning restore CS8653 // 默认表达式为类型参数引入了 null 值。

            Dictionary<string, ServiceItem>? dict = FindSubDictionary<T>();

            if (dict == null)
                return false;
            if (dict.TryGetValue(id, out ServiceItem item))
            {
                if (item.Value is T res)
                {
                    value = res;
                    return true;
                }
            }
            return false;
        }
    }
}
