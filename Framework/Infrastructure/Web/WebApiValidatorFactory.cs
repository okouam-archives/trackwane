using System;
using System.Web.Http;
using FluentValidation;

namespace Trackwane.Framework.Infrastructure.Web
{
    public class WebApiValidatorFactory : ValidatorFactoryBase
    {
        private readonly HttpConfiguration configuration;

        public WebApiValidatorFactory(HttpConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return configuration.DependencyResolver.GetService(validatorType) as IValidator;
        }
    }
}
