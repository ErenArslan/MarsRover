using MediatR;
using System;

namespace MarsRover.Application.Commands
{
    public class TurnRightCommand : IRequest
    {
        public Guid RoverId { get; }

        public TurnRightCommand(Guid roverId)
        {
            RoverId = roverId;
        }
    }
}
