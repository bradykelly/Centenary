using System.Runtime.Serialization;

namespace Centenary.Storage;

public class BlobApiException: Exception
{
    public string? ContainerName { get; set; }
    public string? BlobName { get; set; }
    public string? BlobPrefix { get; set; }

    public BlobApiException()
    {
    }

    protected BlobApiException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public BlobApiException(string? message) : base(message)
    {
    }

    public BlobApiException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
    
    public static void ThrowForBlob(string? message, string? containerName, string? blobName, string? blobPrefix, Exception? innerException = null)
    {
        var ex =  new BlobApiException(message, innerException);
        ex.ContainerName = containerName;
        ex.BlobName = blobName;
        ex.BlobPrefix = blobPrefix;
        throw ex;
    }
}