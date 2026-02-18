namespace Domain.Primitives.Domain.Interaces
{
    // This interface defines the contract for a unit of work, which is a design pattern used to group multiple operations into a single transaction.
    // Gaining atomicity and consistency when performing multiple operations that should either all succeed or all fail together.
   public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}