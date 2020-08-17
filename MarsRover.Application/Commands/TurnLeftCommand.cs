using MediatR;
using System;

namespace MarsRover.Application.Commands
{
    public class TurnLeftCommand : IRequest
    {
        public Guid RoverId { get; }

        public TurnLeftCommand(Guid roverId)
        {
            RoverId = roverId;
        }
    }
}