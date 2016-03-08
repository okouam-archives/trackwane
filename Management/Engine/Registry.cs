using FluentValidation;
using paramore.brighter.commandprocessor;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Management.Commands.Vehicles;
using Trackwane.Management.Handlers.Vehicles;
using Trackwane.Management.Listeners.Organizations.Drivers;
using Trackwane.Management.Server.Controllers;

namespace Trackwane.Management.Engine
{
    public class Registry : StructureMap.Registry
    {
        public Registry()
        {
            Scan(cfg =>
            {
                cfg.AssemblyContainingType<ArchiveVehicle>();

                cfg.AssemblyContainingType<ArchiveVehicleValidator>();

                cfg.AssemblyContainingType<DriverRegisteredListener>();

                cfg.AssemblyContainingType<AlertApiController>();

                cfg.ConnectImplementationsToTypesClosing(typeof(AbstractValidator<>));

                cfg.ConnectImplementationsToTypesClosing(typeof(RequestHandler<>));

                cfg.ConnectImplementationsToTypesClosing(typeof(RuntimeRequestHandler<>));

                cfg.SingleImplementationsOfInterface();
            });
        }
    }
}
