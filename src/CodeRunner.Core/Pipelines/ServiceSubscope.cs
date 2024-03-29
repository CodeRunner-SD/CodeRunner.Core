﻿using CodeRunner.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CodeRunner.Pipelines
{
    public sealed class ServiceSubscope : IServiceScope
    {
        public ServiceSubscope(IServiceScope source) => Source = source;

        public string Name => Source.Name;

        private IServiceScope Source { get; set; }

        private List<Func<Type, string, bool>> ReadFilters { get; } = new List<Func<Type, string, bool>>();

        private List<Func<Type, string, bool>> WriteFilters { get; } = new List<Func<Type, string, bool>>();

        private bool ReadAccess<T>(string id) => ReadFilters.Select(x => x(typeof(T), id)).All(x => x);

        private bool WriteAccess<T>(string id) => WriteFilters.Select(x => x(typeof(T), id)).All(x => x);

        public void Add<T>(T item, string id = "") where T : notnull
        {
            Assert.IsTrue(WriteAccess<T>(id));
            Source.Add(item, id);
        }

        public T GetService<T>(string id = "") where T : notnull
        {
            Assert.IsTrue(ReadAccess<T>(id));
            return Source.GetService<T>(id);
        }

        public string GetSource<T>(string id = "") where T : notnull
        {
            Assert.IsTrue(ReadAccess<T>(id));
            return Source.GetSource<T>(id);
        }

        public void Remove<T>(string id = "")
        {
            Assert.IsTrue(WriteAccess<T>(id));
            Source.Remove<T>(id);
        }

        public void Replace<T>(T item, string id = "") where T : notnull
        {
            Assert.IsTrue(WriteAccess<T>(id));
            Source.Replace(item, id);
        }

        public bool TryGet<T>([MaybeNullWhen(false), NotNullWhen(true)] out T value, string id = "") where T : notnull
        {
            if (ReadAccess<T>(id))
            {
                return Source.TryGet(out value, id);
            }
            else
            {
#pragma warning disable CS8653 // 默认表达式为类型参数引入了 null 值。
                value = default;
#pragma warning restore CS8653 // 默认表达式为类型参数引入了 null 值。
                return false;
            }
        }

        public ServiceSubscope UseReadFilter(Func<Type, string, bool> filter)
        {
            ReadFilters.Add(filter);
            return this;
        }

        public ServiceSubscope UseWriteFilter(Func<Type, string, bool> filter)
        {
            WriteFilters.Add(filter);
            return this;
        }

        public ServiceSubscope NoRead<T>(string? id = null)
        {
            return id == null
                ? UseReadFilter((type, _) => type != typeof(T))
                : UseReadFilter((type, tid) => !(type == typeof(T) && tid == id));
        }

        public ServiceSubscope NoWrite<T>(string? id = null)
        {
            return id == null
                ? UseWriteFilter((type, _) => type != typeof(T))
                : UseWriteFilter((type, tid) => !(type == typeof(T) && tid == id));
        }

        public ServiceSubscope NoAccess<T>(string? id = null) => NoRead<T>(id).NoWrite<T>(id);
    }
}
