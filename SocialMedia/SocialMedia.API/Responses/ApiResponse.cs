namespace SocialMedia.API.Responses
{
    using SocialMedia.CORE.CustomEntities;

    public class ApiResponse<T>
    {
        public ApiResponse(T data)
        {
            Data = data;
        }
        public T Data { get; set; }

        public MetaData Meta { get; set; }
    }
}
