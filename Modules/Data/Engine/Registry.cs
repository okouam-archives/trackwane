using FluentValidation;
using MassTransit;
using Trackwane.Data.Contracts;
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
            AssemblyScanner
                  .FindValidatorsInAssembly(typeof(_Data_Contracts_Assembly_).Assembly)
                  .ForEach(result => {
                      For(result.InterfaceType).Use(result.ValidatorType);
                  });

            Scan(cfg =>
            {
                cfg.AssemblyContainingType<_Data_Engine_Assembly_>();

                cfg.SingleImplementationsOfInterface();

                cfg.WithDefaultConventions();

                cfg.ConnectImplementationsToTypesClosing(typeof(Handler<>));

                cfg.ConnectImplementationsToTypesClosing(typeof(AbstractValidator<>));

                cfg.ConnectImplementationsToTypesClosing(typeof(RuntimeRequestHandler<>));

                cfg.ConnectImplementationsToTypesClosing(typeof(IConsumer<>));
            });
        }
    }
}
