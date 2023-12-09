﻿using FluentValidation;
using NN.POS.Model.Dtos.BusinessPartners;
using NN.POS.Model.Enums;

namespace NN.POS.Web.Validations.Contacts
{
    public class UpdateCustomerValidator : AbstractValidator<UpdateBusinessPartnerDto>
    {
        public UpdateCustomerValidator()
        {
            RuleFor(i => i.BusinessType)
                .NotEmpty()
                .WithMessage("Business Type is require")
                .Must(i => i != BusinessPartnerEnum.BusinessType.None)
                .WithMessage("Please select Business Type");

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Address is required");

            RuleFor(x => x.ContactType)
                .NotEmpty()
                .WithMessage("Contact Type is required")
                .Must(i => i != BusinessPartnerEnum.ContactType.None)
                .WithMessage("Please select Contact Type");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Email is not a valid email address");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name is required")
                .MaximumLength(20)
                .WithMessage("First name must be less than 20 characters");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last name is required")
                .MaximumLength(20)
                .WithMessage("Last name must be less than 20 characters");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required");

        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result =
                await ValidateAsync(ValidationContext<UpdateBusinessPartnerDto>.CreateWithOptions((UpdateBusinessPartnerDto)model, x => x.IncludeProperties(propertyName)));
            return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
        };
    }
}