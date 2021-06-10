/***
 * Created by Darcy
 * Date: Thursday, 10 June 2021
 * Time: 18:03:08
 * Description: Description
 ***/

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
        }

    }
}