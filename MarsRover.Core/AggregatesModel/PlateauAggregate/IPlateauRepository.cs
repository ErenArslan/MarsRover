using MarsRover.Core.SeedWork;
using System;
using System.Threading.Tasks;

namespace MarsRover.Core.AggregatesModel.PlateauAggregate
{
    public interface IPlateauRepository : IRepository<Plateau>
    {
        Task SaveAsync(Plateau plateau);
        Task<Plateau> GetAsync(Guid plateauId);

    }
}
