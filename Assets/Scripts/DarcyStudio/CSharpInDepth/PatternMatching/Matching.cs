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

    }

    public class Triangle : Shape
    {

    }
}