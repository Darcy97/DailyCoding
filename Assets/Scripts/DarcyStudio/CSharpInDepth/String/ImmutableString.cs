/***
 * Created by Darcy
 * Date: Tuesday, 15 June 2021
 * Time: 17:27:07
 * Description: 字符串不可变特性
 ***/

using UnityEngine;

namespace DarcyStudio.CSharpInDepth.String
{
    public class ImmutableString
    {
        private void Test ()
        {
            var str1 = "222";
            var str2 = str1; // 字符串特殊，这里复习一下，竟然给忘了，字符串是引用类型，这里 str2 与 str1 指向同一个内存地址 指向 "222"
            str1 = "111";    // 但是字符串是不可变的，所以这里修改 str1 的值时 实际上 str1 指向了新的地址，所以str2 不会变
            Debug.Log ($"Str2: {str2}"); //输出 222
        }        
    }
}