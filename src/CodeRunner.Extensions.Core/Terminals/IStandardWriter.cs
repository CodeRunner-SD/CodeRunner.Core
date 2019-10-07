using CodeRunner.Extensions.Terminals.Rendering;
using System.Collections.Generic;

namespace CodeRunner.Extensions.Terminals
{
    public interface IStandardWriter
    {
        void Write(string content);

        void WriteEmphasize(string content);

        void WriteStandout(string content);

        void WriteBold(string content);

        void WriteUnderline(string content);

        void WriteBlink(string content);

        void WriteError(string content);

        void WriteWarning(string content);

        void WriteInformation(string content);

        void WriteDebug(string content);

        void WriteFatal(string content);

        void WriteTable<TSource>(IEnumerable<TSource> sources, params IOutputTableColumnView<TSource>[] columns);
    }
}
