using FluentValidation;
using Trackwane.Framework.Infrastructure.Requests;
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
            Scan(cfg =>
            {
                cfg.AssemblyContainingType<ArchiveVehicle>();

                cfg.AssemblyContainingType<ArchiveVehicleValidator>();

                cfg.AssemblyContainingType<DriverRegisteredListener>();

                cfg.AssemblyContainingType<AlertApiController>();

                cfg.ConnectImplementationsToTypesClosing(typeof(AbstractValidator<>));

                cfg.ConnectImplementationsToTypesClosing(typeof(RuntimeRequestHandler<>));

                cfg.SingleImplementationsOfInterface();
            });
        }
    }
}
