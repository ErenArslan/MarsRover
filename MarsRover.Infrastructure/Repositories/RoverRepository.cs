using MarsRover.Core.AggregatesModel.PlateauAggregate;
using MarsRover.Core.AggregatesModel.RoverAggregate;
using MarsRover.Infrastructure.Extensions;
using MarsRover.Infrastructure.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarsRover.Infrastructure.Repositories
{
    public class RoverRepository : IRoverRepository
    {
        private readonly IMediator _mediator;
        private readonly IMongoCollection<Rover> mongoCollection;
        public RoverRepository(IOptions<MongoDbSettings> settings, IMediator mediator)
        {
            _mediator = mediator;


            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                var database = client.GetDatabase(settings.Value.Database);
                mongoCollection = database.GetCollection<Rover>(typeof(Rover).Name.ToLower());

            }

        }

        public async Task FlushAsync()
        {
            await mongoCollection.DeleteManyAsync(p => true);
        }

        public async Task<IEnumerable<Rover>> GetAll()
        {
            return await mongoCollection.Find(p => true).ToListAsync();
        }

        public async Task<Rover> GetAsync(Guid roverId)
        {
            return await mongoCollection.FindSync(p => p.Id == roverId).FirstOrDefaultAsync();
        }

        public async Task SaveAsync(Rover rover)
        {

            await _mediator.DispatchDomainEventsAsync(rover);

            await mongoCollection.InsertOneAsync(rover);
        }

        public async Task UpdateAsync(Rover rover)
        {
            await _mediator.DispatchDomainEventsAsync(rover);

            await mongoCollection.ReplaceOneAsync(p=>p.Id==rover.Id,rover);
        }
    }
}
