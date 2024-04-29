using System;
using UnityEngine;

namespace Gokboerue.Tools
{
    public class Logger<T> where T : class
    {
        private static string logFilePath = "Assets/Logs/log.txt";
        private static string logString { get; set; } = typeof(T).FullName + " ";

        private static void SetLog(object message)
        {
            logString += message + " " + DateTime.Now + "\n";
            Debug.Log(logString);
            System.IO.File.AppendAllText(logFilePath, logString);
        }

        private static void SetLogWarning(object message)
        {
            logString += message + " " + DateTime.Now + "\n";
            Debug.LogWarning(logString);
            System.IO.File.AppendAllText(logFilePath, logString);
        }

        private static void SetLogError(object message)
        {
            logString += message + " " + DateTime.Now + "\n";
            Debug.LogError(logString);
            System.IO.File.AppendAllText(logFilePath, logString);
        }

        private static void Log(object message, LogType logType = LogType.Log)
        {
            switch (logType)
            {
                case LogType.Log:
                    SetLog(message);
                    break;
                case LogType.Warning:
                    SetLogWarning(message);
                    break;
                case LogType.Error:
                    SetLogError(message);
                    break;
            }
        }

        public static Exception LogException(object message, LogType logType)
        {
            logString += message + " " + DateTime.Now + "\n";
            Log(message, logType);
            return new Exception(logString);
        }
    }

    public enum LogType
    {
        Log,
        Warning,
        Error
    }
}
