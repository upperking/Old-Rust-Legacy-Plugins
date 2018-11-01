using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RustEssentials
{
    public class Helper
    {
        public static void Log(string message)
        {
            Fougerite.Logger.Log(message);
        }
        public static void LogError(string message)
        {
            Fougerite.Logger.LogError(message);
        }
        public static void LogWarning(string message)
        {
            Fougerite.Logger.LogWarning(message);
        }
    }
}
