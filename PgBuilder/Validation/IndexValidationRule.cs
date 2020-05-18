namespace Validation
{
    using System;
    using System.Globalization;
    using System.Windows.Controls;

    public class IndexValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return ValidationResult.ValidResult;
        }
    }
}
