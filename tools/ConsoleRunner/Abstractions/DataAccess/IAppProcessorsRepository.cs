using System.Collections.Generic;
using ConsoleRunner.Model;

namespace ConsoleRunner.Abstractions.DataAccess;

public interface IAppProcessorsRepository
{
    IList<AppProcessor> GetProcessorsForMachine(string machine);
}