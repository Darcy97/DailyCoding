/***
 * Created by Darcy
 * Date: Monday, 07 June 2021
 * Time: 15:55:22
 * Description: ⚠️这个可能不配叫做捕获变量😷
 * 这里的捕获变量与闭包捕获变量有所不同
 * 闭包捕获的变量不是 Copy 一份，实际捕获的是 variable 而不是 value，在任一个地方修改，另外一个的值都会改变
 * 通过这种方式捕获的 值类型变量，实际上是 Copy 了一份 value，所以指向的是两个内存，所以修改一处另一处不会改变
 * TODO: 有时间继续探索一下能否完美实现闭包捕获变量的行为
 ***/

// ReSharper disable MemberCanBePrivate.Global

namespace DarcyStudio.Closure.ReusableAction
{
    public class VariableCapture<T>
    {
        protected T Arg;

        public void Capture (T arg)
        {
            Arg = arg;
        }
    }

    public class VariableCapture<T0, T1>
    {
        protected T0 Arg0;
        protected T1 Arg1;

        public void Capture (T0 arg0, T1 arg1)
        {
            Arg0 = arg0;
            Arg1 = arg1;
        }
    }

    public class VariableCapture<T0, T1, T2>
    {
        protected T0 Arg0;
        protected T1 Arg1;
        protected T2 Arg2;

        public void Capture (T0 arg0, T1 arg1, T2 arg2)
        {
            Arg0 = arg0;
            Arg1 = arg1;
            Arg2 = arg2;
        }
    }

    public class VariableCapture<T0, T1, T2, T3>
    {
        protected T0 Arg0;
        protected T1 Arg1;
        protected T2 Arg2;
        protected T3 Arg3;

        public void Capture (T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            Arg0 = arg0;
            Arg1 = arg1;
            Arg2 = arg2;
            Arg3 = arg3;
        }
    }
}