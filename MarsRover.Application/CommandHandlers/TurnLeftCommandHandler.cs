using MarsRover.Application.Commands;
using MarsRover.Core.AggregatesModel.RoverAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MarsRover.Application.CommandHandlers
{
    public class TurnLeftCommandHandler : IRequestHandler<TurnLeftCommand>
    {
        private readonly IRoverRepository _roverRepository;
        public TurnLeftCommandHandler(IRoverRepository roverRepository)
        {
            _roverRepository = roverRepository;
        }
        public async Task<Unit> Handle(TurnLeftCommand request, CancellationToken cancellationToken)
        {
            var rover = await _roverRepository.GetAsync(request.RoverId);
            rover.TurnLeftRover();
            await _roverRepository.UpdateAsync(rover);

            return Unit.Value;
        }

       
    }
}
