using MediatR;

namespace Aplication.Interfaces
{
    public interface IQuery<TResult> : IRequest<TResult> {} 
}