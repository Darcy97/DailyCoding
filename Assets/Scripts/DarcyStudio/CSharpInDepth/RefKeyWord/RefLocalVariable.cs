/***
 * Created by Darcy
 * Date: Thursday, 10 June 2021
 * Time: 18:03:08
 * Description: Description
 ***/

using UnityEngine;

namespace DarcyStudio.CSharpInDepth.RefKeyWord
{
    public class RefLocalVariable
    {

        private void Test ()
        {
            var     x = 2;
            ref var y = ref x;
            x++;
            y++; // 此时 y = 4 x = 4

            // ref int z; //非法 必须在声明时初始化
            // z = 2;

            var     x = 10;
            ref var y = ref RefReturn (ref x);
            y++;
            Debug.Log (x); //  结果是 11 
            // 如上调用 等价于 ref int y = ref x;
        }

        /// <summary>
        /// RefReturn
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private static ref int RefReturn (ref int p)
        {
            return ref p;
        }

    }
}