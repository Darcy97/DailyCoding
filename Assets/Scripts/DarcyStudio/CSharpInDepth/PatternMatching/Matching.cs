/***
 * Created by Darcy
 * Date: Wednesday, 09 June 2021
 * Time: 19:06:42
 * Description: Description
 ***/

using System;
using UnityEngine;

namespace DarcyStudio.CSharpInDepth.PatternMatching
{
    public class Matching
    {
        private void Perimeter (Shape shape)
        {
            switch (shape)
            {
                case null:
                    throw new ArgumentNullException ();
                case Rectangle rect:
                    // Do something;
                    break;
                case Circle circle:
                    // Do something;
                    break;
                case Triangle tri:
                    // Do something;
                    break;
                default:
                    throw new ArgumentException ();
            }
        }

        private void Test (Shape shape)
        {
            if (shape is Circle circle)
            {
                Debug.Log (circle);
            }

            // Debug.Log (circle);//非法 因为编译器无法确定 circle 是否被赋值，这取决于 shape
            circle = new Circle ();
        }

        /// <summary>
        /// 哨兵语句
        /// </summary>
        private void TestSentinel (int n)
        {
            switch (n)
            {
                case var t when t > 2:
                    break;
                case var _ when n > 2:
                    break;
            }
        }

        // 模式匹配与基于常量的 Switch 语句 有一个重大差别，模式匹配的 switch 语句 case 的顺序会影响执行结果，因为每个条件可能不是互斥的
    }

    public abstract class Shape
    {
        public static Shape CreateRandomShape ()
        {
            //Random 假装是 Random
            return new Circle ();
        }
    }

    public class Rectangle : Shape
    {

    }

    public class Circle : Shape
    {
        public float Radius;
    }

    public class Triangle : Shape
    {

    }
}