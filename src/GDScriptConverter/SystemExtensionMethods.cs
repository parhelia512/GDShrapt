﻿namespace System
{
    public static class SystemExtensionMethods
    {
        public static bool IsNullOrEmpty(this string self) => string.IsNullOrEmpty(self);
        public static bool IsNullOrWhiteSpace(this string self) => string.IsNullOrWhiteSpace(self);
    }
}
