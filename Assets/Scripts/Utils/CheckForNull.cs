#nullable enable
using System;

namespace Utils
{
    public static class CheckForNull
    {
        public static T EnsureOrNull<T>(this T? value, string? message = null)
        {
            if (value == null || value.ToString() == "null")
            {
                throw new NullReferenceException(message);
            }

            return value;
        }
    }
}