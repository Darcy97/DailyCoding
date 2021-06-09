/***
 * Created by Darcy
 * Date: Wednesday, 09 June 2021
 * Time: 19:06:42
 * Description: Description
 ***/

using System;

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
    }

    public abstract class Shape
    {

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