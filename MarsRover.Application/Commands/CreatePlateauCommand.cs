using MediatR;
using System;

namespace MarsRover.Application.Commands
{
    public class CreatePlateauCommand: IRequest
    {
        public string PlateauSizeInput { get;  }

        public Guid PlateauId { get;  }
        public CreatePlateauCommand(string plateauSizeInput, Guid plateauId)
        {
            PlateauSizeInput = plateauSizeInput;
            PlateauId = plateauId;
        }

    }
}
