namespace Companies.API.Responses
{
    //public abstract class BaseResponse
    //{
    //    public bool Success { get; }
    //    protected BaseResponse(bool success) => Success = success;

    //}
    public abstract record BaseResponse(bool Success);

    //public sealed class OkResponse<T> : BaseResponse
    //{
    //    public T Result { get; }
    //    public OkResponse(T result) : base(true) => Result = result;

    //} 
    public sealed record OkResponse<T>(T Result) : BaseResponse(true);

    public abstract record NotFoundResponse(string Message) : BaseResponse(false);
    public record CompanyNotFoundResponse(Guid Id) : NotFoundResponse($"The company with id: {Id} dosen't exists");
}
