using System.Reflection;
using FluentValidation;

namespace SimpleRestAPI.Shared.Validations
{
    public static class FluentValidationConfig
    {
        public static void ConfigureFluentValidation()
        {
            ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, expression) =>
            {
                return memberInfo.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>()?.GetName();
            };
        }
    }
}
