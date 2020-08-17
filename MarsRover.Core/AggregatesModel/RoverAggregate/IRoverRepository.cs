using MarsRover.Core.AggregatesModel.PlateauAggregate;
using MarsRover.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarsRover.Core.AggregatesModel.RoverAggregate
{
    public interface IRoverRepository:IRepository<Rover>
    {
        Task SaveAsync(Rover rover);
        Task UpdateAsync(Rover rover);
        Task FlushAsync();
        Task<Rover> GetAsync(Guid roverId);
        Task<IEnumerable<Rover>> GetAll();


    }
}
