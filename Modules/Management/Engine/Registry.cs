using FluentValidation;
using MassTransit;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Management.Contracts;
using Trackwane.Management.Engine.Commands.Vehicles;
using Trackwane.Management.Engine.Controllers;
using Trackwane.Management.Engine.Handlers.Vehicles;
using Trackwane.Management.Engine.Listeners.Organizations.Drivers;

namespace Trackwane.Management.Engine
{
    public class Registry : StructureMap.Registry
    {
        public Registry()
        {
            AssemblyScanner
                       .FindValidatorsInAssembly(typeof(_Management_Contracts_Assembly_).Assembly)
                       .ForEach(result => {
                           For(result.InterfaceType).Use(result.ValidatorType);
                       });

            Scan(cfg =>
            {
                cfg.AssemblyContainingType<_Management_Engine_Assembly_>();

                cfg.SingleImplementationsOfInterface();

                cfg.WithDefaultConventions();

                cfg.ConnectImplementationsToTypesClosing(typeof(Handler<>));

                cfg.ConnectImplementationsToTypesClosing(typeof(AbstractValidator<>));

                cfg.ConnectImplementationsToTypesClosing(typeof(RuntimeRequestHandler<>));

                cfg.SingleImplementationsOfInterface();

                cfg.ConnectImplementationsToTypesClosing(typeof(IConsumer<>));
            });
        }
    }
}
