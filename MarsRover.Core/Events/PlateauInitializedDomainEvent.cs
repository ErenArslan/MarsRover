using MarsRover.Core.AggregatesModel.PlateauAggregate;
using MediatR;

namespace MarsRover.Core.Events
{
    public class PlateauInitializedDomainEvent : INotification
    {
        public Size Size { get; }

        public PlateauInitializedDomainEvent(Size size)
        {
            Size = size;
        }
    }
}
