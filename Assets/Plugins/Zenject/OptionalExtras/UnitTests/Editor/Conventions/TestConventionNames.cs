
#if !(UNITY_WSA && ENABLE_DOTNET)

using NUnit.Framework;
using Assert = ModestTree.Assert;

namespace Zenject.Tests.Convention.Names
{
    [TestFixture]
    public class TestConventionNames : ZenjectUnitTestFixture
    {
        [Test]
        public void TestWithSuffix()
        {
            Container.Bind<IController>()
                .To(x => x.AllNonAbstractClasses().InNamespace("Zenject.Tests.Convention.Names").WithSuffix("UIPlayer")).AsTransient();

            Assert.That(Container.Resolve<IController>() is FooController);
        }

        [Test]
        public void TestWithPrefix()
        {
            Container.Bind<IController>()
                .To(x => x.AllTypes().InNamespace("Zenject.Tests.Convention.Names").WithPrefix("UIPlayer")).AsTransient();

            Assert.That(Container.Resolve<IController>() is ControllerBar);
        }

        [Test]
        public void TestMatchingRegex()
        {
            Container.Bind<IController>()
                .To(x => x.AllNonAbstractClasses().InNamespace("Zenject.Tests.Convention.Names").MatchingRegex("UIPlayer$")).AsTransient();

            Assert.That(Container.Resolve<IController>() is FooController);
        }

        interface IController
        {
        }

        class FooController : IController
        {
        }

        class ControllerBar : IController
        {
        }

        class QuxControllerAsdf : IController
        {
        }

        class IgnoredFooController
        {
        }

        class ControllerBarIgnored
        {
        }
    }
}

#endif
