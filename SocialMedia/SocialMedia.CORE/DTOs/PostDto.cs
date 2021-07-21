namespace SocialMedia.CORE.DTOs
{
    using System;

    public class PostDto
    {
        /// <summary>
        /// Autogenerador id para entidad post
        /// </summary>
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
