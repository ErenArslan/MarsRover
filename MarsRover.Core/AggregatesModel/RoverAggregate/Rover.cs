using MarsRover.Core.AggregatesModel.RoverAggregate;
using MarsRover.Core.Events;
using MarsRover.Core.Exceptions;
using MarsRover.Core.SeedWork;
using MediatR;
using System;

namespace MarsRover.Core.AggregatesModel.PlateauAggregate
{
    public class Rover : Entity,IAggregateRoot
    {
        public Position Position { get;private set; }

        public Guid PlateauId { get;private set; }


        public Rover(Guid id):base(id)
        {

        }

        public void SetPlateauId(Guid plateauId)
        {
            PlateauId = plateauId;
        }
        public void PlaceRover(Position position,Guid plateauId)
        {
            Position = position;
            PlateauId = plateauId;
            AddDomainEvent(new RoverPlacedDomainEvent(position, plateauId));
        }

   
        public void TurnRightRover()
        {
            var head =this.Position.Direction + 1 > Direction.W ? Direction.N : this.Position.Direction + 1;
            Position = new Position(this.Position.X, this.Position.Y, head);
            AddDomainEvent(new RoverMovedDomainEvent(Position.Direction,Id));
        }

        public void TurnLeftRover()
        {
            var head = this.Position.Direction - 1 < Direction.N ? Direction.W : this.Position.Direction - 1;
            Position = new Position(this.Position.X, this.Position.Y, head);
            AddDomainEvent(new RoverMovedDomainEvent(Position.Direction, Id));
        }

        public void ForwardRover()
        {
            int tempX = Position.X;
            int tempY = Position.Y;

            switch (Position.Direction)
            {
                case Direction.N:
                    Position = new Position(Position.X, Position.Y+1, Position.Direction);
                    break;

                case Direction.S:
                    Position = new Position(Position.X, Position.Y - 1, Position.Direction);
                    break;
                case Direction.W:
                    Position = new Position(Position.X-1, Position.Y, Position.Direction);
                    break;

                case Direction.E:
                    Position = new Position(Position.X+1, Position.Y, Position.Direction);
                    break;

                default:
                    throw new MarsRoverDomainException("Invalid Direction");
            }
            AddDomainEvent(new RoverMovedDomainEvent(Position.Direction, Id));
           
        }

       



    }
}
