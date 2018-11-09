using System;
using System.IO;
using NUnit.Framework;
using Shouldly;

namespace MyNUnit.Tests
{
    public class TestExceptionTests
    {
        [Test]
        public void TestClassWithoutParameterlessConstructorThrowsException()
        {
            var exception = Should.Throw<AggregateException>(
                () => TestSystem.RunTests("../../../../TestProjects/TestProject8/bin"));
            exception.InnerException.InnerException.ShouldBeOfType<InvalidConstructorException>();
        }

        [Test]
        public void TestWithParametersThrowsException()
        {
            var exception = Should.Throw<AggregateException>(
                () => TestSystem.RunTests("../../../../TestProjects/TestProject9/bin"));
            exception.InnerException.InnerException.ShouldBeOfType<InvalidTestMethodException>();
        }

        [Test]
        public void WrongPathThrowsException()
        {
            Should.Throw<DirectoryNotFoundException>(
                () => TestSystem.RunTests("no_existed_folder"));
            Should.Throw<IOException>(
                () => TestSystem.RunTests("../../../MyNUnit.csproj"));
        }
    }
}