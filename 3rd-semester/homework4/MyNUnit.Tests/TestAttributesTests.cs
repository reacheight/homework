﻿using NUnit.Framework;
using Shouldly;
using TestProject1;
using TestProject10;
using TestProject2;
using TestProject3;
using TestProject4;
using TestProject5;

namespace MyNUnit.Tests
{
    public class TestAttributesTests
    {
        [Test]
        public void TestMethodsRun()
        {
            TestClass1.Executed = new[] {false, false, false};
            TestSystem.RunTests("../../../../TestProjects/TestProject1/bin");
            TestClass1.Executed.ShouldAllBe(flag => flag);
        }

        [Test]
        public void TestMethodsRunOnce()
        {
            TestClass2.Executed = new[] {0, 0, 0};
            TestSystem.RunTests("../../../../TestProjects/TestProject2/bin");
            TestClass2.Executed.ShouldAllBe(count => count == 1);
        }

        [Test]
        public void TestMethodsRunInParallel()
        {
            TestSystem.RunTests("../../../../TestProjects/TestProject3/bin");
            TestClass3.Threads.ShouldBeUnique();
        }

        [Test]
        public void BeforeAndAfterMethodsRunProperly()
        {
            TestClass4.BeforeExecuted = new[] {0, 0};
            TestClass4.AfterExectured = new[] {0};
            
            TestSystem.RunTests("../../../../TestProjects/TestProject4/bin");
            
            TestClass4.BeforeExecuted.ShouldBe(new[] {2, 2});
            TestClass4.AfterExectured.ShouldBe(new[] {2});
        }
        
        [Test]
        public void BeforeAndTestMethodsRunOnTheSameInstance()
        {
            var instance1 = new TestClass10();
            var instance2 = new TestClass10();
            instance1.GetHashCode().ShouldNotBe(instance2.GetHashCode());
            
            TestSystem.RunTests("../../../../TestProjects/TestProject10/bin");
            
            TestClass10.BeforeHash.ShouldBe(TestClass10.TestHash);
        }

        [Test]
        public void AfterAndTestMethodsRunOnTheSameInstance()
        {
            var instance1 = new TestClass10();
            var instance2 = new TestClass10();
            instance1.GetHashCode().ShouldNotBe(instance2.GetHashCode());

            TestSystem.RunTests("../../../../TestProjects/TestProject10/bin");
            TestClass10.AfterHash.ShouldBe(TestClass10.TestHash);
        }

        [Test]
        public void BeforeClassAndAfterClassMethodsRunProperly()
        {
            TestClass5.BeforeClassExecuted = new[] {0, 0};
            TestClass5.AfterClassExectured = new[] {0};
            
            TestSystem.RunTests("../../../../TestProjects/TestProject5/bin");
            
            TestClass5.BeforeClassExecuted.ShouldBe(new[] {1, 1});
            TestClass5.AfterClassExectured.ShouldBe(new[] {1});
        }
    }
}