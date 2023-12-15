﻿using FluentValidation;
using NN.POS.Model.Dtos.ItemMasters;
using NN.POS.Model.Enums;
using static NN.POS.Model.Enums.BusinessPartnerEnum;

namespace NN.POS.Web.Validations.ItemMasters;

public class CreateItemMasterDataValidator : AbstractValidator<CreateItemMasterDataDto>
{

    public CreateItemMasterDataValidator()
    {
        RuleFor(i => i.Name)
            .NotEmpty()
            .WithMessage("Name cannot be null!");

        RuleFor(i => i.Code)
            .NotEmpty()
            .WithMessage("Code cannot be null!");

        RuleFor(i => i.Barcode)
            .NotEmpty()
            .WithMessage("Barcode cannot be null!");

        RuleFor(i => i.PriceListId)
            .Must(i => i > 0)
            .WithMessage("Please choose price list!");

        RuleFor(i => i.BaseUomId)
            .Must(i => i > 0)
            .WithMessage("Please choose Unit of measure (Uom)!");

        RuleFor(i => i.Process)
            .NotEmpty()
            .WithMessage("Process cannot be null!")
            .Must(i => i != ItemMasterDataProcess.None)
            .WithMessage("Please select Process");

        RuleFor(i => i.Type)
            .NotEmpty()
            .WithMessage("Type cannot be null!")
            .Must(i => i != ItemMasterDataType.None)
            .WithMessage("Please select Type");

        RuleFor(i => i.UomGroupId)
            .Must(i => i > 0)
            .WithMessage("Please choose Unit of measure Group (UomG)!");

        RuleFor(i => i.WarehouseId)
            .Must(i => i > 0)
            .WithMessage("Please choose warehouse!");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result =
            await ValidateAsync(ValidationContext<CreateItemMasterDataDto>.CreateWithOptions((CreateItemMasterDataDto)model, x => x.IncludeProperties(propertyName)));
        return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
    };
}