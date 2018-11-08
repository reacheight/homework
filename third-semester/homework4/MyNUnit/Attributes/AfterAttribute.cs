using System;

namespace MyNUnit
{
    /// <summary>
    /// Class that implements after attribute.
    /// Indicates that method should be executed after execution of any test in the class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AfterAttribute : Attribute
    {
    }
}