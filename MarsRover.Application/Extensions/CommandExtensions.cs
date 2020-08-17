using MarsRover.Application.Commands;
using MarsRover.Core.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;

namespace MarsRover.Application.Extensions
{
    public static class CommandExtensions
    {
        public static IEnumerable<IRequest> ToMoveCommands(this string commandInput, Guid roverId)
        {
            foreach (Char letter in commandInput.ToCharArray())
            {
               
                switch (char.ToUpper(letter))
                {
                    case 'L':
                        yield return new TurnLeftCommand(roverId);
                        break;

                    case 'R':
                        yield return new TurnRightCommand(roverId);
                        break;

                    case 'M':
                        yield return new ForwardCommand(roverId);
                        break;

                    default:
                        throw new MarsRoverDomainException("Invalid movement command.");
                }
            }
        }
    }
}
