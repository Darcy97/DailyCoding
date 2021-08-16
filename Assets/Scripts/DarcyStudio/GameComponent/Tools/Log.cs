/***
 * Created by Darcy
 * Date: Thursday, 10 June 2021
 * Time: 16:39:08
 * Description: 统一 Log 接口
 * 有时间把缺的补上
 ***/

using UnityEngine;

namespace DarcyStudio.GameComponent.Tools
{
    public static class Log
    {
        public enum LogLevel
        {
            Info,
            Warning,
            Error,
            Exception,
        }

        private static LogLevel _logLevel = LogLevel.Info;

        public static void SetLogLevel (LogLevel level)
        {
            _logLevel = level;
        }

        public static void Info (string info)
        {
            if (!CanLog (LogLevel.Info))
                return;
            Debug.Log (info);
        }

        public static void Info (string format, object arg0)
        {
            if (!CanLog (LogLevel.Info))
                return;
            Debug.Log (string.Format (format, arg0));
        }

        public static void Info (string format, object arg0, object arg1)
        {
            if (!CanLog (LogLevel.Info))
                return;
            Debug.Log (string.Format (format, arg0, arg1));
        }

        public static void Info (string format, object arg0, object arg1, object arg2)
        {
            if (!CanLog (LogLevel.Info))
                return;
            Debug.Log (string.Format (format, arg0, arg1, arg2));
        }

        public static void Info (string format, params object[] args)
        {
            if (!CanLog (LogLevel.Info))
                return;
            Debug.Log (string.Format (format, args));
        }

        public static void Warning (string error)
        {
            if (!CanLog (LogLevel.Warning))
                return;
            Debug.LogWarning (error);
        }

        public static void Error (string error)
        {
            if (!CanLog (LogLevel.Error))
                return;
            Debug.LogError (error);
        }

        public static void Error (string format, object arg0)
        {
            if (!CanLog (LogLevel.Info))
                return;
            Debug.LogError (string.Format (format, arg0));
        }


        private static bool CanLog (LogLevel level)
        {
            return level >= _logLevel;
        }

    }
}