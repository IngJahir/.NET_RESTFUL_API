using FluentValidation;
using SocialMedia.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.INFRASTRUCTURE.Validators
{
    public class PostValidator: AbstractValidator<PostDto>
    {
        public PostValidator()
        {
            RuleFor(post => post.Description)
                .NotNull()
                .Length(10, 15);

            RuleFor(post => post.Date)
                .NotNull()
                .LessThan(DateTime.Now);
        }

    }
}
