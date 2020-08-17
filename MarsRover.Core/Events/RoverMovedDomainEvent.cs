using MarsRover.Core.AggregatesModel.PlateauAggregate;
using MediatR;
using System;

namespace MarsRover.Core.Events
{
    public class RoverMovedDomainEvent :INotification
    {
        public Direction Direction { get;  }
        public Guid  RoverId { get; }
        public RoverMovedDomainEvent(Direction direction,Guid roverId)
        {
            Direction = direction;
            RoverId = roverId;
        }
    }
}
