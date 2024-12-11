using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Presentation.Models.Validators
{
    public static class Validators
    {
        public static ValidatorFn Required()
        {
            var result = new ValidatorFn()
            {
                Message = "This Field Is Required",
                Name = nameof(Validators.Required)
            };
            result.Validator = (val) =>
            {
                if (val != null && !string.IsNullOrWhiteSpace(val.ToString()))
                {
                    return null;
                }
                else
                {
                    return new KeyValuePair<string, string>(result.Name, result.Message);
                }
            };
            return result;
        }

        public static ValidatorFn Email() {

            var result = new ValidatorFn()
            {
                Name = nameof(Validators.Email),
                Message = "Invalid Email"
            };
            result.Validator = (val) =>
            {
                if (Regex.IsMatch(val?.ToString()?.Trim() ?? string.Empty, @".+@.+"))
                {
                    return null;
                }
                else
                {
                    return new KeyValuePair<string, string>(result.Name, result.Message);
                }
            };
            return result;
        }

        public static ValidatorFn Pattern(Regex regex, string patternName) {

            var result = new ValidatorFn()
            {
                Name = $"{patternName}Pattern",
                Message = "Invalid Email"
            };
            result.Validator = (val) =>
            {
                return regex.IsMatch(val?.ToString() ?? string.Empty) ? null : new KeyValuePair<string, string>(result.Name , result.Message);
            };
            return result;
        }

    }
}
