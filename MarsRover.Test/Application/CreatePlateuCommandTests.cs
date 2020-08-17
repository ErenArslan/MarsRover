using MarsRover.Application.CommandHandlers;
using MarsRover.Application.Commands;
using MarsRover.Core.AggregatesModel.PlateauAggregate;
using MarsRover.Core.Exceptions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace MarsRover.Test.Application
{
    public class CreatePlateuCommandTests
    {

        [Theory]
        [InlineData(new object[] { "5" })]
        [InlineData(new object[] { "8 A" })]
        [InlineData(new object[] { "A A" })]
        [InlineData(new object[] { "8 7 6" })]
        public async void CreatePlateuCommandHandler_InvalidInputs_ThrowException(string input)
        {

            var repo = new Mock<IPlateauRepository>();
            var plateauId = Guid.NewGuid();
            var command = new CreatePlateauCommand(input, plateauId);
            var sut = new CreatePlateauCommandHandler(repo.Object);


            await Assert.ThrowsAsync<MarsRoverDomainException>(async () => await sut.Handle(command, CancellationToken.None));


        }

        [Theory]
        [InlineData(new object[] { "5 5" })]
        [InlineData(new object[] { "100 100" })]
      
        public async void CreatePlateuCommandHandler_Valid_Success(string input)
        {

            var repo = new Mock<IPlateauRepository>();
            var plateauId = Guid.NewGuid();
            var command = new CreatePlateauCommand(input, plateauId);
            var sut = new CreatePlateauCommandHandler(repo.Object);
            var result = await sut.Handle(command, CancellationToken.None);

            Assert.True(Unit.Value==result);

        }
    }
}
