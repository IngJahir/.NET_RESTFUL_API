namespace SocialMedia.INFRASTRUCTURE.Services
{
    using SocialMedia.CORE.QueryFilters;
    using SocialMedia.INFRASTRUCTURE.Interfaces;
    using System;

    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
    }
}
