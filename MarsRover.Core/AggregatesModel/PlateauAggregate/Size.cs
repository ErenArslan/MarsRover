using MarsRover.Core.SeedWork;
using System.Collections.Generic;

namespace MarsRover.Core.AggregatesModel.PlateauAggregate
{
    public class Size : ValueObject
    {
        public int Width { get;private set; }
        public int Height { get; private set; }

      


        public Size(int width,int height)
        {
            Width = width;
            Height = height;
        }

        protected override IEnumerable<object> GetFields()
        {
            yield return Width;
            yield return Height;
        }
    }
}
