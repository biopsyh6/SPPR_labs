namespace WEB_253504_Kolesnikov.UI.Extensions
{
    public static class HttpRequestExtensions
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            return request.Headers["X-requested-with"].ToString().Equals("XMLHttpRequest");
        }
    }
}
