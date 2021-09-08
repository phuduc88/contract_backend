using System.Collections.Generic;

namespace Contract.API.Constants
{
    public static class CustomHttpRequestHeader
    {
        public const string AuthorizationToken = "X-Authorization-Token";
        public const string AuthorizationToken_Connection = "X-Authorization-Connection";
        public const string CollectionTotal = "X-Collection-Total";
        public const string CollectionSkip = "X-Collection-Skip";
        public const string CollectionTake = "X-Collection-Take";
        public const string AccessControlExposeHeadersContent = "X-Collection-Total, X-Collection-Skip, X-Collection-Take";
        public const string AccessControlExposeHeaders = "Access-Control-Expose-Headers";
    }

    public static class FileUpload
    {
        public const string FileExtension = ".zip";
    }

    public static class Characters
    {
        public const string Slash = @"/";
        public const string Underscore = "_";
    }
    public static class Formatter
    {
        public const string DateTimeFormat = "yyyyMMddHHmmss";
    }

    public static class FilterPattern
    {
        public const string AllValues = "*";
    }    
}