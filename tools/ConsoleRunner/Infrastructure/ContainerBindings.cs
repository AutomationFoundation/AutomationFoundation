using Autofac;
using ConsoleRunner.Infrastructure.DataAccess;

namespace ConsoleRunner.Infrastructure
{
    public class ContainerBindings : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().AsImplementedInterfaces();

            base.Load(builder);
        }
    }
}