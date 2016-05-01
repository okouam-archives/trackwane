using System;
using FluentValidation;
using FluentValidation.Results;

namespace Trackwane.Framework.Infrastructure
{
    public class BaseValidator
    {
        protected static void ValidateAndThrow(Func<ValidationResult> action)
        {
            var result = action();

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
