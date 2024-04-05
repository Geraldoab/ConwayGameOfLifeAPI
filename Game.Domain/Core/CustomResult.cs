using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace Game.Infra.Data.Core
{
    public class CustomResult
    {
        public CustomResult()
        {
        }

        public IEnumerable<string>? Errors { get; }
        public bool Success { get; }
        public bool Failure => !Success;
        public HttpStatusCode? CurrentHttpStatusCode { get; }

        internal static class InternalErrorMessages
        {
            public static readonly string ErrorObjectIsNotProvidedForFailure =
                "Error when trying to create a custom failure result.";

            public static readonly string ErrorObjectIsProvidedForSuccess =
                "Error when trying to create a custom success result.";
        }

        public dynamic? Data { get; set; }
        private static readonly CustomResult OkResult = new CustomResult(true, Enumerable.Empty<string>());

        public CustomResult(HttpStatusCode? httpStatusCode = null)
        {
            CurrentHttpStatusCode = httpStatusCode;
        }

        public CustomResult(dynamic data, HttpStatusCode? httpStatusCode = null)
        {
            Success = true;
            Data = data;
            CurrentHttpStatusCode = httpStatusCode;
        }

        public CustomResult(bool isSuccess, IEnumerable<string> errorMessages, HttpStatusCode? httpStatusCode = null)
        {
            bool doNotExistsErrorMessage = errorMessages.Count() == 0;
            bool doExistsErrorMessage = !doNotExistsErrorMessage;

            if (isSuccess)
            {
                if (doExistsErrorMessage)
                    throw new ArgumentException(
                        InternalErrorMessages.ErrorObjectIsProvidedForSuccess,
                        nameof(errorMessages));
            }
            else
            {
                if (doNotExistsErrorMessage)
                    throw new ArgumentNullException(
                        nameof(errorMessages),
                        InternalErrorMessages.ErrorObjectIsNotProvidedForFailure);
            }

            Success = isSuccess;
            Errors = errorMessages;
            CurrentHttpStatusCode = httpStatusCode;
        }


        public static CustomResult Ok(dynamic? response = null, HttpStatusCode? httpStatusCode = null)
        {
            return response != null ? new CustomResult(response, httpStatusCode) : new CustomResult(true, Enumerable.Empty<string>(), httpStatusCode);
        }

        public static CustomResult Fail(string errorMessage, HttpStatusCode? httpStatusCode = null)
        {
            return new CustomResult(false, new List<string> { errorMessage }, httpStatusCode: httpStatusCode);
        }

        public static CustomResult Fail(IEnumerable<string> errorMessages)
        {
            return new CustomResult(false, errorMessages);
        }

        public static CustomResult Fail(ValidationResult errorMessages)
        {
            List<string> erros = new List<string>();
            foreach (var error in errorMessages.Errors)
            {
                erros.Add(error.ErrorMessage);
            }
            return new CustomResult(false, erros);
        }

        public static CustomResult Fail(ModelStateDictionary modelState)
        {
            List<string> erros = new List<string>();
            var ms = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in ms)
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                erros.Add(errorMsg);
            }
            return new CustomResult(false, erros);
        }
    }
}
