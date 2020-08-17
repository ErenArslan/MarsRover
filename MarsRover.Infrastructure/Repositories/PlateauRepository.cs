using MarsRover.Core.AggregatesModel.PlateauAggregate;
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
    public class PlateauRepository : IPlateauRepository
    {
        private readonly IMediator _mediator;
        private readonly IMongoCollection<Plateau> mongoCollection;
        public PlateauRepository(IOptions<MongoDbSettings> settings, IMediator mediator)
        {
            _mediator = mediator;


            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                var database = client.GetDatabase(settings.Value.Database);
                mongoCollection = database.GetCollection<Plateau>(typeof(Plateau).Name.ToLower());

            }

        }


        public async Task<Plateau> GetAsync(Guid plateauId)
        {
            return await mongoCollection.FindSync(p => p.Id == plateauId).FirstOrDefaultAsync();
        }

        public async Task SaveAsync(Plateau plateau)
        {

            await _mediator.DispatchDomainEventsAsync(plateau);

            await mongoCollection.InsertOneAsync(plateau);
        }

      
    }
}
