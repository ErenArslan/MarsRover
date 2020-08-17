using MarsRover.Application.CommandHandlers;
using MarsRover.Application.Commands;
using MarsRover.Core.AggregatesModel.PlateauAggregate;
using MarsRover.Core.AggregatesModel.RoverAggregate;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MarsRover.Test.Application
{
    public class TurnLeftCommandTests
    {
        [Fact]

        public async void TurnLeftCommandHandler_ShoulBeWest_Success()
        {

            var roverRepo = new Mock<IRoverRepository>();

            var roverId = Guid.NewGuid();

            roverRepo.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(LoadRover(roverId, Guid.NewGuid()));

            var command = new TurnLeftCommand(roverId);
            var sut = new TurnLeftCommandHandler(roverRepo.Object);
            await sut.Handle(command, CancellationToken.None);

           var rover = await roverRepo.Object.GetAsync(roverId);

            Assert.Equal(rover.Position.Direction, Direction.W);


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
