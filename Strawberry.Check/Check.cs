using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Strawberry.Check
{
    public static class Check
    {
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CheckBuilder<TClass> Object<TClass>(TClass obj, string name)
            where TClass : class =>
            new CheckBuilder<TClass>(obj, name);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNull<T>([AllowNull] T value, [NotNull] string name)
            where T : class
        {
            _ = value ?? throw new ArgumentNullException(name);
        }
        
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNullOrEmpty([AllowNull] string value, [NotNull] string name)
        {
            _ = value ?? throw new ArgumentNullException(name);

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"Value of '{name}' is empty", name);
            }
        }
        
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNullOrWhiteSpace([AllowNull] string value, [NotNull] string name)
        {
            _ = value ?? throw new ArgumentNullException(name);

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"Value of '{name}' contains only white spaces", name);
            }
        }
    }
}
