namespace SocialMedia.INFRASTRUCTURE.Interfaces
{
    using SocialMedia.CORE.QueryFilters;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IUriService
    {
        Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl);
    }
}
