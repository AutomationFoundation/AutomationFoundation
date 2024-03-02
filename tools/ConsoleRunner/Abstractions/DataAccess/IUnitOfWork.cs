namespace ConsoleRunner.Abstractions.DataAccess;

public interface IUnitOfWork
{
    IAppProcessorsRepository AppProcessors { get; }

    void SaveChanges();
}