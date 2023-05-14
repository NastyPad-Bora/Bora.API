namespace Bora.API.Shared.Domain.Service;

public class BaseResponse<TEntity>
{
    public TEntity? Resource { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }

    public BaseResponse(TEntity? resource)
    {
        Message = "Success";
        Resource = resource;
        Success = true;
    }

    public BaseResponse(string message)
    {
        Success = false;
        Message = message;
        Resource = default;
    }
}