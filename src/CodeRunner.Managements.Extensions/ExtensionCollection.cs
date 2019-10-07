using CodeRunner.Diagnostics;
using CodeRunner.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CodeRunner.Managements.Extensions
{
    public class ExtensionCollection : IEnumerable<ExtensionLoader>
    {
        private Dictionary<string, ExtensionLoader> Loaders { get; set; } = new Dictionary<string, ExtensionLoader>();

        public IEnumerable<IExtension> GetExtensions() => from item in Loaders.Values select item.Extension;

        public void Load(ExtensionLoader loader)
        {
            Assert.ArgumentNotNull(loader, nameof(loader));
            loader.Load();
            Assert.IsNotNull(loader.Assembly?.FullName);
            Loaders.Add(loader.Assembly.FullName, loader);
        }

        public void Unload(ExtensionLoader loader)
        {
            Assert.ArgumentNotNull(loader, nameof(loader));
            Assert.IsNotNull(loader.Assembly?.FullName);
            _ = Loaders.Remove(loader.Assembly.FullName);
            loader.Unload();
        }

        public void Unload()
        {
            ExtensionLoader[] data = Loaders.Values.ToArray();
            foreach (ExtensionLoader item in data)
                Unload(item);
        }

        public ExtensionLoader Get(Type type)
        {
            Assert.ArgumentNotNull(type, nameof(type));
            Assert.IsNotNull(type.Assembly.FullName);
            string key = type.Assembly.FullName;
            return Loaders[key];
        }

        public bool TryGet(Type type, [NotNullWhen(true), MaybeNullWhen(false)] out ExtensionLoader? value)
        {
            Assert.ArgumentNotNull(type, nameof(type));
            Assert.IsNotNull(type.Assembly.FullName);
            string key = type.Assembly.FullName;
            return Loaders.TryGetValue(key, out value);
        }

        public IEnumerator<ExtensionLoader> GetEnumerator() => Loaders.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
