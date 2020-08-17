using MarsRover.Core.AggregatesModel.RoverAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Core.Events
{
    public class RoverPlacedDomainEvent :INotification
    {
        public Position Position { get; }
        public Guid PlateauId { get;  }


        public RoverPlacedDomainEvent(Position position,Guid plateauId)
        {
            Position = position;
            PlateauId = plateauId;
        }
    }
}
