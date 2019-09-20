namespace AutomationFoundation.Hosting.Stubs
{
    public class StubHostingEnvironment : HostingEnvironment
    {
        public StubHostingEnvironment(string environmentName)
        {
            EnvironmentName = environmentName;
        }

        public override string EnvironmentName { get; }
    }
}