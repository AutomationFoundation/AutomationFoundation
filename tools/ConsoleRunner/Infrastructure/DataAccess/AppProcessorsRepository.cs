using System.Collections.Generic;
using ConsoleRunner.Abstractions.DataAccess;
using ConsoleRunner.Model;

namespace ConsoleRunner.Infrastructure.DataAccess
{
    public class AppProcessorsRepository : IAppProcessorsRepository
    {
        public IList<AppProcessor> GetProcessorsForMachine(string machine)
        {
            return new[]
            {
                new AppProcessor
                {
                    AppProcessorId = 1,
                    IsEnabled = true,
                    Name = "Test 1",
                    ProcessorType = ProcessorTypeEnum.ProducerConsumer
                }
            };
        }
    }
}