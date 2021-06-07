/***
 * Created by Darcy
 * Date: Monday, 07 June 2021
 * Time: 14:21:32
 * Description: IAction
 ***/

namespace DarcyStudio.Action.ReusableAction
{
    public interface IAction
    {
        void Invoke ();
    }

    public interface IAction<in T>
    {
        void Invoke (T arg);
    }

    public interface IAction<in T0, in T1>
    {
        void Invoke (T0 arg0, T1 arg1);
    }

    public interface IAction<in T0, in T1, in T2>
    {
        void Invoke (T0 arg0, T1 arg1, T2 arg2);
    }

    public interface IAction<in T0, in T1, in T2, in T3>
    {
        void Invoke (T0 arg0, T1 arg1, T2 arg2, T3 arg3);
    }

    public interface IAction<in T0, in T1, in T2, in T3, in T4>
    {
        void Invoke (T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }

    public interface IAction<in T0, in T1, in T2, in T3, in T4, in T5>
    {
        void Invoke (T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }

    public interface IAction<in T0, in T1, in T2, in T3, in T4, in T5, in T6>
    {
        void Invoke (T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);
    }
}