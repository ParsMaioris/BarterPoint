namespace BarterPoint.Domain;
using BarterPoint.Application;

public interface ITransactionRepository : IRepository<Transaction, int, AddTransactionRequest>
{
}