﻿namespace SocialMedia.CORE.CustomEntities
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MetaData
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public string NextPageUrl { get; set; }
        public string previousPageUrl { get; set; }

    }
}
