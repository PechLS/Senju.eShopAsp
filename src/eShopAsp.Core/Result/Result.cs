using System.Text.Json.Serialization;
using eShopAsp.Core.Interfaces.Result;

namespace eShopAsp.Core.Result;

public class Result<T> : IResult
{
    protected Result(){}
    public Result(T value) => Value = value;
    protected internal Result(T value, string successMessage) : this(value) => SuccessMessage = successMessage;
    protected Result(ResultStatus status) => Status = status;

    public static implicit operator T(Result<T> result) => result.Value;
    public static implicit operator Result<T>(T value) => new (value);

    public static implicit operator Result<T>(Result result) => new (default(T))
    {
        Status = result.Status,
        Errors = result.Errors,
        SuccessMessage = result.SuccessMessage,
        CorrelationId = result.CorrelationId,
        ValidationErrors = result.ValidationErrors
    };

    [JsonIgnore] public Type ValueType => typeof(T);
    public ResultStatus Status { get; protected set; } = ResultStatus.Ok;
    public IEnumerable<string> Errors { get; protected set; } = new List<string>();
    public List<ValidationError> ValidationErrors { get; protected set; } = new();
    public bool IsSuccess => Status == ResultStatus.Ok;
    public string SuccessMessage { get; protected set; } = String.Empty;
    public string CorrelationId { get; protected set; } = String.Empty;
    public T Value { get; }

    public object GetValue() => this.Value;

    public PagedResult<T> TpPagedResult(PagedInfo pagedInfo)
    {
        var pagedResult = new PagedResult<T>(pagedInfo, Value)
        {
            Status = Status,
            CorrelationId = CorrelationId,
            Errors = Errors,
            ValidationErrors = ValidationErrors,
            SuccessMessage = SuccessMessage
        };
        return pagedResult;
    }

    public static Result<T> Success(T value) => new (value);
    public static Result<T> Success(T value, string successMessage) => new(value, successMessage);
    public static Result<T> Error(params string[] errorMessages) => new(ResultStatus.Error) { Errors = errorMessages };
    public static Result<T> Invalid(ValidationError validationError) => new(ResultStatus.Invalid) {ValidationErrors = {validationError}};
    public static Result<T> Invalid(List<ValidationError> validationErrors) => new(ResultStatus.Invalid) {ValidationErrors = validationErrors};
    public static Result<T> NotFound() => new(ResultStatus.NotFound);
    public static Result<T> NotFound(params string[] errorMessages) => new(ResultStatus.NotFound) { Errors = errorMessages };
    public static Result<T> Forbidden() => new(ResultStatus.ForBidden);
    public static Result<T> Unauthorized() => new(ResultStatus.Unauthorized);
    public static Result<T> Conflict() => new(ResultStatus.Conflict);
    public static Result<T> Conflict(params string[] errorMessages) => new(ResultStatus.Conflict) {Errors = errorMessages}; 
    public static Result<T> CriticalError(params string[] errorMessages) => new(ResultStatus.CriticalError) {Errors = errorMessages};
    public static Result<T> Unavailable(params string[] errorMessages) => new(ResultStatus.Unavailable) {Errors = errorMessages};
    
}