/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Friday, 10 December 2021
 * Time: 14:27:55
 ***/

using UnityEngine;

namespace HugePit
{
    public class NullCheckOfUnityEngineObject:MonoBehaviour
    {
        
        /* 最近项目里看到一个低级错误导致的空异常，
         * 仔细看了一眼报错信息，竟然报错竟然出现在下面第一个 1-1 的 if 判断里
         * 当时第一想法是为什么空异常会报在，判空这一行，仔细看了下堆栈信息发现是调用 gameObject 的 get 方法报的错
         * 突然恍然大悟，如果这个对象关联的 gameObject 已经被销毁了，这样调用会直接抛异常
         * 看反编译代码也可以看到 gameObject 会调用底层的 get 方法，所以 Unity 底层的处理应该是当访问一个已经被销毁的对象的 gameObject 就会直接抛异常
         * 所以如果需要这样的判断，应该是用下面 1-2 这种写法，其实我也一直这么写，已经习惯了，并且淡忘了为什么这么写，所以第一眼看到第一种写法竟然没发现问题
         *
         * 上一句为何说 "如果需要这样的判断"，其实不推荐这种逻辑，一个对象的生命周期我们应该保证清晰，所以应该明确一个对象什么时候被销毁了
         * 所以已经知道被销毁了，我们就不该在对这个对象进行任何操作，也不该调用这个方法
         * 这种无脑 NullCheck 我实属讨厌，非常讨厌！
         *
         * 当时在群里同步了这个小问题，有一个同事同意了这种做法，
         * 并且说 gameObject 等价于 this.gameObject, this 都已经为空了，调用 this.gameObject 肯定是会抛异常，
         * 当时下班回家了在和女朋友聊天，没仔细想，第二天仔细看了他的发言，其实这个逻辑不对
         *
         * 为什么 要用 (this == null) 这个判断呢，其实这么做是因为 Unity 的一个特殊处理，
         * 其实这个特殊处理，作为长期使用 Unity 的开发人员，应该足够清晰明确，但是我发现周围的人很多人都对这个概念不是很清晰
         * 这里贴一个 Unity 官方的 Blog https://blog.unity.com/technology/custom-operator-should-we-keep-it
         * 在这里简单描述下
         * 其实如果抛开 Unity ，单看第二种这个写法，其实你会很奇怪，如果这个对象本身都为空了，
         * 那么这个方法是怎么调用到的呢，是因为确切的讲，这个对象并不为空，但是为什么 this == null 会返回 true 呢
         * 因为 Unity 这里有一个特殊特殊处理，Unity 重写了 UnityEngine.Object 的 "==" 操作符
         * 所以在对一个继承自 UnityEngine.Object 使用 "==" 操作符时，Unity 会有一些额外的逻辑判断，
         * 该例返回 true 是因为 Unity 会检查这个对象相关联的 gameObject 是否被销毁，如果销毁就认为 == null 成立
         * Unity 上面的 Blog 早在 14 年讨论过是否要保留这种处理，还是把这个处理去除，并且提供另外一个接口来检查 UnityEngine.Object 对象的生命周期
         * 因为这和我们通常意义上的 Null 有了一些差别，稍有不慎就会因为这个被坑到
         * 但是看结果是 Unity 保留了这种处理，那么我们作为开发人员就应该明确清楚这个逻辑
         *
         * 由此引申的一个小问题
         * 如下 2-1.1 a?.Method 这种写法其实是一个语法糖
         * 等价于 2-1.2
         * 
         * 但是 2-2.1 与 2-2.2 并不等价 因为对于 UnityEngine.Object 对象 == 被重载，但是 ?. 并没有重载
         * 所以使用 ?. 这种语法糖，最终底层执行的 null 判断是 c# 默认的，并不是 Unity 重载的
         * 所以这里一定要注意，曾经看到过很多这种错误，我非常喜欢各种语法糖与新特性，也倡导推荐大家使用
         * 很多固执的老程序员讨厌这些东西，所以他们干脆不用，我很不赞同
         * 但是我们使用这种新特性时也要带着脑子，不然适得其反
         * 
         * 这里赞赏一番 Rider，做的实在强大，有很多提示，比如下面 2-2.1 这行代码，
         * 如果你用 Rider 打开一个 Unity 工程，并且 Rider 检测出来你这是一个 Unity 工程
         * 就会标红提示这个写法是有问题的 "'?.' on a type deriving from 'UnityEngine.Object' bypasses the lifetime check on the underlying Unity engine object"
         *
         * 当然没有工具是万能的，曾经发现一个 Rider 的 小 bug: 详见 SuperValueAssign.cs
        */
        public void Method ()
        {
            // 1-1
            if (gameObject == null)
                return;
            
            // 1-2
            if (this == null)
                return;

            // 2-0
            var a = new A ();
            var b = new B ();

            // 
            {
                //some logic
            }
            
            // 2-1.1
            a?.Method ();
            
            // 2-1.2
            if(a != null)
                a.Method ();
           
            // 2-2.1
            b?.Method ();
            
            // 2-2.2
            if(b != null)
                b.Method ();
            
            
        }
    }

    public class A
    {
        public void Method ()
        {
            
        }
    }

    public class B : UnityEngine.Object
    {
        public void Method ()
        {
            
        }
    }
}