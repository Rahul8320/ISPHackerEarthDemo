using System.Net;

namespace ISPHackerEarth.Application.Common;

public class ServiceResult
{
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    public string Message { get; set; } = string.Empty;
}

public class ServiceResult<T> : ServiceResult where T : class
{
    public T Data { get; set; } = default!;
}
