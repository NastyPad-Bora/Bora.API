namespace Bora.API.Shared.Domain.Repository;

public interface IUnitOfWork
{
    Task CompleteAsync();
}