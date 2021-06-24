/***
 * Created by Darcy
 * Date: Wednesday, 16 June 2021
 * Time: 20:54:36
 * Description: Description
 ***/

using UnityEngine;

namespace DarcyStudio.StudyNotes.CSharpInDepth.LocalMethod
{
    public class Example
    {

        /*
        /// <summary>
        /// 局部方法只能捕获作用域内的变量
        /// </summary>
        private void Invalid0 ()
        {
            for (var i = 0; i < 10; i++)
            {
                PrintI ();
            }
         
            //非法
            void PrintI () => Debug.Log (i);
        }
        */

        /// <summary>
        /// 局部方法必须在其捕获的变量之后声明
        /// 该规则是 出于一致性考虑 (所有访问都必须在声明之后) 而非必要
        /// 
        /// </summary>
        private void Invalid1 ()
        {
            // void PrintI () => Debug.Log (i); //非法
            const int i = 10;

            // PrintI ();
            void PrintI2 () => Debug.Log (i);
            PrintI2 ();
        }
        
        // 局部方法不能捕获 Ref 变量
        
        // 局部方法有与闭包相似的一些行为 但是效率却高于闭包 ！！！
        // 因为闭包被当作委托使用时 生命周期可能不确定，所以编译器需要生成一个匿名类来存储数据，有额外的堆内存分配
        // 局部方法不涉及堆内存的分配，只操作栈内存
        
    }
}