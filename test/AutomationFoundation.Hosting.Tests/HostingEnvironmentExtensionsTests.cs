using NUnit.Framework;

namespace AutomationFoundation.Hosting
{
    [TestFixture]
    public class HostingEnvironmentExtensionsTests
    {
        [Test]
        public void ReturnTrueForProduction1()
        {
            var target = new HostingEnvironment("Production");
            var result = target.IsProduction();

            Assert.True(result);
        }

        [Test]
        public void ReturnTrueForProduction2()
        {
            var target = new HostingEnvironment("Prod");
            var result = target.IsProduction();

            Assert.True(result);
        }

        [Test]
        public void ReturnTrueForProduction3()
        {
            var target = new HostingEnvironment("Prod_2");
            var result = target.IsProduction();

            Assert.True(result);
        }

        [Test]
        public void ReturnTrueForProduction4()
        {
            var target = new HostingEnvironment("PROD");
            var result = target.IsProduction();

            Assert.True(result);
        }

        [Test]
        public void ReturnTrueForAnyValueOtherThanProd()
        {
            var target = new HostingEnvironment("AnyValueOtherThanProd");
            var result = target.IsNonProduction();

            Assert.True(result);
        }

        [Test]
        public void ReturnFalseForProd()
        {
            var target = new HostingEnvironment("Prod");
            var result = target.IsNonProduction();

            Assert.False(result);
        }

        [Test]
        public void ReturnTrueForDevelopmentAsNonProduction()
        {
            var target = new HostingEnvironment("Dev");
            var result = target.IsNonProduction();

            Assert.True(result);
        }


        [Test]
        public void ReturnTrueForDevelopment1()
        {
            var target = new HostingEnvironment("Development");
            var result = target.IsDevelopment();

            Assert.True(result);
        }

        [Test]
        public void ReturnTrueForDevelopment2()
        {
            var target = new HostingEnvironment("Dev");
            var result = target.IsDevelopment();

            Assert.True(result);
        }

        [Test]
        public void ReturnTrueForDevelopment3()
        {
            var target = new HostingEnvironment("Dev_2");
            var result = target.IsDevelopment();

            Assert.True(result);
        }

        [Test]
        public void ReturnTrueForDevelopment4()
        {
            var target = new HostingEnvironment("DEV");
            var result = target.IsDevelopment();

            Assert.True(result);
        }
    }
}