namespace AutomationFoundation.Hosting.Stubs
{
    public class TestableDefaultHostingEnvironment : DefaultHostingEnvironment
    {
        private readonly string environmentName;

        public TestableDefaultHostingEnvironment(string environmentName)
        {
            this.environmentName = environmentName;
        }

        protected override string GetEnvironmentVariable(string variable)
        {
            return environmentName;
        }
    }
}