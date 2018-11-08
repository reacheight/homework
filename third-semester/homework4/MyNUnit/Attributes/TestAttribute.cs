using System;
using System.Runtime.Serialization;

namespace MyNUnit
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TestAttribute : Attribute
    {
        public Type Excpected { get; set; } 
        public string Ignore { get; set; }
    }
}