using MediatR;

namespace Aplication.Interfaces
{
    public interface ICommand<TResult> : IRequest<TResult> {}
}