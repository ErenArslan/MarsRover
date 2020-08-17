using MarsRover.Core.AggregatesModel.PlateauAggregate;
using MarsRover.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Core.AggregatesModel.RoverAggregate
{
    public class Position : ValueObject
    {
        public int X { get;private set; }
        public int Y { get; private set; }

        public Direction Direction { get; private set; }

        public Position(int x=0,int y=0,Direction direction=Direction.N)
        {
            X = x;
            Y = y;
            Direction = direction;
        }
        protected override IEnumerable<object> GetFields()
        {
            yield return X;
            yield return Y;
            yield return Direction;
        }
    }
}
