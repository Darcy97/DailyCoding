/***
 * Created by Darcy
 * Date: Thursday, 10 June 2021
 * Time: 16:39:08
 * Description: 统一 Log 接口
 * 有时间把缺的补上
 * 2021.12.06 更新
 * 将通过 if 判断局部变量的方式去掉
 * 优化为使用 Conditional
 * 这样做的好处是在打包前设置好 Symbol 后 (调用如下代码 PreBuildEnableLog/PreBuildDisableLog)
 * 在打出的正式包里就不会编译相关代码
 * 可以少编译很多字符串
 * 尤其是在大型项目里，Log 随处可见，这样可以减少一些性能消耗
 *
 * 注：C#在编译时会将程序集中声明的所有字符串常量放到保留池中（intern pool）
 ***/

using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using Debug = UnityEngine.Debug;

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

        [Conditional ("DEBUG_INFO")]
        public static void Info (string info)
        {
            if (!CanLog (LogLevel.Info))
                return;
            Debug.Log (info);
        }

        [Conditional ("DEBUG_INFO")]
        public static void Info (string format, object arg0)
        {
            if (!CanLog (LogLevel.Info))
                return;
            Debug.Log (string.Format (format, arg0));
        }

        [Conditional ("DEBUG_INFO")]
        public static void Info (string format, object arg0, object arg1)
        {
            if (!CanLog (LogLevel.Info))
                return;
            Debug.Log (string.Format (format, arg0, arg1));
        }

        [Conditional ("DEBUG_INFO")]
        public static void Info (string format, object arg0, object arg1, object arg2)
        {
            if (!CanLog (LogLevel.Info))
                return;
            Debug.Log (string.Format (format, arg0, arg1, arg2));
        }

        [Conditional ("DEBUG_INFO")]
        public static void Info (string format, params object[] args)
        {
            if (!CanLog (LogLevel.Info))
                return;
            Debug.Log (string.Format (format, args));
        }

        [Conditional ("DEBUG_WARNING")]
        public static void Warning (string error)
        {
            if (!CanLog (LogLevel.Warning))
                return;
            Debug.LogWarning (error);
        }

        [Conditional ("DEBUG_ERROR")]
        public static void Error (string error)
        {
            if (!CanLog (LogLevel.Error))
                return;
            Debug.LogError (error);
        }

        [Conditional ("DEBUG_ERROR")]
        public static void Error (string format, object arg0)
        {
            if (!CanLog (LogLevel.Info))
                return;
            Debug.LogError (string.Format (format, arg0));
        }

        [Conditional ("DEBUG_ERROR")]
        public static void Error (string format, object arg0, object arg1)
        {
            if (!CanLog (LogLevel.Info))
                return;
            Debug.LogError (string.Format (format, arg0, arg1));
        }

        private static bool CanLog (LogLevel level)
        {
            return level >= _logLevel;
        }

        private static void PreBuildEnableLog ()
        {
            SetSymbol ("DEBUG_INFO",    true);
            SetSymbol ("DEBUG_WARNING", true);
            SetSymbol ("DEBUG_ERROR",   true);
        }

        private static void PreBuildDisableLog ()
        {
            SetSymbol ("DEBUG_INFO",    false);
            SetSymbol ("DEBUG_WARNING", false);
            SetSymbol ("DEBUG_ERROR",   false);
        }

        private static void SetSymbol (string symbol, bool enable)
        {
            var defines = GetDefinesList (EditorUserBuildSettings.selectedBuildTargetGroup);
            if (enable)
            {
                if (!defines.Contains (symbol))
                {
                    defines.Add (symbol);
                }
            }
            else
            {
                while (defines.Contains (symbol))
                {
                    defines.Remove (symbol);
                }
            }

            var definesString = string.Join (";", defines.ToArray ());
            PlayerSettings.SetScriptingDefineSymbolsForGroup (EditorUserBuildSettings.selectedBuildTargetGroup,
                definesString);
        }

        private static List<string> GetDefinesList (BuildTargetGroup group)
        {
            return new List<string> (PlayerSettings.GetScriptingDefineSymbolsForGroup (group).Split (';'));
        }

    }
}