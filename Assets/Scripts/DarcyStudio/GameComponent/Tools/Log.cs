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
#if UNITY_EDITOR
using UnityEditor;
#endif
using Debug = UnityEngine.Debug;

namespace DarcyStudio.GameComponent.Tools
{
    public static class Log
    {

        //其实通过 Conditional 标志来区分 Log 这个标志就可以去除了
        //鉴于这是一个测试工程 也不做发布，就先保留吧
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

        [Conditional ("DEBUG_WARNING")]
        public static void Warning (string warning)
        {
            if (!CanLog (LogLevel.Warning))
                return;
            Debug.LogWarning (warning);
        }

        [Conditional ("DEBUG_ERROR")]
        public static void Error (string error)
        {
            if (!CanLog (LogLevel.Error))
                return;
            Debug.LogError (error);
        }

        private static bool CanLog (LogLevel level)
        {
            return level >= _logLevel;
        }
#if UNITY_EDITOR

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
#endif


        // 看起来这个单元测试很简单
        // 但是刚刚同步改了项目里的 Log，
        // 也觉得改的很简单，不会出什么问题，
        // 但是打包测试发现竟然导致游戏卡死。。。
        // 如果有这个单元测试那么只要修改后一运行就知道是否有问题了
        // 第一次尝试增加单元测试脚本，虽然很简单😁
        public static void Test ()
        {
            Debug.Log ($"Set log level: {LogLevel.Info}");
            SetLogLevel (LogLevel.Info);
            Info ("<color=cyan>Test LogInfo</color>");
            Warning ("Test warning");
            Error ("Test error");

            Debug.Log ($"Set log level: {LogLevel.Warning}");
            SetLogLevel (LogLevel.Warning);
            Info ("<color=cyan>Test LogInfo</color>");
            Warning ("Test warning");
            Error ("Test error");

            Debug.Log ($"Set log level: {LogLevel.Error}");
            SetLogLevel (LogLevel.Error);
            Info ("<color=cyan>Test LogInfo</color>");
            Warning ("Test warning");
            Error ("Test error");
        }
    }
}