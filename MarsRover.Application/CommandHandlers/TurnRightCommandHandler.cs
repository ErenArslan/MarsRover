using MarsRover.Application.Commands;
using MarsRover.Core.AggregatesModel.RoverAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MarsRover.Application.CommandHandlers
{
    public class TurnRightCommandHandler : IRequestHandler<TurnRightCommand>
    {
        private readonly IRoverRepository _roverRepository;
        public TurnRightCommandHandler(IRoverRepository roverRepository)
        {
            _roverRepository = roverRepository;
        }
        public async Task<Unit> Handle(TurnRightCommand request, CancellationToken cancellationToken)
        {
            var rover = await _roverRepository.GetAsync(request.RoverId);
            rover.TurnRightRover();
            await _roverRepository.UpdateAsync(rover);

            return Unit.Value;
        }
    }
}
