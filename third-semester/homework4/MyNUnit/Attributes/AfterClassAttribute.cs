using System;

namespace MyNUnit.Attributes
{
    /// <summary>
    /// Class that implements after class attribute.
    /// Indicates that method should be executed after execution of all tests in the class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AfterClassAttribute : Attribute
    {
    }
}