using FluentValidation;
using SimpleRestAPI.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRestAPI.Shared.Validations
{
    public class EmployeeViewModelValidation : BaseAbstractValidations<EmployeeViewModel>
    {
        protected bool IsUpdate { get; private set; }

        public EmployeeViewModelValidation(bool isUpdate = false)
        {
            IsUpdate = isUpdate;

            When(p => !IsUpdate, () =>
            {
                ValidateEmptyValues();
                ValidateAge();
            });
        }

        private void ValidateEmptyValues()
        {
            ValidateNotEmpty(p => p.FirstName).WithMessage(GetMessageFieldMustBeProvided());
            ValidateNotEmpty(p => p.LastName).WithMessage(GetMessageFieldMustBeProvided());
            ValidateNotEmpty(p => p.Email).WithMessage(GetMessageFieldMustBeProvided());
            ValidateNotEmpty(p => p.DocNumber).WithMessage(GetMessageFieldMustBeProvided());
            ValidateNotEmpty(p => p.BirthDate).WithMessage(GetMessageFieldMustBeProvided());
            ValidateNotEmpty(p => p.ManagerId).WithMessage(GetMessageFieldMustBeProvided());
            ValidateNotEmpty(p => p.ManagerName).WithMessage(GetMessageFieldMustBeProvided());
            ValidateNotEmpty(p => p.RoleId).WithMessage(GetMessageFieldMustBeProvided());
        }

        private void ValidateAge()
        {
            var minAge = 18;
            var today = DateTime.Today;

            RuleFor(p => p.BirthDate)
                .Must(a => (today.Year - a.Year) >= minAge)
                .WithMessage($"Idade mínima para cadastro é de {minAge} anos.");
        }
    }
}
