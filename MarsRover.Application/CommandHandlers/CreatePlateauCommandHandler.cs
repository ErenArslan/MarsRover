using MarsRover.Application.Commands;
using MarsRover.Core.AggregatesModel.PlateauAggregate;
using MarsRover.Core.Exceptions;
using MediatR;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MarsRover.Application.CommandHandlers
{
    public class CreatePlateauCommandHandler : IRequestHandler<CreatePlateauCommand>
    {
        private readonly IPlateauRepository _plateauRepository;
        public CreatePlateauCommandHandler(IPlateauRepository plateauRepository)
        {
            _plateauRepository = plateauRepository;
        }
        public async Task<Unit> Handle(CreatePlateauCommand request, CancellationToken cancellationToken)
        {
            var plateau = new Plateau(request.PlateauId);
            var size = ParseSize(request.PlateauSizeInput);
            plateau.Initialize(size);

            await _plateauRepository.SaveAsync(plateau);
            return Unit.Value;
        }

        private Size ParseSize(string commandInput)
        {
            if (new Regex(@"^\d+ \d+$").Match(commandInput).Success==false)
                throw new MarsRoverDomainException("Command Input could not resolved.");

            Size size = null;
            var commandLetters = commandInput.Split(' ');


            if (int.TryParse(commandLetters[0], out int width))
            {
                if (int.TryParse(commandLetters[1], out int height))
                {
                    size = new Size(width, height);
                }
            }

            return size;


        }
    }
}
