using FluentValidation;
using paramore.brighter.commandprocessor;
using paramore.brighter.commandprocessor.Logging;
using StructureMap;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure.Validation
{
    internal class ValidateHandler<T> : RuntimeRequestHandler<T> where T : class, IRequest
    {
        private readonly IContainer container;

        public ValidateHandler(IProvideTransactions transaction, IExecutionEngine engine, IContainer container, ILog log) : base(transaction, engine, log)
        {
            this.container = container;
        }

        public override T Handle(T msg)
        {
            var validator = container.TryGetInstance<AbstractValidator<T>>();

            if (validator != null)
            {
                validator.ValidateAndThrow(msg);
            }

            return base.Handle(msg);
        }
    }
}
