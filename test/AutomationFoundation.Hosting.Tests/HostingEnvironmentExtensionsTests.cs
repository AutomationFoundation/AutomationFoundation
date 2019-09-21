using AutomationFoundation.Hosting.Stubs;
using NUnit.Framework;

namespace AutomationFoundation.Hosting
{
    [TestFixture]
    public class HostingEnvironmentExtensionsTests
    {
        [Test]
        public void ReturnTrueForProduction1()
        {
            var target = new StubHostingEnvironment("Production");
            var result = target.IsProduction();

            Assert.True(result);
        }

        [Test]
        public void ReturnTrueForProduction2()
        {
            var target = new StubHostingEnvironment("Prod");
            var result = target.IsProduction();

            Assert.True(result);
        }

        [Test]
        public void ReturnTrueForProduction3()
        {
            var target = new StubHostingEnvironment("Prod_2");
            var result = target.IsProduction();

            Assert.True(result);
        }

        [Test]
        public void ReturnTrueForProduction4()
        {
            var target = new StubHostingEnvironment("PROD");
            var result = target.IsProduction();

            Assert.True(result);
        }

        [Test]
        public void ReturnTrueForAnyValueOtherThanProd()
        {
            var target = new StubHostingEnvironment("AnyValueOtherThanProd");
            var result = target.IsNonProduction();

            Assert.True(result);
        }

        [Test]
        public void ReturnFalseForProd()
        {
            var target = new StubHostingEnvironment("Prod");
            var result = target.IsNonProduction();

            Assert.False(result);
        }

        [Test]
        public void ReturnTrueForDevelopmentAsNonProduction()
        {
            var target = new StubHostingEnvironment("Dev");
            var result = target.IsNonProduction();

            Assert.True(result);
        }


        [Test]
        public void ReturnTrueForDevelopment1()
        {
            var target = new StubHostingEnvironment("Development");
            var result = target.IsDevelopment();

            Assert.True(result);
        }

        [Test]
        public void ReturnTrueForDevelopment2()
        {
            var target = new StubHostingEnvironment("Dev");
            var result = target.IsDevelopment();

            Assert.True(result);
        }

        [Test]
        public void ReturnTrueForDevelopment3()
        {
            var target = new StubHostingEnvironment("Dev_2");
            var result = target.IsDevelopment();

            Assert.True(result);
        }

        [Test]
        public void ReturnTrueForDevelopment4()
        {
            var target = new StubHostingEnvironment("DEV");
            var result = target.IsDevelopment();

            Assert.True(result);
        }
    }
}