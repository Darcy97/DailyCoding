/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Wednesday, 21 July 2021
 * Time: 11:50:59
 * Description: Time
 ***/

using System.Text;
using DarcyStudio.GameComponent.StringHelper;
using DarcyStudio.GameComponent.Tools;

namespace DarcyStudio.GameComponent.TimeHelper
{
    public static class TimeUtils
    {

        private static readonly StringBuilder _stringBuilder = new StringBuilder ();

        private static readonly string[] intToStringTable =
        {
            "00", "01", "02", "03", "04", "05", "06", "07", "08", "09",
            "10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
            "20", "21", "22", "23", "24", "25", "26", "27", "28", "29",
            "30", "31", "32", "33", "34", "35", "36", "37", "38", "39",
            "40", "41", "42", "43", "44", "45", "46", "47", "48", "49",
            "50", "51", "52", "53", "54", "55", "56", "57", "58", "59"
        };

        private const int SecondsOneDay = 86400;

        /// <summary>
        /// 由于数字转字符串会有 GC 且倒计时每秒都更新
        /// 这里做一下处理，减少每次获取时间字符串的 GC
        /// 把秒数转成1d 00:00:00格式
        /// </summary>
        /// <returns>  00:00:00 </returns>
        /// <param name="secs">Secs.</param>
        public static string SecondToFmtString (long secs)
        {
            if (secs < 0)
            {
                Log.Error ("Value is less than 0");
                return "00:00:00";
            }

            _stringBuilder.Clear ();

            var dayNum = secs / SecondsOneDay;

            var day  = StringUtils.IntToString ((int) dayNum);
            var hour = IntToString ((int) (secs / 3600 % 24));
            var min  = IntToString ((int) (secs / 60   % 60));
            var sec  = IntToString ((int) (secs        % 60));

            if (secs > SecondsOneDay)
            {
                _stringBuilder.Append (day).Append ("d ");
                _stringBuilder.Append (hour).Append (":");
                _stringBuilder.Append (min).Append (":");
                _stringBuilder.Append (sec);
            }
            else
            {
                _stringBuilder.Append (hour).Append (":");
                _stringBuilder.Append (min).Append (":");
                _stringBuilder.Append (sec);
            }

            var result = _stringBuilder.ToString ();

            return result;
        }

        /// <summary>
        /// 仅用于时间转换
        /// 0  -> "00"
        /// 12 -> "12"
        /// 大于60的不做处理
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        private static string IntToString (int variable)
        {
            return variable >= 60 || variable < 0 ? intToStringTable[0] : intToStringTable[variable];
        }
    }
}