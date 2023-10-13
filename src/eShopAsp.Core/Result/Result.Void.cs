namespace eShopAsp.Core.Result;

public class Result : Result<Result>
{
    public  Result() : base() {}
    protected internal Result(ResultStatus resultStatus) : base(resultStatus) {}

    public static Result Success() => new();
    public static Result SuccessWithMessage(string successMessage) => new() { SuccessMessage = successMessage };
    public static Result<T> Success<T>(T value) => new(value);
    public static Result<T> Success<T>(T value, string successMessage) => new(value, successMessage);
    public new static Result Error(params string[] errorMessages) => new(ResultStatus.Error) { Errors = errorMessages };
    public static Result ErrorWithCorrelationId(string correlationId, params string[] errorMessages)
        => new(ResultStatus.Error) { CorrelationId = correlationId, Errors = errorMessages };
    
    public new static Result Invalid(ValidationError validationError) => new(ResultStatus.Invalid) {ValidationErrors = {validationError}}; 
    public new static Result Invalid(List<ValidationError> validationErrors) => new(ResultStatus.Invalid) {ValidationErrors = validationErrors};
    public new static Result NotFound() => new(ResultStatus.NotFound);
    public new static Result NotFound(params string[] errorMessages) => new(ResultStatus.NotFound) {Errors = errorMessages};
    public new static Result Forbidden() => new(ResultStatus.ForBidden);
    public new static Result Unauthorized() => new(ResultStatus.Unauthorized);
    public new static Result Conflict() => new(ResultStatus.Conflict);
    public new static Result Conflict(params string[] errorMessages) =>  new(ResultStatus.Conflict){Errors = errorMessages};
    public new static Result Unavailable(params string[] errorMessages) => new(ResultStatus.Unavailable) {Errors = errorMessages};
    public new static Result CriticalError(params string[] errorMessages) => new(ResultStatus.CriticalError) {Errors = errorMessages};
}