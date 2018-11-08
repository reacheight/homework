using System;

namespace MyNUnit
{
    /// <summary>
    /// Class that implements before attribute.
    /// Indicates that method should be executed before execution of any test in the class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class BeforeAttribute : Attribute
    {
    }
}