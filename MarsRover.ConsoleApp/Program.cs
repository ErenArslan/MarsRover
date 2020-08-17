using MarsRover.Application.CommandHandlers;
using MarsRover.Application.Commands;
using MarsRover.Application.Extensions;
using MarsRover.Core.AggregatesModel.PlateauAggregate;
using MarsRover.Core.AggregatesModel.RoverAggregate;
using MarsRover.Infrastructure.Repositories;
using MarsRover.Infrastructure.Settings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MarsRover.ConsoleApp
{
    class Program
    {
        public static IConfigurationRoot configuration;

        static async Task Main(string[] args)
        {
            var serviceProvider = Initialize();
            var _mediator = serviceProvider.GetRequiredService<IMediator>();
            var _roverRepository = serviceProvider.GetRequiredService<IRoverRepository>();
            var _commands = new List<IRequest>();


            Console.WriteLine("Plateau Size:");
            var plateauSize = Console.ReadLine();

            var plateauId = Guid.NewGuid();
            var createPlateauCommand = new CreatePlateauCommand(plateauSize, plateauId);
            _commands.Add(createPlateauCommand);

            do
            {
                Console.WriteLine("Rover Position:");
                var roverPosition = Console.ReadLine();
                var roverId = Guid.NewGuid();
                var placeRoverCommand = new PlaceRoverCommand(roverPosition, plateauId, roverId);
                _commands.Add(placeRoverCommand);

                Console.WriteLine("Rover Directions:");
                var roverDirections = Console.ReadLine().Replace(" ","");
                var directionCommands= roverDirections.ToMoveCommands(roverId);
                _commands.AddRange(directionCommands);

                Console.WriteLine("Do you want to continue ? (Y/N)");
                var result = Console.ReadLine();
                if (result.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                {
                    break;
                }

            } while (true);


            foreach (var command in _commands)
            {
                await _mediator.Send(command);
            }


            var _rovers = await _roverRepository.GetAll();

            foreach (var rover in _rovers)
            {
                Console.WriteLine($"{rover.Position.X} {rover.Position.Y} {rover.Position.Direction}");
            }

            await _roverRepository.FlushAsync();

            Console.WriteLine("---------------");
            Console.ReadKey();
        }



        static IServiceProvider Initialize()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;



        }
        private static void ConfigureServices(IServiceCollection services)
        {
            configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
               .AddJsonFile("appsettings.json", false)
               .Build();


            services.AddSingleton<IConfigurationRoot>(configuration);


            services.AddMediatR(typeof(CreatePlateauCommandHandler));
            services.Configure<MongoDbSettings>(options =>
            {
                options.ConnectionString
                    = configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.Database
                    = configuration.GetSection("MongoConnection:Database").Value;
            });



            services.AddScoped<IRoverRepository, RoverRepository>();
            services.AddScoped<IPlateauRepository, PlateauRepository>();



        }

    }
}
