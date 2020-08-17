using MarsRover.Application.CommandHandlers;
using MarsRover.Application.Commands;
using MarsRover.Core.AggregatesModel.PlateauAggregate;
using MarsRover.Core.AggregatesModel.RoverAggregate;
using MarsRover.Core.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MarsRover.Test.Application
{
  public  class ForwardCommandTests
    {
        [Fact]
      
        public async void ForwardCommandHandler_OutOfBoundries_ThrowException()
        {

            var plateauRepo = new Mock<IPlateauRepository>();
            var roverRepo = new Mock<IRoverRepository>();

            var plateauId = Guid.NewGuid();
            var roverId = Guid.NewGuid();

            plateauRepo.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(LoadPlateau(plateauId,2, 2));
            roverRepo.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(LoadRover(roverId,plateauId));

            var command = new ForwardCommand(roverId);
            var sut = new ForwardCommandHandler(roverRepo.Object, plateauRepo.Object);


            await Assert.ThrowsAsync<MarsRoverDomainException>(async () => await sut.Handle(command, CancellationToken.None));


        }

       

        async Task<Plateau> LoadPlateau(Guid plateauId,int width, int height)
        {

            var t = Task.Run(() =>
            {
                var plateau = new Plateau(plateauId);
                plateau.Initialize(new Size(width, height));

                return plateau;
            });

            return await t;

        }

        async Task<Rover> LoadRover(Guid roverId, Guid plateauId)
        {

            var t = Task.Run(() =>
            {
                var rover = new Rover(roverId);
                rover.PlaceRover(new Position(1, 2, Direction.N), plateauId);
                return rover;
            });

            return await t;

        }
    }
}
