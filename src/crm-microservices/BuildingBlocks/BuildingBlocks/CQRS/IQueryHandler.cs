using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface IQueryHandler<in TQuey, TResponse>
        : IRequestHandler<TQuey, TResponse>
        where TQuey : IQuery<TResponse>
        where TResponse : notnull
    {
    }
}
