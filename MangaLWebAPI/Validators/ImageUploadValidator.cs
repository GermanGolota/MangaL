using FluentValidation;
using MangaLWebAPI.Mediatr;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangaLWebAPI.Validators
{
    public class ImageUploadValidator : AbstractValidator<ImageUploadCommand>
    {
        public ImageUploadValidator()
        {
            RuleFor(command => command.File)
                .NotNull().WithMessage("Please provide a file")
                .Must(ContainContent).WithMessage("File, that has been send is empty");
        }
        public bool ContainContent(IFormFile file)
        {
            return file is not null && file.Length > 0;
        }
    }
}
