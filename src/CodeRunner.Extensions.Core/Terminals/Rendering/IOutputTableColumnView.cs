﻿namespace CodeRunner.Extensions.Terminals.Rendering
{
    public interface IOutputTableColumnView<T>
    {
        int MeasureHeader();

        void RenderHeader(ITerminal terminal, int length);

        int Measure(T value);

        void Render(ITerminal terminal, T value, int length);
    }
}
