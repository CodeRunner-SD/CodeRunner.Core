using System;

namespace CodeRunner.Extensions.Terminals
{
    public interface ITerminal
    {
        IStandardReader Input { get; }

        IStandardWriter Output { get; }

#pragma warning disable CA1716 // 标识符不应与关键字匹配
        IStandardWriter Error { get; }
#pragma warning restore CA1716 // 标识符不应与关键字匹配

        ConsoleColor BackgroundColor
        {
            get;
            set;
        }

        ConsoleColor ForegroundColor
        {
            get;
            set;
        }

        int CursorLeft
        {
            get;
            set;
        }

        int CursorTop
        {
            get;
            set;
        }

        void ResetColor();

        void Clear();

        void SetCursorPosition(int left, int top);

        void HideCursor();

        void ShowCursor();
    }
}
