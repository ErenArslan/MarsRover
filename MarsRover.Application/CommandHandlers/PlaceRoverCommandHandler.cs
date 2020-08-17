using MarsRover.Application.Commands;
using MarsRover.Core.AggregatesModel.PlateauAggregate;
using MarsRover.Core.AggregatesModel.RoverAggregate;
using MarsRover.Core.Exceptions;
using MediatR;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MarsRover.Application.CommandHandlers
{
    public class PlaceRoverCommandHandler : IRequestHandler<PlaceRoverCommand>
    {
        private readonly IRoverRepository _roverRepository;
        private readonly IPlateauRepository _plateauRepository;
        public PlaceRoverCommandHandler(IRoverRepository roverRepository, IPlateauRepository plateauRepository)
        {
            _roverRepository = roverRepository;
            _plateauRepository = plateauRepository;
        }
        public async Task<Unit> Handle(PlaceRoverCommand request, CancellationToken cancellationToken)
        {
            var rover = new Rover(request.RoverId);
            var roverPosition = ParsePosition(request.PlaceRoverCommandInput);
            rover.PlaceRover(roverPosition, request.PlateauId);

            var plateu = await _plateauRepository.GetAsync(rover.PlateauId);

            if (rover.Position.X > plateu.Size.Width ||
               rover.Position.X < 0 ||
               rover.Position.Y > plateu.Size.Height ||
               rover.Position.Y < 0)
            {
                throw new MarsRoverDomainException("Out Of Boundiries");
            }




            await _roverRepository.SaveAsync(rover);
            return Unit.Value;
        }

        private Position ParsePosition(string commandInput)
        {

            if (new Regex(@"^\d+ \d+ [NSEW]$").Match(commandInput).Success == false)
                throw new MarsRoverDomainException("Command Input could not resolved.");


            var commandLetters = commandInput.Split(' ');


            int x = int.Parse(commandLetters[0]);
            int y = int.Parse(commandLetters[1]);

            string directionInput = commandLetters[2].ToUpper();
            var direction = (Direction)Enum.Parse(typeof(Direction), directionInput);


            var roverPosition = new Position(x, y, direction);


            return roverPosition;



        }
    }
}
