using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace _0_Framework.Application
{
    public class FileExtensionLimitation : ValidationAttribute, IClientModelValidator
    {
        private readonly string[] _validExtensions;

        public FileExtensionLimitation(string[] validExtensions)
        {
            _validExtensions = validExtensions;
        }

        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file == null)
                return true;

            var fileExtension = Path.GetExtension(file.FileName);
            var result = _validExtensions.Contains(fileExtension);
            return result;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val-fileExtensionLimit", ErrorMessage);
        }
    }
}
