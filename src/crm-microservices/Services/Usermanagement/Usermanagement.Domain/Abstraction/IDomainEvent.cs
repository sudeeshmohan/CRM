using MediatR;

namespace Usermanagement.Domain.Abstraction
{
    public interface IDomainEvent : INotification
    {
        Guid EventId => Guid.NewGuid();
        public DateTime OccuredOn => DateTime.UtcNow;
        public string EventType => GetType().AssemblyQualifiedName ?? string.Empty;
    }
}
