
using FluentValidation;
using SimpleRestAPI.Shared.Constants;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace SimpleRestAPI.Shared.Validations
{
    public abstract class BaseAbstractValidations<T> : AbstractValidator<T>
    {
        protected const string PropertyName = "{PropertyName}";

        protected virtual string GetMessageFieldMustBeProvided(string field = PropertyName)
            => string.Format(ValidationsConstants.Messages.FieldMustBeProvided, field);

        protected virtual string GetMessageMaxLength(string field, int maxLength)
            => string.Format(ValidationsConstants.Messages.MaxLength, field, maxLength);

        protected virtual string GetMessageMustNotContainSpecialCharacter(string field)
            => string.Format(ValidationsConstants.Messages.FieldMustNotContainSpecialCharacter, field);

        protected virtual string GetMessageGreaterThanDateTime(DateTime maxDateTime, string field = PropertyName)
            => string.Format(ValidationsConstants.Messages.GreaterThanDateTime, field, maxDateTime);

        protected virtual string GetMessageInvalidSize(decimal maxSize)
            => string.Format(ValidationsConstants.Messages.InvalidSize, PropertyName, maxSize);

        protected virtual string GetMessageMinLength(string field, int maxLength)
            => string.Format(ValidationsConstants.Messages.MinLength, field, maxLength);

        protected IRuleBuilderOptions<T, TProperty> ValidateSpecialCharacters<TProperty>(Expression<Func<T, TProperty>> expression)
            => RuleFor(expression).Must(value =>
                                  {
                                      var strValue = value as string;

                                      if (String.IsNullOrEmpty(strValue))
                                          return true;

                                      return !BaseAbstractValidations<T>.HasSpecialCharacters(strValue);
                                  })
                                  .WithMessage(GetMessageMustNotContainSpecialCharacter(PropertyName));

        protected IRuleBuilderOptions<T, TProperty> ValidateSpecialCharactersExceptHifen<TProperty>(Expression<Func<T, TProperty>> expression)
            => RuleFor(expression).Must(value =>
        {
            var strValue = value as string;

            if (String.IsNullOrEmpty(strValue))
                return true;

            return !BaseAbstractValidations<T>.HasSpecialCharactersExceptHifen(strValue);
        })
                          .WithMessage(GetMessageMustNotContainSpecialCharacter(PropertyName));

        protected IRuleBuilderOptions<T, TProperty> ValidateNotEmpty<TProperty>(Expression<Func<T, TProperty>> expression)
            => RuleFor(expression).NotEmpty()
                                  .WithMessage(GetMessageFieldMustBeProvided(PropertyName));

        protected IRuleBuilderOptions<T, TProperty> ValidateMaxLength<TProperty>(Expression<Func<T, TProperty>> expression, int maxLength)
            => RuleFor(expression).Must(value =>
                                  {
                                      var strValue = value as string;

                                      if (String.IsNullOrEmpty(strValue))
                                          return true;

                                      return !BaseAbstractValidations<T>.MaximumLength(strValue, maxLength);
                                  })
                                  .WithMessage(GetMessageMaxLength(PropertyName, maxLength));

        protected IRuleBuilderOptions<T, TProperty> ValidateMinLength<TProperty>(Expression<Func<T, TProperty>> expression, int maxLength)
            => RuleFor(expression).Must(value =>
            {
                var strValue = value as string;

                if (String.IsNullOrEmpty(strValue))
                    return true;

                return !BaseAbstractValidations<T>.MinimumLength(strValue, maxLength);
            })
                                  .WithMessage(GetMessageMinLength(PropertyName, maxLength));
        protected IRuleBuilderOptions<T, decimal?> ValidateMaxSize(Expression<Func<T, decimal?>> expression, decimal maxSize)
            => RuleFor(expression).LessThanOrEqualTo(maxSize)
                                  .WithMessage(GetMessageInvalidSize(maxSize));

        private static bool HasSpecialCharacters(string input)
            => input.Any(ch => !char.IsLetterOrDigit(ch) && ch != ' ');

        private static bool HasSpecialCharactersExceptHifen(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            var regex = new Regex(@"[^\w\s-]");
            return regex.IsMatch(input);
        }
        private static bool MaximumLength(string input, int maxLength)
            => input.Length > maxLength;

        private static bool MinimumLength(string input, int minLength)
            => input.Length < minLength;
    }
}
