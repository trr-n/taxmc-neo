using System;
using System.Collections.Generic;
using UnityEngine;

namespace trrne.Box
{
    public static class std
    {
#nullable enable
        public static void cout(object? obj = null) => MonoBehaviour.print(obj ?? "null.");
#nullable disable
        public static string endl() => "\n";
    }
}