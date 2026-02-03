using MediatR;

namespace EshopModulith.Shared.Contracts.CQRS
{
    public interface ICommand : IRequest<Unit>
    {
    }
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
