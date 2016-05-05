using System.Reflection;
using FluentValidation;
using MassTransit;
using Trackwane.AccessControl.Contracts;
using Trackwane.AccessControl.Engine.Processors.Handlers.Users;
using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.AccessControl.Engine
{
    public class Registry : StructureMap.Registry
    {
        public Registry()
        {
            AssemblyScanner
                .FindValidatorsInAssembly(typeof(_Access_Control_Messages_Assembly_).Assembly)
                .ForEach(result => { 
                    For(result.InterfaceType).Use(result.ValidatorType);
                });

            Scan(cfg =>
            {
                cfg.AssemblyContainingType<_Access_Control_Engine_Assembly_>();

                cfg.SingleImplementationsOfInterface();

                cfg.WithDefaultConventions();

                cfg.ConnectImplementationsToTypesClosing(typeof(AbstractValidator<>));

                cfg.ConnectImplementationsToTypesClosing(typeof(IConsumer<>));
            });
        }
    }
}
