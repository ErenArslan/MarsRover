using MarsRover.Application.Commands;
using MarsRover.Core.AggregatesModel.PlateauAggregate;
using MarsRover.Core.AggregatesModel.RoverAggregate;
using MarsRover.Core.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MarsRover.Application.CommandHandlers
{
    public class ForwardCommandHandler : IRequestHandler<ForwardCommand>
    {
        private readonly IRoverRepository _roverRepository;
        private readonly IPlateauRepository _plateauRepository;
        public ForwardCommandHandler(IRoverRepository roverRepository, IPlateauRepository plateauRepository)
        {
            _roverRepository = roverRepository;
            _plateauRepository = plateauRepository;
        }
        public async Task<Unit> Handle(ForwardCommand request, CancellationToken cancellationToken)
        {
            var rover = await _roverRepository.GetAsync(request.RoverId);
            rover.ForwardRover();



            var plateu = await _plateauRepository.GetAsync(rover.PlateauId);
          
            if (rover.Position.X > plateu.Size.Width ||
               rover.Position.X < 0 ||
               rover.Position.Y > plateu.Size.Height ||
               rover.Position.Y < 0)
            {
                throw new MarsRoverDomainException("Out Of Boundiries");
            }

            await _roverRepository.UpdateAsync(rover);

            return Unit.Value;
        }
    }
}
