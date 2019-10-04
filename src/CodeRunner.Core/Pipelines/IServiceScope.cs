using System.Diagnostics.CodeAnalysis;

namespace CodeRunner.Pipelines
{
    public interface IServiceScope
    {
        string Name { get; }

        void Add<T>(T item, string id = "") where T : notnull;
        T GetService<T>(string id = "") where T : notnull;
        string GetSource<T>(string id = "") where T : notnull;
        void Remove<T>(string id = "");
        void Replace<T>(T item, string id = "") where T : notnull;
        bool TryGet<T>([MaybeNullWhen(false), NotNullWhen(true)] out T value, string id = "") where T : notnull;
    }
}