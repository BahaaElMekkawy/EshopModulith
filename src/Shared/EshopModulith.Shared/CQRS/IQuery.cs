using MediatR;

namespace EshopModulith.Shared.CQRS
{
    public interface IQuery<out T> : IRequest<T> where T : notnull
    {
    }
}
