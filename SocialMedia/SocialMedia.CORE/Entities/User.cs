﻿namespace SocialMedia.CORE.Entities
{
    using System;
    using System.Collections.Generic;

    public partial class User:BaseEntity
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Post = new HashSet<Post>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Post> Post { get; set; }
    }
}
