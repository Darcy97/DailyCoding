/***
 * Created by Darcy
 * Date: Monday, 07 June 2021
 * Time: 17:37:32
 * Description: Womple(独素元组) and Giant tuple(巨型元组)
 ***/

using System;

namespace DarcyStudio.CSharpInDepth.Tuple
{
    public class SpecialTuple
    {
        private void Test ()
        {
            var tuple  = (1, 2, 3, 4, 5, 6, 7, 8, 9); //巨型元组 7 之后的部分其实会被编译器转化成一个元组，相当于 (1, 2, 3, 4, 5, 6, 7, (8, 9))
            var item9  = tuple.Item9; // 这个调用编译器会转化成 tuple.Rest.Item2
            var sItem9 = tuple.Rest.Item2;
        }
    }
}