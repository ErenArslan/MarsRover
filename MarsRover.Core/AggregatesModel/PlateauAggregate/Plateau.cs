using MarsRover.Core.Events;
using MarsRover.Core.SeedWork;
using System;

namespace MarsRover.Core.AggregatesModel.PlateauAggregate
{
    public class Plateau : Entity, IAggregateRoot
    {
        public Size Size { get;private set; }


      
        public Plateau(Guid id):base(id)
        {
           
        }


    
        public void Initialize(Size size)
        {
            Size = size;
            AddDomainEvent(new PlateauInitializedDomainEvent(size));
        }

      
    }
}
