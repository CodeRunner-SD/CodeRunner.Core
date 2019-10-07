using CodeRunner.Diagnostics;
using System;

namespace CodeRunner.Extensions.Terminals.Rendering
{
    public class OutputTableColumnStringView<T> : IOutputTableColumnView<T>
    {
        public OutputTableColumnStringView(Func<T, string> valueFunc, string header)
        {
            Assert.ArgumentNotNull(valueFunc, nameof(valueFunc));
            Assert.ArgumentNotNull(header, nameof(header));
            ValueFunc = valueFunc;
            Header = header;
        }

        private Func<T, string> ValueFunc { get; }

        private string Header { get; }

        protected string GetValue(T value) => ValueFunc(value);

        public virtual int Measure(T value) => GetValue(value).Length;

        public virtual int MeasureHeader() => Header.Length;

        public virtual void Render(ITerminal terminal, T value, int length)
        {
            Assert.ArgumentNotNull(terminal, nameof(terminal));
            terminal.Output.Write(GetValue(value).PadRight(length));
        }

        public virtual void RenderHeader(ITerminal terminal, int length)
        {
            Assert.ArgumentNotNull(terminal, nameof(terminal));
            terminal.Output.WriteEmphasize(Header.PadRight(length));
        }
    }
}
