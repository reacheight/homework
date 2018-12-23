using System;

namespace MyNUnit.Attributes
{
    /// <summary>
    /// Class that implements test attribute.
    /// Indicates whether method is test method or not.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TestAttribute : Attribute
    {
        public Type Expected { get; set; } 
        public string Ignore { get; set; }
    }
}