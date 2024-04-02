using FluentValidation.Results;

namespace Game.Application.Contracts.Core
{
    public abstract class BaseRequest
    {
        public abstract ValidationResult Validate();
    }
}
