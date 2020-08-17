using MediatR;
using System;

namespace MarsRover.Application.Commands
{
    public class ForwardCommand : IRequest
    {
        public Guid RoverId { get; }

        public ForwardCommand(Guid roverId)
        {
            RoverId = roverId;
        }
    }
}
