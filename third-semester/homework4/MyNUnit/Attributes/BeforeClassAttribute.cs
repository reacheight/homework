using System;

namespace MyNUnit.Attributes
{
    /// <summary>
    /// Class that implements before class attribute.
    /// Indicates that method should be executed before execution of all tests in the class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class BeforeClassAttribute : Attribute
    {
    }
}