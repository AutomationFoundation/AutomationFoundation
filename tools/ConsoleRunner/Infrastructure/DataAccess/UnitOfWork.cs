using System;
using ConsoleRunner.Abstractions.DataAccess;

namespace ConsoleRunner.Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    public IAppProcessorsRepository AppProcessors { get; } = new AppProcessorsRepository();

    ~UnitOfWork()
    {
        Dispose(false);
    }

    public void SaveChanges()
    {
        // This method intentionally left blank.
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
    }
}