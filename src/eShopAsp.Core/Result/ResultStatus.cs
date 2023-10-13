namespace eShopAsp.Core.Result;

public enum ResultStatus
{
    Ok,
    Error,
    ForBidden,
    Unauthorized,
    Invalid,
    NotFound,
    Conflict,
    CriticalError,
    Unavailable
}