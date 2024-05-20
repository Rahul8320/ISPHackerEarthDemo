using System.Net;

namespace ISPHackerEarth.Domain.Common.Exceptions;

public class ISPException(HttpStatusCode statusCode, Exception exception) : Exception
{
    public HttpStatusCode StatusCode { get; set; } = statusCode;
    public Exception Exception { get; set; } = exception;
}
