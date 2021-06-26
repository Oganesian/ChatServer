using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace ChatClient.ValidationRules
{
    public class UsernameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                var username = value.ToString();
                Regex regex = new Regex("^.{3,32}#[0-9]{4}$");
                return regex.IsMatch(username) ? ValidationResult.ValidResult : new ValidationResult(false, "Ungültiger Username");
            }
            return new ValidationResult(false, "Geben Sie einen Username ein");
        }
    }
}
