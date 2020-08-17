using MarsRover.Core.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MarsRover.Application.DomainEventHandlers
{
    public class RoverPlacedDomainEventHandler : INotificationHandler<RoverPlacedDomainEvent>
    {
        public async Task Handle(RoverPlacedDomainEvent notification, CancellationToken cancellationToken)
        {  
            //For EventSourcing and Integration Events. I.E publish to queue via RabbitMq or Kafka

            await Task.CompletedTask;
        }
    }
}
