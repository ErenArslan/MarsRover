using MediatR;
using System;

namespace MarsRover.Application.Commands
{
    public class PlaceRoverCommand : IRequest
    {
        public string PlaceRoverCommandInput { get;  }
        public Guid PlateauId { get;  }
        public Guid RoverId { get; }
        public PlaceRoverCommand(string placeRoverCommandInput,Guid plateauId,Guid roverId)
        {
            PlaceRoverCommandInput = placeRoverCommandInput;
            PlateauId = plateauId;
            RoverId = roverId;
        }
    }
}
