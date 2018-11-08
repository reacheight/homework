using System;
using System.Runtime.Serialization;

namespace MyNUnit
{
    /// <summary>
    /// Class that implements test attribute.
    /// Indicates whether method is test method or not.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TestAttribute : Attribute
    {
        public Type Excpected { get; set; } 
        public string Ignore { get; set; }
    }
}