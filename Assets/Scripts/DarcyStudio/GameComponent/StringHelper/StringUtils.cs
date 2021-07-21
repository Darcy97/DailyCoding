/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Wednesday, 21 July 2021
 * Time: 11:58:56
 * Description: StringUtils
 * 有时间好好完善一下这个类
 ***/

using System.Collections.Generic;

namespace DarcyStudio.GameComponent.StringHelper
{
    public static class StringUtils
    {
        private static readonly string[] IntToStringTable =
        {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            "10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
            "20", "21", "22", "23", "24", "25", "26", "27", "28", "29",
            "30"
        };

        private static readonly Dictionary<int, string> IntValueToString = new Dictionary<int, string> (1000);


        /// <summary>
        /// 缓存 int 对应的 String
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        public static string IntToString (int variable)
        {
            if (variable >= 0 && variable < IntToStringTable.Length)
            {
                return IntToStringTable[variable];
            }

            if (variable == -1)
            {
                return "-1";
            }

            if (!IntValueToString.TryGetValue (variable, out var result))
            {
                IntValueToString.Add (variable, result = variable.ToString ());
            }

            return result;
        }
    }
}