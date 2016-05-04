using FluentValidation;
using Trackwane.Data.Domain;
using Trackwane.Data.Engine.Commands;
using Trackwane.Data.Engine.Handlers;
using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Data.Engine
{
    public class Registry : StructureMap.Registry
    {
        public Registry()
        {
            Scan(cfg =>
            {
                cfg.AssemblyContainingType<SensorReading>();

                cfg.AssemblyContainingType<SaveSensorReadingHandler>();

                cfg.AssemblyContainingType<SaveSensorReading>();

                cfg.ConnectImplementationsToTypesClosing(typeof(AbstractValidator<>));

                cfg.ConnectImplementationsToTypesClosing(typeof(RuntimeRequestHandler<>));
            });
        }
    }
}
