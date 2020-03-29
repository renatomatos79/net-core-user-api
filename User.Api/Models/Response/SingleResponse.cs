namespace User.Api.Models.Response
{
    public class SingleResponse<T> : BaseResponse where T : class
    {
        public T Data { get; set; }
    }
}
