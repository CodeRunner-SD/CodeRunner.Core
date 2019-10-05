using CodeRunner.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CodeRunner.Templates
{
    public class ResolveContext
    {
        public ResolveContext(IDictionary<string, object>? variables = null) => Variables = variables ?? new Dictionary<string, object>();

        private IDictionary<string, object> Variables { get; }

        public ResolveContext WithVariable<T>(Variable variable, T value) where T : notnull
        {
            Assert.ArgumentNotNull(variable, nameof(variable));

            return WithVariable(variable.Name, value);
        }

        public ResolveContext WithVariable<T>(string name, T value) where T : notnull
        {
            Assert.ArgumentNotNull(name, nameof(name));
            Assert.ArgumentNotNull(value, nameof(value));

            if (Variables.ContainsKey(name))
            {
                Variables[name] = value;
            }
            else
            {
                Variables.Add(name, value);
            }

            return this;
        }

        public ResolveContext WithoutVariable(string name)
        {
            Assert.ArgumentNotNull(name, nameof(name));

            _ = Variables.Remove(name);
            return this;
        }

        public T GetVariable<T>(Variable variable)
        {
            Assert.ArgumentNotNull(variable, nameof(variable));

            if (Variables.TryGetValue(variable.Name, out object? val))
            {
                return (T)val;
            }
            else
            {
                if (variable.IsRequired)
                {
                    throw new Exception($"No required variable with name {variable.Name}.");
                }
                return variable.GetDefault<T>();
            }
        }

        public bool TryGetVariable<T>(Variable variable, [NotNullWhen(true), MaybeNullWhen(false)] out T value)
        {
            Assert.ArgumentNotNull(variable, nameof(variable));

#pragma warning disable CS8653 // 默认表达式为类型参数引入了 null 值。
            value = default;
#pragma warning restore CS8653 // 默认表达式为类型参数引入了 null 值。

            if (Variables.TryGetValue(variable.Name, out object? val))
            {
                if (val is T res)
                {
                    value = res;
                    return true;
                }
            }
            else if (!variable.IsRequired)
            {
                value = variable.GetDefault<T>();
                return true;
            }
            return false;
        }

        public bool HasVariable(string name)
        {
            Assert.ArgumentNotNull(name, nameof(name));
            return Variables.ContainsKey(name);
        }
    }
}
