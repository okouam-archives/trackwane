using FluentValidation;
using paramore.brighter.commandprocessor;
using Trackwane.AccessControl.Engine.Processors.Handlers.Users;
using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.AccessControl.Engine
{
    public class Registry : StructureMap.Registry
    {
        public Registry()
        {
            Scan(cfg =>
            {
                cfg.AssemblyContainingType<RegisterUserValidator>();

                cfg.SingleImplementationsOfInterface();

                cfg.WithDefaultConventions();

                cfg.ConnectImplementationsToTypesClosing(typeof(AbstractValidator<>));

                cfg.ConnectImplementationsToTypesClosing(typeof(RequestHandler<>));

                cfg.ConnectImplementationsToTypesClosing(typeof(RuntimeRequestHandler<>));
            });
        }
    }
}
