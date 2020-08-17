using MarsRover.Application.CommandHandlers;
using MarsRover.Application.Commands;
using MarsRover.Core.AggregatesModel.PlateauAggregate;
using MarsRover.Core.AggregatesModel.RoverAggregate;
using MarsRover.Core.Exceptions;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MarsRover.Test.Application
{
    public class PlaceRoverCommandTests
    {
        [Theory]
        [InlineData(new object[] { "1 5 5" })]
        [InlineData(new object[] { "1 H 2" })]
        [InlineData(new object[] { "8 A" })]
        [InlineData(new object[] { "A A" })]
        [InlineData(new object[] { "8 7 K" })]
        public async void PlaceRoverCommandHandler_InvalidInputs_ThrowException(string input)
        {

            var plateauRepo = new Mock<IPlateauRepository>();
            var roverRepo = new Mock<IRoverRepository>();

            plateauRepo.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(LoadPlateau(5,5));

            var plateauId = Guid.NewGuid();
            var roverId = Guid.NewGuid();
            var command = new PlaceRoverCommand(input, plateauId, roverId);
            var sut = new PlaceRoverCommandHandler(roverRepo.Object, plateauRepo.Object);


            await Assert.ThrowsAsync<MarsRoverDomainException>(async () => await sut.Handle(command, CancellationToken.None));


        }

        [Theory]
        [InlineData(new object[] { "1 5 N" })]
      
        public async void PlaceRoverCommandHandler_OutOfBoundries_ThrowException(string input)
        {

            var plateauRepo = new Mock<IPlateauRepository>();
            var roverRepo = new Mock<IRoverRepository>();

            plateauRepo.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(LoadPlateau(5, 4));

            var plateauId = Guid.NewGuid();
            var roverId = Guid.NewGuid();
            var command = new PlaceRoverCommand(input, plateauId, roverId);
            var sut = new PlaceRoverCommandHandler(roverRepo.Object, plateauRepo.Object);


            await Assert.ThrowsAsync<MarsRoverDomainException>(async () => await sut.Handle(command, CancellationToken.None));


        }

        [Theory]
        [InlineData(new object[] { "1 2 N" })]
        [InlineData(new object[] { "4 3 W" })]

        public async void PlaceRoverCommandHandler_Valid_Success(string input)
        {

            var plateauRepo = new Mock<IPlateauRepository>();
            var roverRepo = new Mock<IRoverRepository>();

            plateauRepo.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(LoadPlateau(5, 5));

            var plateauId = Guid.NewGuid();
            var roverId = Guid.NewGuid();
            var command = new PlaceRoverCommand(input, plateauId, roverId);
            var sut = new PlaceRoverCommandHandler(roverRepo.Object, plateauRepo.Object);
            var result= await sut.Handle(command, CancellationToken.None);


            Assert.True(Unit.Value == result);

        }


      async Task<Plateau> LoadPlateau(int width, int height)
        {

            var t = Task.Run(() =>
            {
                var plateau = new Plateau(Guid.NewGuid());
            plateau.Initialize(new Size(width, height));

            return plateau;
            });

            return await t;

        }

    }
}
