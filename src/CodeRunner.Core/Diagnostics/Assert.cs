using Microsoft;
using System;
using System.Diagnostics.CodeAnalysis;

namespace CodeRunner.Diagnostics
{
    public static class Assert
    {
        public static void IsTrue([DoesNotReturnIf(false)] bool condition, string message = "")
        {
            if (!condition)
            {
                throw new AssertFailedException($"{nameof(IsTrue)}: {message}");
            }
        }

        public static void IsFalse([DoesNotReturnIf(true)] bool condition, string message = "")
        {
            if (condition)
            {
                throw new AssertFailedException($"{nameof(IsFalse)}: {message}");
            }
        }

        public static void ArgumentNotNull([ValidatedNotNull][NotNull] object? value, string paramName, string message = "")
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName, message);
            }
        }

        public static void IsNotNull([ValidatedNotNull][NotNull] object? value, string message = "")
        {
            if (value == null)
            {
                throw new AssertFailedException($"{nameof(IsNotNull)}: {message}");
            }
        }

        public static void IsNull([MaybeNull] object? value, string message = "")
        {
            if (value != null)
            {
                throw new AssertFailedException($"{nameof(IsNull)}: {message}");
            }
        }

        [DoesNotReturn]
        public static void Fail(string message = "") => throw new AssertFailedException($"{nameof(Fail)}: {message}");
    }
}
